using Npgsql;

namespace App_pokemon_Crud_Forms.Tablas
{
    public partial class Tabla6 : Form
    {
        static string connectionString;
        static int idUser;
        public Tabla6(int id, string connection)
        {
            InitializeComponent();
            idUser = id;
            connectionString = connection;
        }

        private void LoadData(string connectionString)
        {
            string query = "SELECT * FROM \"public\".\"Objeto\" WHERE \"Status\" = 1";

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

        private void Tabla6_Load(object sender, EventArgs e)
        {
            LoadData(connectionString);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int idCreationUser = idUser; // Suponiendo que obtienes esto desde el login o sesión

                // Consulta para insertar un nuevo objeto
                string query = "INSERT INTO \"public\".\"Objeto\" (\"Nombre\", \"Región_ID\", \"Gimnasio_ID\", \"Descripción\", \"IdCreationUser\") " +
                               "VALUES (@nombre, @regionId, @gimnasioId, @descripcion, @idCreationUser)";

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        // Agregar parámetros
                        command.Parameters.AddWithValue("@nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@regionId", string.IsNullOrEmpty(txtRegion.Text) ? (object)DBNull.Value : Convert.ToInt32(txtRegion.Text));
                        command.Parameters.AddWithValue("@gimnasioId", string.IsNullOrEmpty(txtGimnasio.Text) ? (object)DBNull.Value : Convert.ToInt32(txtGimnasio.Text));
                        command.Parameters.AddWithValue("@descripcion", txtDescripcion.Text);
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
                int idEditUser = idUser; // Suponiendo que obtienes esto desde el login o sesión
                int idObjeto = Convert.ToInt32(txtIdObjeto.Text); // ID del objeto a actualizar

                // Consulta para actualizar un objeto
                string query = "UPDATE \"public\".\"Objeto\" SET \"Nombre\" = @nombre, \"Región_ID\" = @regionId, \"Gimnasio_ID\" = @gimnasioId, " +
                               "\"Descripción\" = @descripcion, \"IdEditUser\" = @idEditUser WHERE \"ID_Objeto\" = @idObjeto";

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        // Agregar parámetros
                        command.Parameters.AddWithValue("@nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@regionId", string.IsNullOrEmpty(txtRegion.Text) ? (object)DBNull.Value : Convert.ToInt32(txtRegion.Text));
                        command.Parameters.AddWithValue("@gimnasioId", string.IsNullOrEmpty(txtGimnasio.Text) ? (object)DBNull.Value : Convert.ToInt32(txtGimnasio.Text));
                        command.Parameters.AddWithValue("@descripcion", txtDescripcion.Text);
                        command.Parameters.AddWithValue("@idEditUser", idEditUser);
                        command.Parameters.AddWithValue("@idObjeto", idObjeto);

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
                int idObjeto = Convert.ToInt32(txtIdObjeto.Text); // ID del objeto a eliminar

                // Consulta para eliminar un objeto (cambiar estado a inactivo)
                string query = "UPDATE \"public\".\"Objeto\" SET \"Status\" = 0 WHERE \"ID_Objeto\" = @idObjeto";

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        // Agregar parámetro
                        command.Parameters.AddWithValue("@idObjeto", idObjeto);

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

    }
}
