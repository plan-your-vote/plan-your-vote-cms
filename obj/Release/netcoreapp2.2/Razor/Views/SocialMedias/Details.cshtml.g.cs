#pragma checksum "C:\projects\plan-your-vote-cms-develop\Web\Views\SocialMedias\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2530aeac263e38f39ce612922be4d23efb7318d7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_SocialMedias_Details), @"mvc.1.0.view", @"/Views/SocialMedias/Details.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/SocialMedias/Details.cshtml", typeof(AspNetCore.Views_SocialMedias_Details))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 2 "C:\projects\plan-your-vote-cms-develop\Web\Views\_ViewImports.cshtml"
using Web.Models;

#line default
#line hidden
#line 3 "C:\projects\plan-your-vote-cms-develop\Web\Views\_ViewImports.cshtml"
using Web.CmsControllers;

#line default
#line hidden
#line 2 "C:\projects\plan-your-vote-cms-develop\Web\Views\SocialMedias\Details.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
#line 3 "C:\projects\plan-your-vote-cms-develop\Web\Views\SocialMedias\Details.cshtml"
using Microsoft.Extensions.Localization;

#line default
#line hidden
#line 4 "C:\projects\plan-your-vote-cms-develop\Web\Views\SocialMedias\Details.cshtml"
using Web;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2530aeac263e38f39ce612922be4d23efb7318d7", @"/Views/SocialMedias/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"94d61326fb64e86108578a76380a5f57f5fd475a", @"/Views/_ViewImports.cshtml")]
    public class Views_SocialMedias_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Web.Models.SocialMedia>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-info"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 6 "C:\projects\plan-your-vote-cms-develop\Web\Views\SocialMedias\Details.cshtml"
  
    ViewData["Title"] = @Localizer["socialMedia_detail_title"];

#line default
#line hidden
            BeginContext(248, 5, true);
            WriteLiteral("\n<h1>");
            EndContext();
            BeginContext(254, 17, false);
#line 10 "C:\projects\plan-your-vote-cms-develop\Web\Views\SocialMedias\Details.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
            EndContext();
            BeginContext(271, 104, true);
            WriteLiteral("</h1>\n\n<div class=\"panel panel-default\">\n    <dl class=\"row\">\n        <dt class=\"col-sm-2\">\n            ");
            EndContext();
            BeginContext(376, 45, false);
#line 15 "C:\projects\plan-your-vote-cms-develop\Web\Views\SocialMedias\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.MediaName));

#line default
#line hidden
            EndContext();
            BeginContext(421, 58, true);
            WriteLiteral("\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
            EndContext();
            BeginContext(480, 41, false);
#line 18 "C:\projects\plan-your-vote-cms-develop\Web\Views\SocialMedias\Details.cshtml"
       Write(Html.DisplayFor(model => model.MediaName));

#line default
#line hidden
            EndContext();
            BeginContext(521, 57, true);
            WriteLiteral("\n        </dd>\n        <dt class=\"col-sm-2\">\n            ");
            EndContext();
            BeginContext(579, 43, false);
#line 21 "C:\projects\plan-your-vote-cms-develop\Web\Views\SocialMedias\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Message));

#line default
#line hidden
            EndContext();
            BeginContext(622, 58, true);
            WriteLiteral("\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
            EndContext();
            BeginContext(681, 39, false);
#line 24 "C:\projects\plan-your-vote-cms-develop\Web\Views\SocialMedias\Details.cshtml"
       Write(Html.DisplayFor(model => model.Message));

#line default
#line hidden
            EndContext();
            BeginContext(720, 57, true);
            WriteLiteral("\n        </dd>\n        <dt class=\"col-sm-2\">\n            ");
            EndContext();
            BeginContext(778, 40, false);
#line 27 "C:\projects\plan-your-vote-cms-develop\Web\Views\SocialMedias\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Link));

#line default
#line hidden
            EndContext();
            BeginContext(818, 58, true);
            WriteLiteral("\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
            EndContext();
            BeginContext(877, 36, false);
#line 30 "C:\projects\plan-your-vote-cms-develop\Web\Views\SocialMedias\Details.cshtml"
       Write(Html.DisplayFor(model => model.Link));

#line default
#line hidden
            EndContext();
            BeginContext(913, 42, true);
            WriteLiteral("\n        </dd>\n    </dl>\n</div>\n<div>\n    ");
            EndContext();
            BeginContext(955, 98, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2530aeac263e38f39ce612922be4d23efb7318d78051", async() => {
                BeginContext(1026, 23, false);
#line 35 "C:\projects\plan-your-vote-cms-develop\Web\Views\SocialMedias\Details.cshtml"
                                                                     Write(Localizer["editButton"]);

#line default
#line hidden
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 35 "C:\projects\plan-your-vote-cms-develop\Web\Views\SocialMedias\Details.cshtml"
                                                   WriteLiteral(Model.ID);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1053, 6, true);
            WriteLiteral(" \n    ");
            EndContext();
            BeginContext(1059, 71, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2530aeac263e38f39ce612922be4d23efb7318d710658", async() => {
                BeginContext(1103, 23, false);
#line 36 "C:\projects\plan-your-vote-cms-develop\Web\Views\SocialMedias\Details.cshtml"
                                          Write(Localizer["backButton"]);

#line default
#line hidden
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1130, 8, true);
            WriteLiteral("\n</div>\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IStringLocalizer<SharedResource> Localizer { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Web.Models.SocialMedia> Html { get; private set; }
    }
}
#pragma warning restore 1591