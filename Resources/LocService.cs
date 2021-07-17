using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// https://damienbod.com/2017/11/01/shared-localization-in-asp-net-core-mvc/
// https://github.com/damienbod/AspNetCoreMvcSharedLocalization/tree/master/AspNetCoreMvcSharedLocalization

namespace Web.Resources
{
    using System.Reflection;

    using Microsoft.Extensions.Localization;

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
