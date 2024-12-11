using Npgsql;

namespace App_pokemon_Crud_Forms
{
    public partial class login : Form
    {
        static string connectionString = "Host=localhost;Port=5432;Username=root;Password=220504;Database=pokemon";
        static int id = 0;

        public login()
        {
            InitializeComponent();
        }

        public static bool Iniciar_seccion(string nombreUsuario, string contrasena, int idUsuario = -1)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Consulta SQL para verificar el usuario
                    string query = "SELECT \"Id\" FROM \"public\".\"User\" WHERE \"Username\" = @usuario AND \"Password\" = @contrasena";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        // Agregar parámetros a la consulta para evitar SQL Injection
                        command.Parameters.AddWithValue("@usuario", nombreUsuario);
                        command.Parameters.AddWithValue("@contrasena", EncriptarContrasena(contrasena));  // Asegúrate de encriptar la contraseña adecuadamente

                        // Ejecutar la consulta y capturar el ID del usuario
                        var result = command.ExecuteScalar();

                        // Si result no es null, se ha encontrado un usuario
                        if (result != null)
                        {
                            // Asignamos el ID del usuario a la variable de salida
                            id = Convert.ToInt32(result);

                            // La conexión se cierra automáticamente al salir del bloque using
                            return true;  // Usuario encontrado
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Mostrar el error en la consola o en un mensaje para el usuario
                MessageBox.Show($"Error al iniciar sesión: {ex.Message}");
                return false;  // Si ocurre un error
            }

            return false;  // Si no se encontró el usuario
        }

        // Método para encriptar la contraseña (esto es solo un ejemplo, utiliza un método seguro)
        private static string EncriptarContrasena(string contrasena)
        {
            // Implementar encriptación (por ejemplo, usando SHA256)
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(contrasena));
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        public static bool Registrar(string nombreUsuario, string contrasena)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Consulta SQL para insertar un nuevo usuario
                    string query = "INSERT INTO \"public\".\"User\" (\"Username\", \"Password\", \"IdCreationUser\", \"CreateDate\") " +
                                   "VALUES(@usuario, @contrasena, 1, NOW())";  // 'NOW()' para la fecha y hora actual en PostgreSQL

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        // Agregar los parámetros a la consulta para evitar inyecciones SQL
                        command.Parameters.AddWithValue("@usuario", nombreUsuario);
                        command.Parameters.AddWithValue("@contrasena", EncriptarContrasena(contrasena));  // Asegúrate de encriptar la contraseña

                        // Ejecutar la consulta de inserción
                        int rowsAffected = command.ExecuteNonQuery();

                        // Si se afectaron filas, la inserción fue exitosa
                        if (rowsAffected > 0)
                        {
                            connection.Close();
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Mostrar error si ocurre alguna excepción
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }

            return false;  // Si no se afectaron filas o hubo un problema
        }

        private void Continuar_Btm_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text == string.Empty && txtPassword.Text == string.Empty)
            {
                return;
            }

            if (Iniciar_seccion(txtUsuario.Text, txtPassword.Text))
            {
                MessageBox.Show("inicio de seccion exitoso", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Inicio inicio = new Inicio(id, connectionString);
                this.Hide();
                inicio.ShowDialog();
            }
            else
            {
                MessageBox.Show("Inicio de seccion incorrecto", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Regsitro_Btm_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text == string.Empty && txtPassword.Text == string.Empty)
            {
                return;
            }

            if (Registrar(txtUsuario.Text, txtPassword.Text))
            {
                MessageBox.Show("Registro exitoso", "Informacion",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Registro incorrecto", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
