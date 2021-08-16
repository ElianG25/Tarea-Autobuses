using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;


namespace Capa_de_negocios.clases
{
    class SQLite
    {
        SQLiteConnection conexion;

        public void AbrirLaConexion()
        {
            conexion = new SQLiteConnection("Data Source= " + Application.StartupPath + "\\BaseDeDatos.db;Version=3");
            conexion.Open();
        }

        public void CerrarLaConexion()
        {
            conexion.Close();
            //MessageBox.Show("La base de datos ha sido desconectada");
        }

        public void CargarLaBaseDeDatos(string Query, DataGridView _object)
        {
            AbrirLaConexion();

            try
            {
                DataTable dataTable = new DataTable();
                SQLiteDataAdapter Adapter = new SQLiteDataAdapter(Query, conexion);

                Adapter.Fill(dataTable);

                _object.DataSource = dataTable;
            }
            catch (Exception VariableError)
            {
                MessageBox.Show("Ocurrio un error: " + VariableError.Message);
            }

            CerrarLaConexion();
        }

        public void CargarLasRelaciones(string QueryChofer, ComboBox _choferCombo, string QueryVehiculo, ComboBox _vehiculoCombo, string QueryRuta, ComboBox _rutaCombo)
        {
            AbrirLaConexion();

            //Limpiar ComboBox para evitar errores
            _choferCombo.Items.Clear();
            _rutaCombo.Items.Clear();
            _vehiculoCombo.Items.Clear();

            //Nombre Chofer
            try
            {
                DataTable dataTable = new DataTable();
                SQLiteDataAdapter Adapter = new SQLiteDataAdapter(QueryChofer, conexion);

                Adapter.Fill(dataTable);

                for (int i = 0; i < dataTable.Rows.Count; i++)
                    _choferCombo.Items.Add(dataTable.Rows[i][0]);
            }
            catch (Exception VariableError)
            {
                MessageBox.Show("Ocurrio un error: " + VariableError.Message);
            }

            //Nombre Vehiculo
            try
            {
                DataTable dataTable = new DataTable();
                SQLiteDataAdapter Adapter = new SQLiteDataAdapter(QueryVehiculo, conexion);

                Adapter.Fill(dataTable);

                for (int i = 0; i < dataTable.Rows.Count; i++)
                    _vehiculoCombo.Items.Add(dataTable.Rows[i][0]);
            }
            catch (Exception VariableError)
            {
                MessageBox.Show("Ocurrio un error: " + VariableError.Message);
            }

            //Nombre Ruta
            try
            {
                DataTable dataTable = new DataTable();
                SQLiteDataAdapter Adapter = new SQLiteDataAdapter(QueryRuta, conexion);

                Adapter.Fill(dataTable);

                for (int i = 0; i < dataTable.Rows.Count; i++)
                    _rutaCombo.Items.Add(dataTable.Rows[i][0]);
            }
            catch (Exception VariableError)
            {
                MessageBox.Show("Ocurrio un error: " + VariableError.Message);
            }

            CerrarLaConexion();
        }

        public void GuardarRuta(TextBox rutaTextBox)
        {
            if (rutaTextBox.Text == string.Empty || rutaTextBox.Text == null)
            {
                MessageBox.Show("El campo no puede estar en blanco");
                return;
            }

            AbrirLaConexion();

            string Query = "Insert Into TablaRutas " +
                "(" +
                "NOMBRE" +
                ")" +

                "Values('" + rutaTextBox.Text + "')";

            SQLiteCommand sQLiteCommand = new SQLiteCommand(Query, conexion);
            sQLiteCommand.ExecuteNonQuery();

            MessageBox.Show("La ruta se ha guardado con exito");
            rutaTextBox.Text = string.Empty;

            CerrarLaConexion();
        }

        public void GuardarVehiculo(TextBox _marca, TextBox _modelo, TextBox _placa, TextBox _color, TextBox _anoXD)
        {
            AbrirLaConexion();

            string Query = "Insert Into TablaAutobuses" +
                "(MARCA, MODELO, PLACA, COLOR, ANO)" +

                "Values " +
                "(" +
                //Marca
                "'" + _marca.Text + "', " +

                //Modelo
                "'" + _modelo.Text + "', " +

                //Placa
                "'" + _placa.Text + "', " +

                //Color
                "'" + _color.Text + "', " +

                //Año
                "'" + _anoXD.Text + "')";

            SQLiteCommand sQLiteCommand = new SQLiteCommand(Query, conexion);
            sQLiteCommand.ExecuteNonQuery();

            MessageBox.Show("Datos guardados con exito");
            _marca.Text = "";
            _modelo.Text = "";
            _color.Text = "";
            _placa.Text = "";
            _anoXD.Text = "";

            CerrarLaConexion();
        }

        public void GuardarChofer(TextBox _nombre, TextBox _apellidos, TextBox _fecha, TextBox _cedula)
        {
            AbrirLaConexion();

            string Query = "Insert Into TablaChoferes" +
                "(NOMBRE, APELLIDOS, FECHANAC, CEDULA)" +
                "Values (" +

                //Nombre
                "'" + _nombre.Text + "', " +

                //Apellidos
                "'" + _apellidos.Text + "', " +

                //Fecha
                "'" + _fecha.Text + "', " +

                //Cedula
                "'" + _cedula.Text + "')";

            SQLiteCommand sQLiteCommand = new SQLiteCommand(Query, conexion);
            sQLiteCommand.ExecuteNonQuery();

            MessageBox.Show("Los datos se han guardado correctamente");
            _nombre.Text = "";
            _apellidos.Text = "";
            _fecha.Text = "";
            _cedula.Text = "";
        }

        public void GuardarRelacion(string _nombreChofer, string _nombreVehiculo, string _nombreRuta)
        {
            AbrirLaConexion();

            try
            {
                string Query = "Insert Into TablaRelacion" +
                    "(NOMBREDELCHOFER, NOMBREDELVEHICULO, NOMBREDELARUTA)" +

                    "Values" +
                    "('" + _nombreChofer + "'," +
                    "'" + _nombreVehiculo + "'," +
                    "'" + _nombreRuta + "')";

                SQLiteCommand sQLiteCommand = new SQLiteCommand(Query, conexion);
                sQLiteCommand.ExecuteNonQuery();

                MessageBox.Show("La relacion se ha guardado con exito");
            }
            catch
            {
                MessageBox.Show("No se pudo guardar la conexion.\nEl chofer acutal ya tiene otra ruta.");
            }

            CerrarLaConexion();
        }
    }
}
