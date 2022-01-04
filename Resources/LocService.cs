using Microsoft.Extensions.Localization;
using System.Reflection;


// https://damienbod.com/2017/11/01/shared-localization-in-asp-net-core-mvc/
// https://github.com/damienbod/AspNetCoreMvcSharedLocalization/tree/master/AspNetCoreMvcSharedLocalization

namespace Web.Resources
{
    public class LocService
    {
        private readonly IStringLocalizer _localizer;

        public LocService(IStringLocalizerFactory factory)
        {
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("SharedResource", assemblyName.Name);
        }

        public LocalizedString GetLocalizedHtmlString(string key)
        {
            return _localizer[key];
        }
    }
}
