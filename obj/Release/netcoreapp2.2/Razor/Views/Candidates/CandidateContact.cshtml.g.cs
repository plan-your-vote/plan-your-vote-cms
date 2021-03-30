#pragma checksum "C:\projects\plan-your-vote-cms-develop\Web\Views\Candidates\CandidateContact.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "dd82c92fb5982a94e6ddb533454bb2f5ca79c540"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Candidates_CandidateContact), @"mvc.1.0.view", @"/Views/Candidates/CandidateContact.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Candidates/CandidateContact.cshtml", typeof(AspNetCore.Views_Candidates_CandidateContact))]
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
#line 1 "C:\projects\plan-your-vote-cms-develop\Web\Views\Candidates\CandidateContact.cshtml"
using Microsoft.Extensions.Localization;

#line default
#line hidden
#line 2 "C:\projects\plan-your-vote-cms-develop\Web\Views\Candidates\CandidateContact.cshtml"
using Web;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dd82c92fb5982a94e6ddb533454bb2f5ca79c540", @"/Views/Candidates/CandidateContact.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"94d61326fb64e86108578a76380a5f57f5fd475a", @"/Views/_ViewImports.cshtml")]
    public class Views_Candidates_CandidateContact : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Web.ViewModels.CandidateViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(103, 1, true);
            WriteLiteral("\n");
            EndContext();
            BeginContext(145, 1, true);
            WriteLiteral("\n");
            EndContext();
#line 7 "C:\projects\plan-your-vote-cms-develop\Web\Views\Candidates\CandidateContact.cshtml"
  
    var lastIndex = Model.Candidate.Contacts.Count - 1;

#line default
#line hidden
            BeginContext(207, 52, true);
            WriteLiteral("\n<div class=\"one-line\">\n    <p class=\"lead\">Contact ");
            EndContext();
            BeginContext(261, 11, false);
#line 12 "C:\projects\plan-your-vote-cms-develop\Web\Views\Candidates\CandidateContact.cshtml"
                        Write(lastIndex+1);

#line default
#line hidden
            EndContext();
            BeginContext(273, 30, true);
            WriteLiteral("</p>\n    <button type=\"button\"");
            EndContext();
            BeginWriteAttribute("id", " id=\"", 303, "\"", 332, 2);
            WriteAttributeValue("", 308, "removeContact_", 308, 14, true);
#line 13 "C:\projects\plan-your-vote-cms-develop\Web\Views\Candidates\CandidateContact.cshtml"
WriteAttributeValue("", 322, lastIndex, 322, 10, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(333, 38, true);
            WriteLiteral(" class=\"btn btn-primary margin-bottom\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 371, "\"", 406, 3);
            WriteAttributeValue("", 381, "removeContact(", 381, 14, true);
#line 13 "C:\projects\plan-your-vote-cms-develop\Web\Views\Candidates\CandidateContact.cshtml"
WriteAttributeValue("", 395, lastIndex, 395, 10, false);

#line default
#line hidden
            WriteAttributeValue("", 405, ")", 405, 1, true);
            EndWriteAttribute();
            BeginContext(407, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(409, 43, false);
#line 13 "C:\projects\plan-your-vote-cms-develop\Web\Views\Candidates\CandidateContact.cshtml"
                                                                                                                             Write(Localizer["candidates_removeContactButton"]);

#line default
#line hidden
            EndContext();
            BeginContext(452, 46, true);
            WriteLiteral("</button>\n</div>\n<div class=\"form-group\">\n    ");
            EndContext();
            BeginContext(499, 99, false);
#line 16 "C:\projects\plan-your-vote-cms-develop\Web\Views\Candidates\CandidateContact.cshtml"
Write(Html.LabelFor(c => c.Candidate.Contacts[lastIndex].ContactMethod, new { @class = "control-label" }));

#line default
#line hidden
            EndContext();
            BeginContext(598, 5, true);
            WriteLiteral("\n    ");
            EndContext();
            BeginContext(604, 156, false);
#line 17 "C:\projects\plan-your-vote-cms-develop\Web\Views\Candidates\CandidateContact.cshtml"
Write(Html.DropDownListFor(c => c.Candidate.Contacts[lastIndex].ContactMethod, Html.GetEnumSelectList(typeof(ContactMethod)), "", new { @class = "form-control" }));

#line default
#line hidden
            EndContext();
            BeginContext(760, 5, true);
            WriteLiteral("\n    ");
            EndContext();
            BeginContext(766, 113, false);
#line 18 "C:\projects\plan-your-vote-cms-develop\Web\Views\Candidates\CandidateContact.cshtml"
Write(Html.ValidationMessageFor(c => c.Candidate.Contacts[lastIndex].ContactMethod, "", new { @class = "text-danger" }));

#line default
#line hidden
            EndContext();
            BeginContext(879, 37, true);
            WriteLiteral("\n</div>\n<div class=\"form-group\">\n    ");
            EndContext();
            BeginContext(917, 98, false);
#line 21 "C:\projects\plan-your-vote-cms-develop\Web\Views\Candidates\CandidateContact.cshtml"
Write(Html.LabelFor(c => c.Candidate.Contacts[lastIndex].ContactValue, new { @class = "control-label" }));

#line default
#line hidden
            EndContext();
            BeginContext(1015, 5, true);
            WriteLiteral("\n    ");
            EndContext();
            BeginContext(1021, 123, false);
#line 22 "C:\projects\plan-your-vote-cms-develop\Web\Views\Candidates\CandidateContact.cshtml"
Write(Html.EditorFor(c => c.Candidate.Contacts[lastIndex].ContactValue, new { htmlAttributes = new { @class = "form-control" } }));

#line default
#line hidden
            EndContext();
            BeginContext(1144, 5, true);
            WriteLiteral("\n    ");
            EndContext();
            BeginContext(1150, 112, false);
#line 23 "C:\projects\plan-your-vote-cms-develop\Web\Views\Candidates\CandidateContact.cshtml"
Write(Html.ValidationMessageFor(c => c.Candidate.Contacts[lastIndex].ContactValue, "", new { @class = "text-danger" }));

#line default
#line hidden
            EndContext();
            BeginContext(1262, 14, true);
            WriteLiteral("\n</div>\n<hr />");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Web.ViewModels.CandidateViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591