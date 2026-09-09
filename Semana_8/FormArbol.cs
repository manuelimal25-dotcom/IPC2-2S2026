using System;
namespace Semana_8
{
    // Formulario para gestionar el Árbol Binario de Búsqueda de la Biblioteca Musical
    // Permite realizar operaciones: Insertar, Buscar por título y Ver todas las canciones
    public class FormArbol : Form
    {
        // Estructura de datos (recibida desde FormMenu, ya puede venir con canciones del JSON)
        private ArbolBinarioBusqueda arbolCanciones;

        // Controles del formulario
        private TextBox txtTitulo = null!;
        private TextBox txtArtista = null!;
        private TextBox txtGenero = null!;
        private TextBox txtDuracion = null!;
        private TextBox txtBuscar = null!;
        private Button btnInsertar = null!;
        private Button btnBuscar = null!;
        private Button btnVer = null!;
        private RichTextBox txtResultado = null!;
        private Label lblTitulo = null!;
        private Label lblArtista = null!;
        private Label lblGenero = null!;
        private Label lblDuracion = null!;
        private Label lblBuscar = null!;
        private Label lblResultado = null!;

        // Constructor: recibe el árbol ya construido desde el menú (puede tener canciones cargadas o estar vacío).
        public FormArbol(ArbolBinarioBusqueda arbol)
        {
            arbolCanciones = arbol;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Configuración del formulario
            this.Text = "Biblioteca Musical - Árbol Binario de Búsqueda";
            this.Size = new System.Drawing.Size(500, 620);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Label - Título de la canción
            lblTitulo = new Label();
            lblTitulo.Text = "Título:";
            lblTitulo.Location = new Point(30, 30);
            lblTitulo.AutoSize = true;
            this.Controls.Add(lblTitulo);

            // TextBox - Título de la canción
            txtTitulo = new TextBox();
            txtTitulo.Location = new Point(30, 55);
            txtTitulo.Size = new Size(420, 25);
            this.Controls.Add(txtTitulo);

            // Label - Artista de la canción
            lblArtista = new Label();
            lblArtista.Text = "Artista:";
            lblArtista.Location = new Point(30, 90);
            lblArtista.AutoSize = true;
            this.Controls.Add(lblArtista);

            // TextBox - Artista de la canción
            txtArtista = new TextBox();
            txtArtista.Location = new Point(30, 115);
            txtArtista.Size = new Size(420, 25);
            this.Controls.Add(txtArtista);

            // Label - Género de la canción
            lblGenero = new Label();
            lblGenero.Text = "Género:";
            lblGenero.Location = new Point(30, 150);
            lblGenero.AutoSize = true;
            this.Controls.Add(lblGenero);

            // TextBox - Género de la canción
            txtGenero = new TextBox();
            txtGenero.Location = new Point(30, 175);
            txtGenero.Size = new Size(420, 25);
            this.Controls.Add(txtGenero);

            // Label - Duración de la canción
            lblDuracion = new Label();
            lblDuracion.Text = "Duración (minutos):";
            lblDuracion.Location = new Point(30, 210);
            lblDuracion.AutoSize = true;
            this.Controls.Add(lblDuracion);

            // TextBox - Duración de la canción
            txtDuracion = new TextBox();
            txtDuracion.Location = new Point(30, 235);
            txtDuracion.Size = new Size(420, 25);
            this.Controls.Add(txtDuracion);

            // Botón INSERTAR (agrega la canción al árbol)
            btnInsertar = new Button();
            btnInsertar.Text = "INSERTAR";
            btnInsertar.Location = new Point(30, 280);
            btnInsertar.Size = new Size(130, 40);
            btnInsertar.BackColor = System.Drawing.Color.LightGreen;
            btnInsertar.Click += BtnInsertar_Click;
            this.Controls.Add(btnInsertar);

            // Botón VER (muestra todas las canciones del árbol en orden alfabético)
            btnVer = new Button();
            btnVer.Text = "VER TODAS";
            btnVer.Location = new Point(175, 280);
            btnVer.Size = new Size(130, 40);
            btnVer.BackColor = System.Drawing.Color.Khaki;
            btnVer.Click += BtnVer_Click;
            this.Controls.Add(btnVer);

            // Label - Buscar por título
            lblBuscar = new Label();
            lblBuscar.Text = "Buscar por título:";
            lblBuscar.Location = new Point(30, 340);
            lblBuscar.AutoSize = true;
            this.Controls.Add(lblBuscar);

            // TextBox - Título a buscar
            txtBuscar = new TextBox();
            txtBuscar.Location = new Point(30, 365);
            txtBuscar.Size = new Size(280, 25);
            this.Controls.Add(txtBuscar);

            // Botón BUSCAR (busca una canción por título dentro del árbol)
            btnBuscar = new Button();
            btnBuscar.Text = "BUSCAR";
            btnBuscar.Location = new Point(320, 363);
            btnBuscar.Size = new Size(130, 30);
            btnBuscar.BackColor = System.Drawing.Color.LightBlue;
            btnBuscar.Click += BtnBuscar_Click;
            this.Controls.Add(btnBuscar);

            // Label - Resultado
            lblResultado = new Label();
            lblResultado.Text = "Resultado:";
            lblResultado.Location = new Point(30, 410);
            lblResultado.AutoSize = true;
            this.Controls.Add(lblResultado);

            // RichTextBox - Mostrar resultado de la búsqueda o del listado completo
            txtResultado = new RichTextBox();
            txtResultado.Location = new Point(30, 435);
            txtResultado.Size = new Size(420, 90);
            txtResultado.ReadOnly = true;
            txtResultado.Font = new System.Drawing.Font("Consolas", 10);
            txtResultado.BackColor = System.Drawing.Color.White;
            txtResultado.BorderStyle = BorderStyle.FixedSingle;
            txtResultado.WordWrap = true;
            txtResultado.ScrollBars = RichTextBoxScrollBars.Vertical;
            this.Controls.Add(txtResultado);
        }

