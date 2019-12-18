window.MathJax = {
    startup: {
        pageReady: () => {
            //console.log("MathJax is loaded, but defaultReady not called.");
            //window.MathJax.startup.getComponents();
            //window.MathJax.startup.makeMethods();
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
            MathJax.texReset();
            MathJax.typesetClear();
            MathJax.startup.document.state(0);

        } catch (ex){
            console.log(ex);
        }
    }
};
