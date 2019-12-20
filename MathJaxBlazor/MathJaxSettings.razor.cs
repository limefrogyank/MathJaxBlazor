using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MathJaxBlazor
{
    public partial class MathJaxSettings
    {
        private bool hasRendered;

        [Inject] private IJSRuntime jsRuntime { get; set; }

        [Parameter] public TexInputSettings Tex { get; set; } = new TexInputSettings();

        protected override async Task OnParametersSetAsync()
        {
            if (hasRendered)
            {
                await ApplySettingsAsync();
            }

            await base.OnParametersSetAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await ApplySettingsAsync();
                hasRendered = true;
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task ApplySettingsAsync()
        {
            await jsRuntime.InvokeVoidAsync("window.mathJaxBlazor.applySettings", Tex);
        }
    }
}
