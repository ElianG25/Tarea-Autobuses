using System;
using System.Windows.Forms;

namespace Capa_de_negocios
{
    public partial class Menu : Form
    {
        string TextForm = "";
        clases.SQLite _SQLite = new clases.SQLite();

        public Menu()
        {
            InitializeComponent();
        }

        //Mostrar hora
        private void MostrarHora_Tick(object sender, EventArgs e)
        {
            ObtenerLaHora();
        }

        //Al cargar
        private void Menu_Load(object sender, EventArgs e)
        {
            TextForm = Text;
            ObtenerLaHora();

            ActualizarLaInformacion();
        }

        //Funcion de obtener la hora.
        private void ObtenerLaHora()
        {
            Text = TextForm + " " + System.DateTime.Now.ToString("dd/MM/yyyy | hh:mm:ss - tt");
        }

        //Actualizar información
        private void ActualizarLaInformacion()
        {
            //Mostrando los choferes.
            _SQLite.CargarLaBaseDeDatos("Select * From TablaChoferes", ChoferesDataGrid);

            //Mostrando los autobuses.
            _SQLite.CargarLaBaseDeDatos("Select * From TablaAutobuses", VehiculosDataGrid);

            //Mostrando las rutas.
            _SQLite.CargarLaBaseDeDatos("Select * From TablaRutas", RutasDataGrid);

            //Mostrando las relaciones.
            _SQLite.CargarLaBaseDeDatos("Select * From TablaRelacion", RelacionesDataGrid);

            //Poniendo las relaciones en los DataGrid individuales.
            _SQLite.CargarLasRelaciones(
                "Select NOMBRE From TablaChoferes",
                ChoferComboBox,

                "Select MARCA From TablaAutobuses",
                ComboBoxVehiculo,

                "Select NOMBRE From TablaRutas",
                ComboBoxRuta);
        }

        //Guardar Ruta
        private void button3_Click(object sender, EventArgs e)
        {
            _SQLite.GuardarRuta(RutaTextbox);
            ActualizarLaInformacion();
        }

        //Guardar Vehiculo
        private void button2_Click(object sender, EventArgs e)
        {
            _SQLite.GuardarVehiculo(MarcaDelVehiculoTextbox, ModeloDelVehiculoTextbox, PlacaDelVehiculoTextbox, ColorDelVehiculoTextbox, AnoDelVehiculoTextboxXD);
            ActualizarLaInformacion();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _SQLite.GuardarChofer(NombreChoferTextBox, ApellidoChoferTextBox, FechaChoferTextBox, CedulaChoferTextBox);
            ActualizarLaInformacion();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ActualizarLaInformacion();
        }

        private void BOTONDERELACION_Click(object sender, EventArgs e)
        {
            _SQLite.GuardarRelacion(ChoferComboBox.Text, ComboBoxVehiculo.Text, ComboBoxRuta.Text);
            ActualizarLaInformacion();
        }

    }
}
