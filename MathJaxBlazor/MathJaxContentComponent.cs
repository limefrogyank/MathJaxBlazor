using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MathJaxBlazor
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1063:Implement IDisposable Correctly", Justification = "<Pending>")]
    public class MathJaxContentComponent : ComponentBase, IDisposable
    {
        [Inject] private IJSRuntime jsRuntime { get; set; }
        
        public void Dispose()
        {
            jsRuntime.InvokeVoidAsync("window.mathJaxBlazor.typesetClear");
        }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            jsRuntime.InvokeVoidAsync("window.mathJaxBlazor.typesetPromise");
            return base.OnAfterRenderAsync(firstRender);
        }
    }
}
