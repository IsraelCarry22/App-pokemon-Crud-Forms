﻿using Npgsql;

namespace App_pokemon_Crud_Forms.Tablas
{
    public partial class Tabla1 : Form
    {
        static string connectionString;
        static int idUser;
        public Tabla1(int id, string connection)
        {
            InitializeComponent();
            idUser = id;
            connectionString = connection;
        }

        private void LoadData(string connectionString)
        {
            string query = "SELECT * FROM \"public\".\"Entrenador\" WHERE \"Status\" = 1";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            // Configura el ListView para mostrar detalles
                            listView1.View = View.Details;

                            // Limpia columnas y elementos existentes para evitar duplicados
                            listView1.Columns.Clear();
                            listView1.Items.Clear();

                            // Añade las columnas dinámicamente basadas en los nombres de los campos del lector
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                // Puedes ajustar el ancho de las columnas según tus necesidades
                                listView1.Columns.Add(reader.GetName(i), 150, HorizontalAlignment.Left);
                            }

                            // Llena el ListView con los datos obtenidos
                            while (reader.Read())
                            {
                                // Crea un nuevo ítem para el ListView
                                ListViewItem item = new ListViewItem(reader.IsDBNull(0) ? string.Empty : reader.GetValue(0).ToString());

                                // Añade los subítems para cada columna adicional
                                for (int i = 1; i < reader.FieldCount; i++)
                                {
                                    string subItem = reader.IsDBNull(i) ? string.Empty : reader.GetValue(i).ToString();
                                    item.SubItems.Add(subItem);
                                }

                                // Añade el ítem completo al ListView
                                listView1.Items.Add(item);
                            }

                            // Opcional: Ajusta automáticamente el ancho de las columnas para que se ajusten al contenido
                            foreach (ColumnHeader column in listView1.Columns)
                            {
                                column.Width = -2; // -2 ajusta el ancho al contenido y al encabezado
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Muestra un mensaje de error más detallado si ocurre una excepción
                    MessageBox.Show($"Ocurrió un error al cargar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int idCreationUser = idUser; // Suponiendo que el idCreationUser lo obtienes de algún lugar, como un login

                // Consulta para insertar un nuevo entrenador
                string query = "INSERT INTO \"public\".\"Entrenador\" (\"Nombre\", \"ApellIdoPaterno\", \"ApellIdoMaterno\", \"Edad\", \"Ciudad_ID\", \"Medalla_ID\", \"Mochila_ID\", \"IdCreationUser\") " +
                               "VALUES (@nombre, @apellidoPaterno, @apellidoMaterno, @edad, @ciudadId, @medallaId, @mochilaId, @idCreationUser)";

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        // Agregar parámetros
                        command.Parameters.AddWithValue("@nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@apellidoPaterno", txtApellidoPaterno.Text);
                        command.Parameters.AddWithValue("@apellidoMaterno", txtApellidoMaterno.Text);
                        command.Parameters.AddWithValue("@edad", Convert.ToInt32(txtEdad.Text)); 
                        command.Parameters.AddWithValue("@ciudadId", string.IsNullOrEmpty(txtCiudad.Text) ? (object)DBNull.Value : Convert.ToInt32(txtCiudad.Text));
                        command.Parameters.AddWithValue("@medallaId", string.IsNullOrEmpty(txtMedalla.Text) ? (object)DBNull.Value : Convert.ToInt32(txtMedalla.Text));
                        command.Parameters.AddWithValue("@mochilaId", string.IsNullOrEmpty(txtMochila.Text) ? (object)DBNull.Value : Convert.ToInt32(txtMochila.Text));
                        command.Parameters.AddWithValue("@idCreationUser", idCreationUser);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Los datos fueron insertados correctamente.");
                            LoadData(connectionString);
                        }
                        else
                        {
                            MessageBox.Show("Error al insertar datos (datos vacíos o inválidos).");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int idEditUser = idUser; // Suponiendo que el idEditUser lo obtienes de algún lugar, como un login
                int idEntrenador = Convert.ToInt32(txtIdEntrenador.Text); // Id del entrenador a editar

                // Consulta para actualizar un entrenador
                string query = "UPDATE \"public\".\"Entrenador\" SET \"Nombre\" = @nombre, \"ApellidoPaterno\" = @apellidoPaterno, \"ApellidoMaterno\" = @apellidoMaterno, " +
                               "\"Edad\" = @edad, \"Ciudad_ID\" = @ciudadId, \"Medalla_ID\" = @medallaId, \"Mochila_ID\" = @mochilaId, \"IdEditUser\" = @idEditUser " +
                               "WHERE \"ID_Entrenador\" = @idEntrenador";

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        // Agregar parámetros
                        command.Parameters.AddWithValue("@nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@apellidoPaterno", txtApellidoPaterno.Text);
                        command.Parameters.AddWithValue("@apellidoMaterno", txtApellidoMaterno.Text);
                        command.Parameters.AddWithValue("@edad", Convert.ToInt32(txtEdad.Text));
                        command.Parameters.AddWithValue("@ciudadId", string.IsNullOrEmpty(txtCiudad.Text) ? (object)DBNull.Value : Convert.ToInt32(txtCiudad.Text));
                        command.Parameters.AddWithValue("@medallaId", string.IsNullOrEmpty(txtMedalla.Text) ? (object)DBNull.Value : Convert.ToInt32(txtMedalla.Text));
                        command.Parameters.AddWithValue("@mochilaId", string.IsNullOrEmpty(txtMochila.Text) ? (object)DBNull.Value : Convert.ToInt32(txtMochila.Text));
                        command.Parameters.AddWithValue("@idEditUser", idEditUser);
                        command.Parameters.AddWithValue("@idEntrenador", idEntrenador);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Los datos fueron modificados correctamente.");
                            LoadData(connectionString);
                        }
                        else
                        {
                            MessageBox.Show("Error al modificar los datos (datos vacíos o inválidos).");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int idEntrenador = Convert.ToInt32(txtIdEntrenador.Text); // Id del entrenador a eliminar

                // Consulta para eliminar un entrenador
                string query = "UPDATE \"public\".\"Entrenador\" SET \"Status\" = 0 WHERE \"ID_Entrenador\" = @idEntrenador";

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        // Agregar parámetro
                        command.Parameters.AddWithValue("@idEntrenador", idEntrenador);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("El registro fue eliminado correctamente.");
                            LoadData(connectionString);
                        }
                        else
                        {
                            MessageBox.Show("Error al eliminar el registro.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Tabla1_Load(object sender, EventArgs e)
        {
            LoadData(connectionString);
        }
    }
}