using Npgsql;

namespace App_pokemon_Crud_Forms.Tablas
{
    public partial class Tabla5 : Form
    {
        static string connectionString;
        static int idUser;
        public Tabla5(int id, string connection)
        {
            InitializeComponent();
            idUser = id;
            connectionString = connection;
        }

        private void LoadData(string connectionString)
        {
            string query = "SELECT * FROM \"public\".\"Ciudad\" WHERE \"Status\" = 1";

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


        private void Tabla5_Load(object sender, EventArgs e)
        {
            LoadData(connectionString);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "INSERT INTO \"public\".\"ciudad\" (\"Nombre\", \"Región_ID\", \"Gimnasio_ID\", \"Descripción\", \"IdCreationUser\") VALUES (@nombre, @regionId, @gimnasioId, @descripcion, @idCreationUser)";

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@regionId", Convert.ToInt32(txtRegion.Text));
                        command.Parameters.AddWithValue("@gimnasioId", Convert.ToInt32(txtGimnasio.Text));
                        command.Parameters.AddWithValue("@descripcion", txtDescripcion.Text);
                        command.Parameters.AddWithValue("@idCreationUser", idUser);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Los datos fueron insertados correctamente.");
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
                string query = "UPDATE \"public\".\"ciudad\" SET \"Nombre\" = @nombre, \"Región_ID\" = @regionId, \"Gimnasio_ID\" = @gimnasioId, \"Descripción\" = @descripcion, \"IdEditUser\" = @idEditUser WHERE \"ID_Ciudad\" = @idCiudad";

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@regionId", Convert.ToInt32(txtRegion.Text));
                        command.Parameters.AddWithValue("@gimnasioId", Convert.ToInt32(txtGimnasio.Text));
                        command.Parameters.AddWithValue("@descripcion", txtDescripcion.Text);
                        command.Parameters.AddWithValue("@idEditUser", idUser);
                        command.Parameters.AddWithValue("@idCiudad", Convert.ToInt32(txtIdCiudad.Text));

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Los datos fueron modificados correctamente.");
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
                string query = "UPDATE \"public\".\"ciudad\" SET \"Status\" = 0 WHERE \"ID_Ciudad\" = @idCiudad";

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idCiudad", Convert.ToInt32(txtIdCiudad.Text));

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("El registro fue eliminado correctamente.");
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
    }
}
