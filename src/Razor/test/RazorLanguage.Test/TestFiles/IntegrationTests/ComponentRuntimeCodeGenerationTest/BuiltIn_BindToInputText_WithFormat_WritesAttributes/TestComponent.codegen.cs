// <auto-generated/>
#pragma warning disable 1591
namespace Test
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    public partial class TestComponent : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "input");
            __builder.AddAttribute(1, "type", "text");
            __builder.AddAttribute(2, "@bind", 
#nullable restore
#line 1 "x:\dir\subdir\Test\TestComponent.cshtml"
                           CurrentDate

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(3, "@bind:format", "MM/dd/yyyy");
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 2 "x:\dir\subdir\Test\TestComponent.cshtml"
       
    public DateTime CurrentDate { get; set; } = new DateTime(2018, 1, 1);

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
