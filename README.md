# MathJaxBlazor
Blazor components that use MathJax

## WARNING!
MathJax changes the DOM.  **Blazor does NOT like this.**  As such, there are limitations to using this library.

## Demo (WASM)
https://limefrogyank.github.io/MathJaxBlazor/MathJaxBlazor.DemoWasm/dist/

## To use
Requires MathJax v3. Add the following to your html doc header:
```
<script type="text/javascript"
        src="_content/MathJaxBlazor/mathJaxBlazor.js">
</script>
<script type="text/javascript" id="MathJax-script" async
        src="https://cdn.jsdelivr.net/npm/mathjax@3/es5/tex-mml-chtml.js">
</script>
```

## Updates
- v1.0.3 - Fixed `Equation` so that null `Value` does not crash Blazor.
- v1.0.2 - Added `ChildContent` to `MathContentComponent` so you can stick it anywhere on a page.

## Components
### MathJaxContentComponent Component
This component can be used as a base class for a page or by itself.  Anything wrapped with this component will have the text parsed for MathJax-parsable content.  By default, MathJax will parse equations surrounded by `\( ... \)`, `\[ ... \]`, and `$$ ... $$`.  If you want to change the parsing markers, see the `MaxJaxSettings` component below.

*Limitations*
- You can ONLY parse LaTeX type equations with this component.  It will currently NOT work with MathML as MathJax is doing something extra when processing MathML that I haven't figured out yet.
- It's also a bit clunky and might have performance issues.  For every state change in Blazor, we **must** revert to the original text or else Blazor will either get rid of the html completely or just crash.  This is just how Blazor works.  

```
@page "/"
@inherits MathJaxContentComponent

$$ a/c=1000 $$

\( a + b = c \)

\[   
\frac{d}{dx}\left( \int_{0}^{x} f(u)\,du\right)=f(x).    
\]
```

### Equation Component
This one works just fine in Blazor because you're not adding anything directly to the DOM.  Instead, you place your LaTeX or MathML code in the `Equation`'s `Value` property and the component will render it as a `MarkupString`.  *As per Microsoft guidance, using `MarkupString` is a security risk if somehow the MathJax output is intercepted.*

To output html directly:
```
<Equation Value="\frac{d}{dx}\left( \int_{0}^{x} f(u)\,du\right)=f(x)" />
```
You can also use `Equation` with the `Template` property.  This will allow you to customize the output yourself.  Instead of `MarkupString`, it will only output the string representation of the html so you'll have to handle the conversion yourself.
```
<Equation Value="\frac{d}{dx}\left( \int_{0}^{x} f(u)\,du\right)=f(x)" >
  <Template>
    <SomeComponent SomeProperty="Context" />
  </Template>
</Equation>
```

### MathJaxSettings Component
To modify the TeX input settings, just stick a `MathJaxSettings` anywhere in your app (preferrably somewhere that won't get updated much).  There is a `TexInputSettings` class with default values that match MathJax's default values.  Create a new class and adjust the settings however you'd like.  The demo has an example where the delimiters for Tex have an added single dollar sign `$..$` delimiter for inlineMath.

- currently only `InlineMath` and `DisplayMath` properties work

```
<MathJaxSettings Tex="texSettings"/>
<Router>...</Router>

@code{

    TexInputSettings texSettings = new TexInputSettings();

    protected override Task OnInitializedAsync()
    {
        texSettings.InlineMath.Add(new string[] { "$", "$" });
        return base.OnInitializedAsync();
    }

}

```
