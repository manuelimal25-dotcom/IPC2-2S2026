// Espera a que el input de archivo cambie, es decir, a que el usuario elija un archivo
document.getElementById("archivo").addEventListener("change", function (evento) {
    // Toma el primer archivo seleccionado, si existe alguno
    const archivoSeleccionado = evento.target.files[0];

    // Toma la etiqueta que se muestra visualmente en lugar del input
    const etiqueta = document.getElementById("etiquetaArchivo");

    // Si hay un archivo, muestra su nombre; si no, vuelve al texto original
    if (archivoSeleccionado) {
        etiqueta.textContent = archivoSeleccionado.name;
    } else {
        etiqueta.textContent = "Seleccionar archivo XML";
    }
});