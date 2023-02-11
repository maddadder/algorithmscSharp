export async function clearDot(element) {
    
    d3.select(element).html("");
}
export async function renderDot(model, element) {
    
    var graphviz = d3.select(element)
        .graphviz();
    
    graphviz.renderDot(model);
}