        // Evento INSERTAR: Agrega una nueva canción al árbol binario de búsqueda
        private void BtnInsertar_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitulo.Text) || string.IsNullOrWhiteSpace(txtArtista.Text) ||
                string.IsNullOrWhiteSpace(txtGenero.Text) || string.IsNullOrWhiteSpace(txtDuracion.Text))
            {
                MessageBox.Show("Por favor ingrese título, artista, género y duración.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtDuracion.Text, out int duracion))
            {
                MessageBox.Show("La duración debe ser un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Cancion nuevaCancion = new Cancion(txtTitulo.Text, txtArtista.Text, txtGenero.Text, duracion);
            arbolCanciones.Insertar(nuevaCancion);

            Console.WriteLine($"INSERTAR realizado: {nuevaCancion.Titulo}");

            txtTitulo.Clear();
            txtArtista.Clear();
            txtGenero.Clear();
            txtDuracion.Clear();
            txtTitulo.Focus();

            MessageBox.Show("Canción insertada en el árbol.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Evento BUSCAR: Busca una canción por título dentro del árbol binario
        private void BtnBuscar_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                MessageBox.Show("Por favor ingrese un título a buscar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Cancion? cancionEncontrada = arbolCanciones.Buscar(txtBuscar.Text);

            if (cancionEncontrada == null)
            {
                Console.WriteLine($"BUSCAR: no se encontró '{txtBuscar.Text}'.");
                txtResultado.Text = $"No se encontró ninguna canción con el título \"{txtBuscar.Text}\".";
                return;
            }

            Console.WriteLine($"BUSCAR realizado: {cancionEncontrada.Titulo}");

            txtResultado.Text = $"Título: {cancionEncontrada.Titulo}\nArtista: {cancionEncontrada.Artista}\nGénero: {cancionEncontrada.Genero}\nDuración: {cancionEncontrada.Duracion} min";
        }

        // Evento VER: Recorre el árbol en orden (inorden) y muestra todas las canciones alfabéticamente
        private void BtnVer_Click(object? sender, EventArgs e)
        {
            if (arbolCanciones.Raiz == null)
            {
                txtResultado.Text = "El árbol no tiene canciones.";
                return;
            }

            System.Text.StringBuilder resultado = new System.Text.StringBuilder();
            int contador = 0;
            RecorrerInorden(arbolCanciones.Raiz, resultado, ref contador);

            Console.WriteLine($"VER TODAS realizado: {contador} canciones.");

            txtResultado.Text = resultado.ToString();
        }

        // Recorre el árbol de izquierda a raíz a derecha (inorden), lo que da el orden alfabético por título.
        // Se recorre desde afuera de ArbolBinarioBusqueda usando la propiedad Raiz, sin modificar los nodos.
        private void RecorrerInorden(Nodo? nodo, System.Text.StringBuilder resultado, ref int contador)
        {
            if (nodo == null)
            {
                return;
            }

            RecorrerInorden(nodo.Izquierdo, resultado, ref contador);

            contador++;
            resultado.AppendLine($"{contador}. {nodo.Cancion.Titulo} - {nodo.Cancion.Artista} ({nodo.Cancion.Genero}, {nodo.Cancion.Duracion} min)");

            RecorrerInorden(nodo.Derecho, resultado, ref contador);
        }
    }
}