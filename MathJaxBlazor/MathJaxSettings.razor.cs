using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathJaxBlazor
{
    public partial class MathJaxSettings
    {
        [Inject] private IJSRuntime jsRuntime { get; set; }

        //[Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public string Tex { get; set; }

        //public void Typeset()
        //{
        //    jsRuntime.InvokeVoidAsync("window.mathJaxBlazor.typesetPromise");
        //}

        //public void TypesetClear()
        //{
        //    jsRuntime.InvokeVoidAsync("window.mathJaxBlazor.typesetClear");
        //}
    }
}
