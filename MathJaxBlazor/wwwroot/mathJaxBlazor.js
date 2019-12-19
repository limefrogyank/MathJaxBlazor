window.MathJax = {
    startup: {
        pageReady: () => {
            console.log("MathJax is loaded and initialized.");
        }
    }
};

window.mathJaxBlazor = {

    typesetPromise: function () {
        MathJax.startup.document.state(0);
        MathJax.texReset();
        MathJax.typesetClear();
        MathJax.startup.document.clear();
        MathJax.typeset();
    },
    typesetClear: function () {
        try {
            MathJax.startup.document.state(0);
            MathJax.texReset();
            MathJax.typesetClear();
            MathJax.startup.document.clear();
        } catch (ex){
            console.log(ex);
        }
    },
    processMathML: async function (input) {
        return MathJax.mathml2chtmlPromise(input).then(function (node) {
            //
            //  The promise returns the typeset node, which we add to the output
            //  Then update the document to include the adjusted CSS for the
            //    content of the new equation.
            //
            MathJax.startup.document.clear();
            MathJax.startup.document.updateDocument();
            return node.outerHTML;
           
        }).catch(function (err) {
            //
            //  If there was an error, put the message into the output instead
            //
            return err.message;
        });
    },
    processLatex: async function (input) {
        return MathJax.tex2chtmlPromise(input).then(function (node) {
            //
            //  The promise returns the typeset node, which we add to the output
            //  Then update the document to include the adjusted CSS for the
            //    content of the new equation.
            //
            MathJax.startup.document.clear();
            MathJax.startup.document.updateDocument();
            return node.outerHTML;
            
        }).catch(function (err) {
            //
            //  If there was an error, put the message into the output instead
            //
            return err.message;
        });
    }
};
