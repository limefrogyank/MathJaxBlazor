using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MathJaxBlazor
{
    public sealed partial class Equation : IAsyncDisposable
    {
        private IJSObjectReference? module;
        [Inject] private IJSRuntime jsRuntime { get; set; } = null!;

        [Parameter] public bool TeXDisplay { get; set; } = true; // only works for TeX inputs
        [Parameter] public EventCallback<string> OutputChanged { get; set; }
        //[Parameter] public bool ShowRawOutput { get; set; } = false;  //for debug, no need for templated component
        [Parameter] public RenderFragment<string> Template { get; set; } = null!;
        [Parameter] public string? Value { get; set; }
        [Parameter] public EventCallback<string> ValueChanged { get; set; }

        public string Output { get; private set; } = String.Empty;

        private bool hasRendered = false;

        protected override async Task OnParametersSetAsync()
        {
            if (hasRendered)
            {
                await ProcessValueAsync();
            }
            await base.OnParametersSetAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                module = await jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/MathJaxBlazor/mathJaxBlazor.js");
                await ProcessValueAsync();
                hasRendered = true;
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task ProcessValueAsync()
        {
            string result = String.Empty;
            if (!string.IsNullOrWhiteSpace(Value))
            {
                if (module != null)
                {
                    if (Value.StartsWith("<math"))
                    {
                        result = await module.InvokeAsync<string>("processMathML", Value);
                    }
                    else
                    {
                        result = await module.InvokeAsync<string>("processLatex", Value, TeXDisplay);
                    }
                }
                Output = result;
                await OutputChanged.InvokeAsync(result);
                StateHasChanged();
            }
            else
            {
                Output = "";
                await OutputChanged.InvokeAsync("");
                StateHasChanged();
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (module != null)
            {
                await module.DisposeAsync();
            }
        }
    }
}
