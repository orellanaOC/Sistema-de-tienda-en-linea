#pragma checksum "C:\Users\Mich\Documents\tooVSCORE\Sistema_TOO\TiendaOnline.solucion\TiendaOnline\Views\Carrito\IndexC.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d8b10549f5653629c949b9e1d0eaa2df70d786e8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Carrito_IndexC), @"mvc.1.0.view", @"/Views/Carrito/IndexC.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Carrito/IndexC.cshtml", typeof(AspNetCore.Views_Carrito_IndexC))]
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
#line 1 "C:\Users\Mich\Documents\tooVSCORE\Sistema_TOO\TiendaOnline.solucion\TiendaOnline\Views\_ViewImports.cshtml"
using TiendaOnline;

#line default
#line hidden
#line 2 "C:\Users\Mich\Documents\tooVSCORE\Sistema_TOO\TiendaOnline.solucion\TiendaOnline\Views\_ViewImports.cshtml"
using TiendaOnline.Models;

#line default
#line hidden
#line 3 "C:\Users\Mich\Documents\tooVSCORE\Sistema_TOO\TiendaOnline.solucion\TiendaOnline\Views\_ViewImports.cshtml"
using TiendaOnline.ViewModels;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d8b10549f5653629c949b9e1d0eaa2df70d786e8", @"/Views/Carrito/IndexC.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6e93f7432df440ab1b28935e4dfa14ed4c983176", @"/Views/_ViewImports.cshtml")]
    public class Views_Carrito_IndexC : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CarritoVM>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("glyphicon glyphicon-remove text-danger remove"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Carrito", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "EliminarDeCarrito", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(18, 485, true);
            WriteLiteral(@"
<div class=""row checkoutForm"">
    <h2>Carrito de compras</h2>
    <h4>Aqui estan los productos que has agregado.</h4>
    <table class=""table table-bordered table-striped"">
        <thead>
            <tr>
                <th>Cantidad seleccionada</th>
                <th>Producto</th>
                <th class=""text-right"">Precio</th>
                <th class=""text-right"">Subtotal</th>
                <th></th>
            </tr>
        </thead>
        <tbody>
");
            EndContext();
#line 17 "C:\Users\Mich\Documents\tooVSCORE\Sistema_TOO\TiendaOnline.solucion\TiendaOnline\Views\Carrito\IndexC.cshtml"
             foreach (var linea in Model.Carrito.ProdCarrito)
            {

#line default
#line hidden
            BeginContext(581, 66, true);
            WriteLiteral("                <tr>\r\n                    <td class=\"text-center\">");
            EndContext();
            BeginContext(648, 14, false);
#line 20 "C:\Users\Mich\Documents\tooVSCORE\Sistema_TOO\TiendaOnline.solucion\TiendaOnline\Views\Carrito\IndexC.cshtml"
                                       Write(linea.Cantidad);

#line default
#line hidden
            EndContext();
            BeginContext(662, 49, true);
            WriteLiteral("</td>\r\n                    <td class=\"text-left\">");
            EndContext();
            BeginContext(712, 29, false);
#line 21 "C:\Users\Mich\Documents\tooVSCORE\Sistema_TOO\TiendaOnline.solucion\TiendaOnline\Views\Carrito\IndexC.cshtml"
                                     Write(linea.Producto.NombreProducto);

#line default
#line hidden
            EndContext();
            BeginContext(741, 50, true);
            WriteLiteral("</td>\r\n                    <td class=\"text-right\">");
            EndContext();
            BeginContext(792, 43, false);
#line 22 "C:\Users\Mich\Documents\tooVSCORE\Sistema_TOO\TiendaOnline.solucion\TiendaOnline\Views\Carrito\IndexC.cshtml"
                                      Write(linea.Producto.Preciounitario.ToString("c"));

#line default
#line hidden
            EndContext();
            BeginContext(835, 76, true);
            WriteLiteral("</td>\r\n                    <td class=\"text-right\">\r\n                        ");
            EndContext();
            BeginContext(913, 62, false);
#line 24 "C:\Users\Mich\Documents\tooVSCORE\Sistema_TOO\TiendaOnline.solucion\TiendaOnline\Views\Carrito\IndexC.cshtml"
                    Write((linea.Cantidad * linea.Producto.Preciounitario).ToString("c"));

#line default
#line hidden
            EndContext();
            BeginContext(976, 73, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td class=\"text-center\">");
            EndContext();
            BeginContext(1049, 164, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "342b9b572af34b94908b947e6d8a35cb", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-drinkId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 26 "C:\Users\Mich\Documents\tooVSCORE\Sistema_TOO\TiendaOnline.solucion\TiendaOnline\Views\Carrito\IndexC.cshtml"
                                                                                                                                                                    WriteLiteral(linea.Producto.IdProducto);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["drinkId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-drinkId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["drinkId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1213, 30, true);
            WriteLiteral("</td>\r\n                </tr>\r\n");
            EndContext();
#line 28 "C:\Users\Mich\Documents\tooVSCORE\Sistema_TOO\TiendaOnline.solucion\TiendaOnline\Views\Carrito\IndexC.cshtml"
            }

#line default
#line hidden
            BeginContext(1258, 178, true);
            WriteLiteral("        </tbody>\r\n        <tfoot>\r\n            <tr>\r\n                <td colspan=\"3\" class=\"text-right\">Total:</td>\r\n                <td class=\"text-right\">\r\n                    ");
            EndContext();
            BeginContext(1437, 32, false);
#line 34 "C:\Users\Mich\Documents\tooVSCORE\Sistema_TOO\TiendaOnline.solucion\TiendaOnline\Views\Carrito\IndexC.cshtml"
               Write(Model.TotalCarrito.ToString("c"));

#line default
#line hidden
            EndContext();
            BeginContext(1469, 84, true);
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n        </tfoot>\r\n    </table>\r\n\r\n</div>");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CarritoVM> Html { get; private set; }
    }
}
#pragma warning restore 1591
