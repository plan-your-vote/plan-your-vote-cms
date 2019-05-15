using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Web.ApiDTO;
using Web.Data;
using Web.Models;

namespace Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpClient client = new HttpClient() { BaseAddress = new Uri("https://api.mapbox.com/directions/v5/mapbox/driving/") };
        public static string access_token;

        public MapConfiguration MapConfiguration { get; set; }

        public MapController(ApplicationDbContext context, IOptions<MapConfiguration> mapConfiguration)
        {
            _context = context;

            MapConfiguration = mapConfiguration.Value;
        }

        [HttpGet("{location}")]
        public async Task<ActionResult<object>> GetClosestLocations(string location)
        {
            KeyVaultClient keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(new AzureServiceTokenProvider().KeyVaultTokenCallback));

            var secret = await keyVaultClient
                .GetSecretAsync($"https://{MapConfiguration.KeyVaultName}.vault.azure.net/secrets/App/{MapConfiguration.SecretName}")
                .ConfigureAwait(false);

            access_token = secret.Value;

            throw new ApplicationException($"mapConfig: {MapConfiguration.KeyVaultName}, {MapConfiguration.SecretName} | Access token: {access_token}");

            var distances = new List<DistanceDTO>();

            try
            {
                double longitude = double.Parse(location.Split(',')[0]);
                double latitude = double.Parse(location.Split(',')[1]);

                foreach (var pollingplace in _context.PollingPlaces)
                {
                    var response = await client
                        .GetAsync(
                            $"{longitude},{latitude};" +
                            $"{pollingplace.Longitude},{pollingplace.Latitude}" +
                            $"?access_token={access_token}"
                        )
                        .ConfigureAwait(false);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new ArgumentException(response.ReasonPhrase);
                    }

                    var map = JsonConvert.DeserializeObject<Map>(
                        await response.Content
                        .ReadAsStringAsync()
                        .ConfigureAwait(false));

                    distances.Add(new DistanceDTO
                    {
                        PollingPlaceID = pollingplace.PollingPlaceId,
                        Distance = map.Routes
                        .Select(routes => routes.Distance)
                        .First(),
                    });
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex);
            }
            catch (FormatException ex)
            {
                return BadRequest(ex);
            }

            return distances.OrderBy(distance => distance.Distance).ToList();
        }
    }
}
