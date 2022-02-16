interface IMathJax {
    typesetPromise: () => Promise<void>;
    texReset: () => void;
    typesetClear: () => void;
    startup: IMathJaxStartup;
    config: IMathJaxConfig;
    mathml2chtmlPromise: (input: string) => Promise<HTMLElement>;
    tex2chtmlPromise: (input: string, settings:any) => Promise<HTMLElement>;
}
interface IMathJaxConfig {
    tex: IMathJaxSetting;
}
interface IMathJaxSetting {
    inlineMath: string;
    displayMath: string;
}
interface IMathJaxStartup {
    document: IMathJaxDocument;
    getComponents: () => void;
}
interface IMathJaxDocument {
    clear: () => void;
    math: any;
    getMathItemsWithin: (element: HTMLElement) => IMathJaxMathItem[];
    updateDocument: () => void;
}
interface IMathJaxMathItem {
    start: IMathJaxMathItem;
    end: IMathJaxMathItem;
    node: HTMLElement;
    delim: string;
    math: string;
}

let MathJax: IMathJax = (<any>window).MathJax;

let promise = new Promise<void>((resolved, rej) => { resolved();});

export function applySettings(texSettings) {
    MathJax.config.tex.inlineMath = texSettings.inlineMath;
    MathJax.config.tex.displayMath = texSettings.displayMath;
    //MathJax.startup.input[0].findTeX.options.processEscapes = texSettings.processEscapes;
    //MathJax.startup.input[0].findTeX.options.processEnvironments = texSettings.processEnvironments;
    //MathJax.startup.input[0].findTeX.options.processRefs = texSettings.processRefs;
    MathJax.startup.getComponents();
}

export function typesetPromise() {
    promise = promise.then(() => {
        typesetClear();
        return MathJax.typesetPromise();
    }).catch(err => {
        console.log(err);
    });
}

export function typesetClear() {
    try {
        undoTypeset();
        //MathJax.startup.document.state(0);
        MathJax.texReset();
        MathJax.typesetClear();
        MathJax.startup.document.clear();
    } catch (ex) {
        console.log(ex);
    }
}

export function undoTypeset() {
    let list = MathJax.startup.document.getMathItemsWithin(document.body);
    //var list = MathJax.startup.document.math.toArray();  // does not work anymore.
    for (let i = 0; i < list.length; i++) {
        list[i].start.node.outerHTML = list[i].start.delim + list[i].math + list[i].end.delim;
    }
}

export function processLatex(input:string, isDisplay:boolean) {
    return MathJax.tex2chtmlPromise(input, { display: isDisplay }).then((node) => {
        //
        //  The promise returns the typeset node, which we add to the output
        //  Then update the document to include the adjusted CSS for the
        //    content of the new equation.
        //
        MathJax.startup.document.clear();
        MathJax.startup.document.updateDocument();
        return node.outerHTML;
    }).catch(err => {
        return err.message;
    });
}

export function processMathML(input: string) {
    return MathJax.mathml2chtmlPromise(input).then((node) => {
        //
        //  The promise returns the typeset node, which we add to the output
        //  Then update the document to include the adjusted CSS for the
        //    content of the new equation.
        //
        MathJax.startup.document.clear();
        MathJax.startup.document.updateDocument();
        return node.outerHTML;
    }).catch(err => {
        return err.message;
    });
}