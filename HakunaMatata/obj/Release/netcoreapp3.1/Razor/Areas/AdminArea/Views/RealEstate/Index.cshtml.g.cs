#pragma checksum "D:\DUT\DATN\HakunaMatata\HakunaMatata\Areas\AdminArea\Views\RealEstate\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0f20c3599138b3bf5cee9bb0f8380f7ab35185fe"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_AdminArea_Views_RealEstate_Index), @"mvc.1.0.view", @"/Areas/AdminArea/Views/RealEstate/Index.cshtml")]
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
#nullable restore
#line 1 "D:\DUT\DATN\HakunaMatata\HakunaMatata\Areas\AdminArea\Views\_ViewImports.cshtml"
using HakunaMatata;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\DUT\DATN\HakunaMatata\HakunaMatata\Areas\AdminArea\Views\_ViewImports.cshtml"
using HakunaMatata.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\DUT\DATN\HakunaMatata\HakunaMatata\Areas\AdminArea\Views\_ViewImports.cshtml"
using System.Security.Claims;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\DUT\DATN\HakunaMatata\HakunaMatata\Areas\AdminArea\Views\_ViewImports.cshtml"
using System.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\DUT\DATN\HakunaMatata\HakunaMatata\Areas\AdminArea\Views\_ViewImports.cshtml"
using HakunaMatata.Helpers;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0f20c3599138b3bf5cee9bb0f8380f7ab35185fe", @"/Areas/AdminArea/Views/RealEstate/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"78366e9c7d2962e661ce2eb237f191be827e513e", @"/Areas/AdminArea/Views/_ViewImports.cshtml")]
    public class Areas_AdminArea_Views_RealEstate_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/mustache.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/pagination.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/admin/js/services/realEstateServices.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            DefineSection("scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0f20c3599138b3bf5cee9bb0f8380f7ab35185fe4920", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0f20c3599138b3bf5cee9bb0f8380f7ab35185fe6019", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0f20c3599138b3bf5cee9bb0f8380f7ab35185fe7118", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
            }
            );
            WriteLiteral("\r\n");
#nullable restore
#line 7 "D:\DUT\DATN\HakunaMatata\HakunaMatata\Areas\AdminArea\Views\RealEstate\Index.cshtml"
  
    int userId = User.Identity.IsAuthenticated ? Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "UserId").Value) : 0;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"row\">\r\n    <div class=\"col-6 mt-2 pb-3\">\r\n        <input type=\"text\" class=\"form-control\"");
            BeginWriteAttribute("value", " value=\"", 437, "\"", 445, 0);
            EndWriteAttribute();
            WriteLiteral(@" id=""search-for-anything"" placeholder=""Nhập từ tìm kiếm"" />
    </div>
</div>
<table class=""table table-striped table-bordered"">
    <thead>
        <tr>
            <th class=""text-center"">#</th>
            <th>Địa chỉ</th>
            <th>Giá/tháng</th>
            <th>Loại phòng</th>
            <th>Người đăng</th>
            <th>Ngày đăng</th>
            <th>Ngày hết hạn</th>
            <th>Tình trạng</th>
            <th></th>
        </tr>
    </thead>

    <tbody id=""real-estate-list-data"">
    </tbody>
</table>

");
#nullable restore
#line 35 "D:\DUT\DATN\HakunaMatata\HakunaMatata\Areas\AdminArea\Views\RealEstate\Index.cshtml"
Write(Html.AntiForgeryToken());

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""pb-1"" id=""pagination""></div>
<input type=""hidden"" id=""paging-current-index"" />

<script id=""data-template"" type=""x-tmpl-mustache"">
    <tr>
        <td class=""text-center"">{{Index}}</td>
        <td>{{Street}}</td>
        <td>{{Price}} VNĐ</td>
        <td>{{Type}}</td>
        <td>{{Agent}}</td>
        <td>{{PostDate}}</td>
        <td>{{ExpireTime}}</td>
        <td>{{Status}}</td>
        <td class=""text-center"">
            <a href=""");
#nullable restore
#line 50 "D:\DUT\DATN\HakunaMatata\HakunaMatata\Areas\AdminArea\Views\RealEstate\Index.cshtml"
                Write(Url.Action("Details","RealEstate"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"?id={{Id}}"" title=""Chi tiết"" class=""btn btn-blue btn-circle"">
                <i class=""fas fa-info"" al></i>
            </a>
            <form onsubmit=""return realEstateServices.disableRealEstate(this);"" class=""d-inline"">
                <input type=""hidden"" name=""id"" value=""{{Id}}"" />
                <button class=""btn btn-warning btn-circle"" title=""Khóa"" type=""submit"">
                    <i class=""fas fa-store-alt-slash""></i>
                </button>
            </form>
            <form onsubmit=""return realEstateServices.deleteRealEstate(this);"" class=""d-inline"">
                <input type=""hidden"" name=""id"" value=""{{Id}}"" />
                <input type=""hidden"" name=""userId"" value=""");
#nullable restore
#line 61 "D:\DUT\DATN\HakunaMatata\HakunaMatata\Areas\AdminArea\Views\RealEstate\Index.cshtml"
                                                     Write(userId);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" />\r\n                <button class=\"btn btn-danger btn-circle\" title=\"Xóa\" type=\"submit\">\r\n                    <i class=\"fas fa-times\"></i>\r\n                </button>\r\n            </form>\r\n        </td>\r\n    </tr>\r\n</script>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
