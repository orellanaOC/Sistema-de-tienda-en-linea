#pragma checksum "C:\Users\Mich\Documents\tooVSCORE\Sistema_TOO\TiendaOnline.solucion\TiendaOnline\Views\Shared\Components\CarritoComponent\Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a95a6e1f98a098af45155be5256d539592f42c28"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_CarritoComponent_Default), @"mvc.1.0.view", @"/Views/Shared/Components/CarritoComponent/Default.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/Components/CarritoComponent/Default.cshtml", typeof(AspNetCore.Views_Shared_Components_CarritoComponent_Default))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a95a6e1f98a098af45155be5256d539592f42c28", @"/Views/Shared/Components/CarritoComponent/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6e93f7432df440ab1b28935e4dfa14ed4c983176", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_CarritoComponent_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CarritoVM>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Carrito", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(18, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\Mich\Documents\tooVSCORE\Sistema_TOO\TiendaOnline.solucion\TiendaOnline\Views\Shared\Components\CarritoComponent\Default.cshtml"
 if (Model.Carrito.ProdCarrito.Count > 0) 
{

#line default
#line hidden
            BeginContext(67, 18, true);
            WriteLiteral("    <li>\r\n        ");
            EndContext();
            BeginContext(85, 222, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "63e417a02a3c4add91ff532897d09e5c", async() => {
                BeginContext(113, 127, true);
                WriteLiteral("\r\n            <span class=\"glyphicon glyphicon-shopping-cart\"></span>\r\n            <span id=\"carrito-status\">\r\n                ");
                EndContext();
                BeginContext(241, 31, false);
#line 9 "C:\Users\Mich\Documents\tooVSCORE\Sistema_TOO\TiendaOnline.solucion\TiendaOnline\Views\Shared\Components\CarritoComponent\Default.cshtml"
           Write(Model.Carrito.ProdCarrito.Count);

#line default
#line hidden
                EndContext();
                BeginContext(272, 31, true);
                WriteLiteral("\r\n            </span>\r\n        ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(307, 13, true);
            WriteLiteral("\r\n    </li>\r\n");
            EndContext();
#line 13 "C:\Users\Mich\Documents\tooVSCORE\Sistema_TOO\TiendaOnline.solucion\TiendaOnline\Views\Shared\Components\CarritoComponent\Default.cshtml"
}

#line default
#line hidden
            BeginContext(323, 2, true);
            WriteLiteral("\r\n");
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
