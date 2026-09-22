const archivo = document.getElementById("archivo");
const etiquetaArchivo = document.getElementById("etiquetaArchivo");
const formulario = document.getElementById("formXml");
const resultado = document.getElementById("resultadoXml");
const detalle = document.getElementById("detalleXml");

archivo.addEventListener("change", function (evento) {
    const archivoSeleccionado = evento.target.files[0];
    etiquetaArchivo.textContent = archivoSeleccionado
        ? archivoSeleccionado.name
        : "Seleccionar archivo XML";
});

formulario.addEventListener("submit", function (evento) {
    evento.preventDefault();

    const archivoSeleccionado = archivo.files[0];
    if (!archivoSeleccionado) {
        return;
    }

    detalle.textContent = `${archivoSeleccionado.name} (${archivoSeleccionado.size} bytes)`;
    resultado.hidden = false;
});