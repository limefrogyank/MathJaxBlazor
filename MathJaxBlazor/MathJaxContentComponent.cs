using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MathJaxBlazor
{
    public class MathJaxContentComponent : ComponentBase, IDisposable
    {
        [CascadingParameter] public MathJaxSettings MathJaxSettings { get; set; }

        public void Dispose()
        {
            MathJaxSettings.TypesetClear();
        }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            MathJaxSettings.Typeset();
            return base.OnAfterRenderAsync(firstRender);
        }
    }
}
