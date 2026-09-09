using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Semana_8
{
    // Formulario principal que funciona como menú
    // Permite cargar canciones desde un archivo JSON y abrir la Biblioteca Musical (Árbol Binario)
    public class FormMenu : Form
    {
        // Árbol compartido: se llena aquí con el JSON y se pasa al FormArbol para seguir insertando/buscando
        private ArbolBinarioBusqueda arbolCanciones;

        private Button btnCargarJson = null!;
        private Button btnArbol = null!;
        private Label lblTitulo = null!;


        public FormMenu()
        {
            arbolCanciones = new ArbolBinarioBusqueda();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Configuración del formulario
            this.Text = "Menú Principal - Biblioteca Musical";
            this.Size = new System.Drawing.Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Label de título
            lblTitulo = new Label();
            lblTitulo.Text = "Ejemplo: Árbol Binario de Búsqueda";
            lblTitulo.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
            lblTitulo.Location = new Point(50, 30);
            lblTitulo.Size = new Size(400, 30);
            lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Controls.Add(lblTitulo);

            // Botón CARGAR JSON (carga canciones desde un archivo y las inserta en el árbol compartido)
            btnCargarJson = new Button();
            btnCargarJson.Text = "CARGAR DESDE JSON";
            btnCargarJson.Location = new Point(75, 100);
            btnCargarJson.Size = new Size(350, 50);
            btnCargarJson.Font = new System.Drawing.Font("Arial", 10);
            btnCargarJson.BackColor = System.Drawing.Color.Khaki;
            btnCargarJson.Click += BtnCargarJson_Click;
            this.Controls.Add(btnCargarJson);

            // Botón para abrir formulario del Árbol Binario (Biblioteca Musical)
            btnArbol = new Button();
            btnArbol.Text = "Biblioteca Musical (Árbol Binario)";
            btnArbol.Location = new Point(75, 170);
            btnArbol.Size = new Size(350, 50);
            btnArbol.Font = new System.Drawing.Font("Arial", 10);
            btnArbol.BackColor = System.Drawing.Color.LightGreen;
            btnArbol.Click += BtnArbol_Click;
            this.Controls.Add(btnArbol);
        }

        // Evento CARGAR JSON: Abre un archivo JSON, lee las canciones y las inserta en el árbol compartido
        private void BtnCargarJson_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog dialogo = new OpenFileDialog();
            dialogo.Filter = "Archivos JSON (*.json)|*.json";
            dialogo.Title = "Seleccionar archivo de canciones";

            // Si el usuario cancela el diálogo, no se hace nada más.
            if (dialogo.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                string contenido = File.ReadAllText(dialogo.FileName);

                JsonSerializerOptions opciones = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                List<CancionJson>? cancionesJson = JsonSerializer.Deserialize<List<CancionJson>>(contenido, opciones);

                if (cancionesJson == null || cancionesJson.Count == 0)
                {
                    MessageBox.Show("El archivo no contiene canciones.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Se convierte cada elemento leído a Cancion y se inserta en el árbol compartido.
                int cantidadInsertada = 0;
                foreach (CancionJson item in cancionesJson)
                {
                    Cancion cancion = new Cancion(item.Titulo, item.Artista, item.Genero, item.Duracion);
                    arbolCanciones.Insertar(cancion);
                    cantidadInsertada++;
                }

                Console.WriteLine($"CARGAR JSON realizado: {cantidadInsertada} canciones insertadas.");

                MessageBox.Show($"Se insertaron {cantidadInsertada} canciones en el árbol.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (JsonException)
            {
                MessageBox.Show("El archivo no tiene un formato JSON válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al leer el archivo:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento: Abrir formulario del Árbol Binario, pasando el árbol ya cargado desde el JSON
        private void BtnArbol_Click(object? sender, EventArgs e)
        {
            FormArbol formArbol = new FormArbol(arbolCanciones);
            formArbol.ShowDialog(); // Abre como modal
        }
    }
}