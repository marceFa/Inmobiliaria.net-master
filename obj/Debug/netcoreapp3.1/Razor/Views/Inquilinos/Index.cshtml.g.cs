#pragma checksum "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Inquilinos\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6a8da15a1f15485bf099ab543d10555832f004ef"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Inquilinos_Index), @"mvc.1.0.view", @"/Views/Inquilinos/Index.cshtml")]
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
#line 1 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\_ViewImports.cshtml"
using Inmobiliaria;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\_ViewImports.cshtml"
using Inmobiliaria.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6a8da15a1f15485bf099ab543d10555832f004ef", @"/Views/Inquilinos/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7bd7a903e1653a1f636f3a9c0022012b370304d0", @"/Views/_ViewImports.cshtml")]
    public class Views_Inquilinos_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Inmobiliaria.Models.Inquilino>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 3 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Inquilinos\Index.cshtml"
   ViewData["Title"] = "Indice"; 

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<h2>Listado de Inquilinos</h2>\n\n<p>\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6a8da15a1f15485bf099ab543d10555832f004ef4123", async() => {
                WriteLiteral("Agregar Inquilino");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n</p>\n<table height=\"100\" width=\"100\" background=\"../imagenes/inmo.png\" class=\"table\">\n    <thead>\n        <tr>\n            <th bgcolor=\"#4CAF50\">\n                ");
#nullable restore
#line 14 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Inquilinos\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Nombre));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th>\n            <th bgcolor=\"#4CAF50\">\n                ");
#nullable restore
#line 17 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Inquilinos\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Apellido));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th>\n            <th bgcolor=\"#4CAF50\">\n                ");
#nullable restore
#line 20 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Inquilinos\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Dni));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th>\n            <th bgcolor=\"#4CAF50\">\n                ");
#nullable restore
#line 23 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Inquilinos\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Telefono));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
            </th>
            <th bgcolor=""#4CAF50"" <label>Lugar Trabajo</label>
            </th>
            <th bgcolor=""#4CAF50"" <label>Nombre Garante</label>
            </th>
            <th bgcolor=""#4CAF50"" <label>Tel Garante</label>
            </th>
            <th bgcolor=""#4CAF50""></th>
        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 35 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Inquilinos\Index.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\n                <td>\n                    ");
#nullable restore
#line 39 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Inquilinos\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.Nombre));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n                    ");
#nullable restore
#line 42 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Inquilinos\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.Apellido));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n                    ");
#nullable restore
#line 45 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Inquilinos\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.Dni));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n                    ");
#nullable restore
#line 48 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Inquilinos\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.Telefono));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n                    ");
#nullable restore
#line 51 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Inquilinos\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.LugarDeTrabajo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n                    ");
#nullable restore
#line 54 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Inquilinos\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.nombreGarante));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n                    ");
#nullable restore
#line 57 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Inquilinos\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.telefonoGarante));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n                    ");
#nullable restore
#line 60 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Inquilinos\Index.cshtml"
               Write(Html.ActionLink("Editar", "Edit", new { Id = item.idInquilino }));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\n\n                    ");
#nullable restore
#line 62 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Inquilinos\Index.cshtml"
               Write(Html.ActionLink("Eliminar", "Delete", new { id = item.idInquilino }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n            </tr>\n");
#nullable restore
#line 65 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Inquilinos\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\n</table>\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Inmobiliaria.Models.Inquilino>> Html { get; private set; }
    }
}
#pragma warning restore 1591
