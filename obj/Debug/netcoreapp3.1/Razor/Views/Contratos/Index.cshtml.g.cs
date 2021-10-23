#pragma checksum "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Contratos\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "47bbe1258c1943e82efc09c0a57fcdcf56c0a98c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Contratos_Index), @"mvc.1.0.view", @"/Views/Contratos/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"47bbe1258c1943e82efc09c0a57fcdcf56c0a98c", @"/Views/Contratos/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7bd7a903e1653a1f636f3a9c0022012b370304d0", @"/Views/_ViewImports.cshtml")]
    public class Views_Contratos_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Inmobiliaria.Models.Contrato>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 3 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Contratos\Index.cshtml"
   ViewData["Title"] = "Lista de Contratos"; 

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<h2>Todos los Contratos</h2>\n\n<h4> ");
#nullable restore
#line 7 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Contratos\Index.cshtml"
Write(Html.ActionLink("Buscar Contratos Vigentes", "Busqueda", "Contratos"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\n\n\n<table height=\"100\" width=\"100\" background=\"../imagenes/inmo.png\" class=\"table\">\n    <thead>\n        <tr>\n            <th bgcolor=\"#4CAF50\">Nº Contrato</th>\n            <th bgcolor=\"#4CAF50\">\n                ");
#nullable restore
#line 15 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Contratos\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Inquilinos));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th>\n            <th bgcolor=\"#4CAF50\">\n                ");
#nullable restore
#line 18 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Contratos\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Inmuebles));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th>\n            <th bgcolor=\"#4CAF50\">Fecha Inicio</th>\n            <th bgcolor=\"#4CAF50\">Fecha Final</th>\n            <th bgcolor=\"#4CAF50\">\n                ");
#nullable restore
#line 23 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Contratos\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.MontoAlquiler));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th>\n            <th bgcolor=\"#4CAF50\">\n                ");
#nullable restore
#line 26 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Contratos\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Vigente));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th>\n            <th bgcolor=\"#4CAF50\"></th>\n        </tr>\n    </thead>\n    <tbody>\n");
#nullable restore
#line 32 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Contratos\Index.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("<tr>\n    <td>\n        ");
#nullable restore
#line 36 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Contratos\Index.cshtml"
   Write(Html.DisplayFor(modelItem => item.idContrato));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    </td>\n    <td>\n        ");
#nullable restore
#line 39 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Contratos\Index.cshtml"
    Write(item.Inquilinos.Nombre + " " + item.Inquilinos.Apellido);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    </td>\n    <td>\n        ");
#nullable restore
#line 42 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Contratos\Index.cshtml"
    Write(item.Inmuebles.Tipo + " " + item.Inmuebles.Direccion);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    </td>\n    <td>\n");
            WriteLiteral("        ");
#nullable restore
#line 46 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Contratos\Index.cshtml"
   Write(item.FechaInicio.ToString("dd/MM/yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    </td>\n    <td>\n");
            WriteLiteral("        ");
#nullable restore
#line 50 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Contratos\Index.cshtml"
   Write(item.FechaFin.ToString("dd/MM/yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    </td>\n    <td>\n        ");
#nullable restore
#line 53 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Contratos\Index.cshtml"
   Write(Html.DisplayFor(modelItem => item.MontoAlquiler));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    </td>\n    <td>\n        ");
#nullable restore
#line 56 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Contratos\Index.cshtml"
   Write(Html.DisplayFor(modelItem => item.Vigente));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    </td>\n    <td>\n        ");
#nullable restore
#line 59 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Contratos\Index.cshtml"
   Write(Html.ActionLink("Editar", "Edit", new { id = item.idContrato }));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\n\n        ");
#nullable restore
#line 61 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Contratos\Index.cshtml"
   Write(Html.ActionLink("Eliminar", "Delete", new { id = item.idContrato }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    </td>\n</tr>");
#nullable restore
#line 63 "C:\Users\Usuario\Desktop\4to_Cuatrimestre\Net\Inmobiliaria.net-master\Views\Contratos\Index.cshtml"
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Inmobiliaria.Models.Contrato>> Html { get; private set; }
    }
}
#pragma warning restore 1591
