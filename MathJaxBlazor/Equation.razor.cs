using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MathJaxBlazor
{
    public sealed partial class Equation : IDisposable
    {
        [Inject] private IJSRuntime jsRuntime { get; set; }


        [Parameter] public EventCallback<string> OutputChanged { get; set; }
        //[Parameter] public bool ShowRawOutput { get; set; } = false;  //for debug, no need for templated component
        [Parameter] public RenderFragment<string> Template { get; set; }
        [Parameter] public string Value { get; set; }
        [Parameter] public EventCallback<string> ValueChanged { get; set; }

        public string Output { get; private set; }

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
                await ProcessValueAsync();
                hasRendered = true;
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        public void Dispose()
        {
            
        }

        private async Task ProcessValueAsync()
        {
            string result;
            if (Value.StartsWith("<math"))
            {
                result = await jsRuntime.InvokeAsync<string>("window.mathJaxBlazor.processMathML", Value);
            }
            else
            {
                result = await jsRuntime.InvokeAsync<string>("window.mathJaxBlazor.processLatex", Value);
            }
            Output = result;
            await OutputChanged.InvokeAsync(result);
            StateHasChanged();
        }
    }
}
