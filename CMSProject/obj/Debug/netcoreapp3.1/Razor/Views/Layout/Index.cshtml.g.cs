#pragma checksum "C:\Users\Aleyna\Desktop\CMSProject\CMSProject\Views\Layout\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ac2af2d264988bd0157d4e7890799c43e181e418"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Layout_Index), @"mvc.1.0.view", @"/Views/Layout/Index.cshtml")]
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
#line 1 "C:\Users\Aleyna\Desktop\CMSProject\CMSProject\Views\_ViewImports.cshtml"
using CMSProject;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Aleyna\Desktop\CMSProject\CMSProject\Views\_ViewImports.cshtml"
using CMSProject.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ac2af2d264988bd0157d4e7890799c43e181e418", @"/Views/Layout/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c10a02efca8adb87913b2fc31d5350df3bf97ae0", @"/Views/_ViewImports.cshtml")]
    public class Views_Layout_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Common.Dtos.LayoutDto>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\Aleyna\Desktop\CMSProject\CMSProject\Views\Layout\Index.cshtml"
   ViewBag.Title = "Index"; 

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""row"">
    <div class=""col-md-12"">
        <div class=""row"">
            <div class=""pull-right"">
                <a class=""btn btn-success btn-circle"" href=""/Layout/Add""><i class=""fa fa-plus""></i> Ekle</a>
            </div>
        </div>

        <!-- BEGIN EXAMPLE TABLE PORTLET-->
        <div class=""portlet light bordered"">
            <div class=""portlet-title"">
                <div class=""caption font-dark"">
                    <i class=""icon-settings font-dark""></i>
                    <span class=""caption-subject bold uppercase"">Layoutlar</span>
                </div>

                <div class=""tools""> </div>

            </div>
            <div class=""portlet-body"">
                <table class=""table table-striped table-bordered table-hover"" id=""sample_1"">
                    <thead>
                        <tr>
                            <th> # </th>
                            <th> Layout Adı </th>
                            <th> Kolon Sayısı </th>
                            <th>");
            WriteLiteral(" İşlemler </th>\n                        </tr>\n                    </thead>\n                    <tbody>\n");
#nullable restore
#line 35 "C:\Users\Aleyna\Desktop\CMSProject\CMSProject\Views\Layout\Index.cshtml"
                         foreach (var laypoutBilgi in Model)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\n                <td>\n                    ");
#nullable restore
#line 39 "C:\Users\Aleyna\Desktop\CMSProject\CMSProject\Views\Layout\Index.cshtml"
               Write(laypoutBilgi.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>");
#nullable restore
#line 41 "C:\Users\Aleyna\Desktop\CMSProject\CMSProject\Views\Layout\Index.cshtml"
               Write(laypoutBilgi.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\n                <td>");
#nullable restore
#line 42 "C:\Users\Aleyna\Desktop\CMSProject\CMSProject\Views\Layout\Index.cshtml"
               Write(laypoutBilgi.Items.Count());

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\n                <td>\n                    <a class=\"btn btn-primary btn-circle\"");
            BeginWriteAttribute("href", " href=\"", 1600, "\"", 1651, 2);
            WriteAttributeValue("", 1607, "/Layout/Update?layoutName=", 1607, 26, true);
#nullable restore
#line 44 "C:\Users\Aleyna\Desktop\CMSProject\CMSProject\Views\Layout\Index.cshtml"
WriteAttributeValue("", 1633, laypoutBilgi.Name, 1633, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\n                        <i class=\"fa fa-edit\"></i>\n                    </a>\n                    <a class=\"btn btn-danger btn-circle\"");
            BeginWriteAttribute("href", " href=\"", 1786, "\"", 1837, 2);
            WriteAttributeValue("", 1793, "/Layout/Delete?layoutName=", 1793, 26, true);
#nullable restore
#line 47 "C:\Users\Aleyna\Desktop\CMSProject\CMSProject\Views\Layout\Index.cshtml"
WriteAttributeValue("", 1819, laypoutBilgi.Name, 1819, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\n                        <i class=\"fa fa-trash\"></i>\n                    </a>\n                </td>\n            </tr>\n");
#nullable restore
#line 52 "C:\Users\Aleyna\Desktop\CMSProject\CMSProject\Views\Layout\Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </tbody>\n                </table>\n            </div>\n        </div>\n        <!-- END EXAMPLE TABLE PORTLET-->\n    </div>\n</div>\n<!-- END EXAMPLE TABLE PORTLET-->");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Common.Dtos.LayoutDto>> Html { get; private set; }
    }
}
#pragma warning restore 1591
