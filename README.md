# MathJaxBlazor
Blazor components that use MathJax

## READ THIS
MathJax changes HTML.  Blazor does NOT like this.  As such, there are limitations to using this library.
1.  You cannot write plain MathML code into your HTML documents and expect MathJax to render it without problems.  MathJax replaces `<math>` with its own tags and thoroughly confuses Blazor. When you try to change state later, everything will die.
2.  You *can* write Latex using the standard MathJax tags `\(` and `)\`.  Since this is plain text, Blazor doesn't expect to have to parse a document tree later.
3.  Wait until `Equation` components are ready if you must use MathML.
