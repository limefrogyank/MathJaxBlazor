using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MathJaxBlazor
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1063:Implement IDisposable Correctly", Justification = "<Pending>")]
    public class MathJaxContentComponent : ComponentBase, IAsyncDisposable
    {
        private IJSObjectReference? module;

        [Inject] private IJSRuntime jsRuntime { get; set; } = null!;

        [Parameter] public RenderFragment ChildContent { get; set; } = null!;

       
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder?.AddContent(0, ChildContent);
            //base.BuildRenderTree(builder);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                module = await jsRuntime!.InvokeAsync<IJSObjectReference>("import", "./_content/MathJaxBlazor/mathJaxBlazor.js");
            }
            if (module != null)
            {
                await module.InvokeVoidAsync("typesetPromise");
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        public async ValueTask DisposeAsync()
        {
            if (module != null)
            {
                await module.InvokeVoidAsync("typesetClear");
                await module.DisposeAsync();
            }
        }
        

    }
}
