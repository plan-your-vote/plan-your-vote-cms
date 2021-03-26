using GeoCoordinatePortable;
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
        public static string AccessToken { get; set; }
        public static MapConfiguration MapConfiguration { get; set; }

        public static async Task<string> GetAccessToken()
        {
            string accessToken = AccessToken;

            try
            {
                if (string.IsNullOrEmpty(accessToken))
                {
                    /*
                    SecretBundle secret = null;

                    KeyVaultClient keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(new AzureServiceTokenProvider().KeyVaultTokenCallback));

                    secret = await keyVaultClient
                        .GetSecretAsync($"https://{MapConfiguration.KeyVaultName}.vault.azure.net/secrets/{MapConfiguration.SecretName}")
                        .ConfigureAwait(false);

                    if (secret != null)
                    {
                        accessToken = secret.Value;
                    }
                    */
                    accessToken = MapConfiguration.MapBoxToken;
                }
            }
            catch (KeyVaultErrorException kvee)
            {
                // TODO
            }

            return accessToken;
        }

        public MapController(ApplicationDbContext context, IOptions<MapConfiguration> mapConfiguration)
        {
            _context = context;

            MapConfiguration = mapConfiguration.Value;

            AccessToken = MapConfiguration.MapBoxToken; //GetAccessToken().GetAwaiter().GetResult();
        }

        [HttpGet("{location}")]
        public async Task<ActionResult<object>> GetClosestLocations(string location)
        {
            var distances = new List<DistanceDTO>();

            try
            {
                double longitude = double.Parse(location.Split(',')[0]);
                double latitude = double.Parse(location.Split(',')[1]);

                var pollingPlaceCoordinate = new GeoCoordinate(latitude, longitude);

                var nearestPollingPlaces = _context.PollingPlaces
                                        .Where(pollingPlace => pollingPlace.ElectionId == _context.StateSingleton.First().RunningElectionID)
                                        .OrderBy(pollingPlace => new GeoCoordinate(pollingPlace.Latitude, pollingPlace.Longitude).GetDistanceTo(pollingPlaceCoordinate))
                                        .Take(20)
                                        .ToList();

                foreach (var pollingPlace in nearestPollingPlaces)
                {
                    var response = await client
                        .GetAsync(
                            $"{longitude},{latitude};" +
                            $"{pollingPlace.Longitude},{pollingPlace.Latitude}" +
                            $"?access_token={AccessToken}"
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
                        PollingPlaceID = pollingPlace.PollingPlaceId,
                        Distance = map.Routes
                        .Select(routes => routes.Distance)
                        .First(),
                    });
                }
            }
            catch (Exception ex)
            {
                return ex;
            }


            return distances.OrderBy(distance => distance.Distance).Take(10).ToList();
        }
    }
}
