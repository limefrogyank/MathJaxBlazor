window.MathJax = {
    startup: {
        pageReady: () => {
            console.log("MathJax is loaded and initialized.");
        }
    }
};

window.mathJaxBlazor = {

    typesetPromise: function () {
        MathJax.typeset();
    },
    typesetClear: function () {
        try {
            MathJax.startup.document.state(0);
            MathJax.texReset();
            MathJax.typesetClear();
        } catch (ex){
            console.log(ex);
        }
    }
};
