﻿using Npgsql;

namespace App_pokemon_Crud_Forms.Tablas
{
    public partial class Tabla2 : Form
    {
        static string connectionString;
        static int idUser;
        public Tabla2(int id, string connection)
        {
            InitializeComponent();
            idUser = id;
            connectionString = connection;
        }

        private void LoadData(string connectionString)
        {
            string query = "SELECT * FROM \"public\".\"Pokémon\" WHERE \"Status\" = 1";

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

        private void Tabla2_Load(object sender, EventArgs e)
        {
            LoadData(connectionString);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int idCreationUser = idUser; // Suponiendo que el idCreationUser lo obtienes de algún lugar, como un login

                // Consulta para insertar un nuevo Pokémon
                string query = "INSERT INTO \"public\".\"Pokémon\" (\"Nombre\", \"Especie_ID\", \"Nivel\", \"Estado_ID\", \"Entrenador_ID\", \"Habilidad_ID\", \"EquipoVillano_ID\", \"Huevo_ID\", \"IdCreationUser\") " +
                               "VALUES (@nombre, @especieId, @nivel, @estadoId, @entrenadorId, @habilidadId, @equipoVillanoId, @huevoId, @idCreationUser)";

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        // Agregar parámetros
                        command.Parameters.AddWithValue("@nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@especieId", string.IsNullOrEmpty(txtEspecie.Text) ? (object)DBNull.Value : Convert.ToInt32(txtEspecie.Text));
                        command.Parameters.AddWithValue("@nivel", txtNivel.Text);
                        command.Parameters.AddWithValue("@estadoId", string.IsNullOrEmpty(txtEstado.Text) ? (object)DBNull.Value : Convert.ToInt32(txtEstado.Text));
                        command.Parameters.AddWithValue("@entrenadorId", string.IsNullOrEmpty(txtEntrenador.Text) ? (object)DBNull.Value : Convert.ToInt32(txtEntrenador.Text));
                        command.Parameters.AddWithValue("@habilidadId", string.IsNullOrEmpty(txtHabilidad.Text) ? (object)DBNull.Value : Convert.ToInt32(txtHabilidad.Text));
                        command.Parameters.AddWithValue("@equipoVillanoId", string.IsNullOrEmpty(txtEquipoVillano.Text) ? (object)DBNull.Value : Convert.ToInt32(txtEquipoVillano.Text));
                        command.Parameters.AddWithValue("@huevoId", string.IsNullOrEmpty(txtHuevo.Text) ? (object)DBNull.Value : Convert.ToInt32(txtHuevo.Text));
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
                int idPokemon = Convert.ToInt32(txtIdPokemon.Text); // Id del Pokémon a editar

                // Consulta para actualizar un Pokémon
                string query = "UPDATE \"public\".\"pokémon\" SET \"Nombre\" = @nombre, \"Especie_ID\" = @especieId, \"Nivel\" = @nivel, \"Estado_ID\" = @estadoId, " +
                               "\"Entrenador_ID\" = @entrenadorId, \"Habilidad_ID\" = @habilidadId, \"EquipoVillano_ID\" = @equipoVillanoId, \"Huevo_ID\" = @huevoId, \"IdEditUser\" = @idEditUser " +
                               "WHERE \"ID_Pokémon\" = @idPokemon";

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        // Agregar parámetros
                        command.Parameters.AddWithValue("@nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@especieId", string.IsNullOrEmpty(txtEspecie.Text) ? (object)DBNull.Value : Convert.ToInt32(txtEspecie.Text));
                        command.Parameters.AddWithValue("@nivel", txtNivel.Text);
                        command.Parameters.AddWithValue("@estadoId", string.IsNullOrEmpty(txtEstado.Text) ? (object)DBNull.Value : Convert.ToInt32(txtEstado.Text));
                        command.Parameters.AddWithValue("@entrenadorId", string.IsNullOrEmpty(txtEntrenador.Text) ? (object)DBNull.Value : Convert.ToInt32(txtEntrenador.Text));
                        command.Parameters.AddWithValue("@habilidadId", string.IsNullOrEmpty(txtHabilidad.Text) ? (object)DBNull.Value : Convert.ToInt32(txtHabilidad.Text));
                        command.Parameters.AddWithValue("@equipoVillanoId", string.IsNullOrEmpty(txtEquipoVillano.Text) ? (object)DBNull.Value : Convert.ToInt32(txtEquipoVillano.Text));
                        command.Parameters.AddWithValue("@huevoId", string.IsNullOrEmpty(txtHuevo.Text) ? (object)DBNull.Value : Convert.ToInt32(txtHuevo.Text));
                        command.Parameters.AddWithValue("@idEditUser", idEditUser);
                        command.Parameters.AddWithValue("@idPokemon", idPokemon);

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
                int idPokemon = Convert.ToInt32(txtIdPokemon.Text); // Id del Pokémon a eliminar

                // Consulta para eliminar un Pokémon
                string query = "UPDATE \"public\".\"pokémon\" SET \"Status\" = 0 WHERE \"ID_Pokémon\" = @idPokemon";

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        // Agregar parámetro
                        command.Parameters.AddWithValue("@idPokemon", idPokemon);

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