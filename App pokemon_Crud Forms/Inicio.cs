using App_pokemon_Crud_Forms.Tablas;

namespace App_pokemon_Crud_Forms
{
    public partial class Inicio : Form
    {
        static int IdUser;
        static string connectionString;
        public Inicio(int id, string connection)
        {
            InitializeComponent();
            IdUser = id;
            connectionString = connection;
        }

        private void Entrenador_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Tabla1 tabla = new Tabla1(IdUser, connectionString);
            this.Hide();
            tabla.ShowDialog();
        }

        private void Pokemon_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Tabla2 tabla = new Tabla2(IdUser, connectionString);
            this.Hide();
            tabla.ShowDialog();
        }

        private void Habilidad_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Tabla3 tabla = new Tabla3(IdUser, connectionString);
            this.Hide();
            tabla.ShowDialog();
        }

        private void Tipo_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Tabla4 tabla = new Tabla4(IdUser, connectionString);
            this.Hide();
            tabla.ShowDialog();
        }

        private void Ciudad_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Tabla5 tabla = new Tabla5(IdUser, connectionString);
            this.Hide();
            tabla.ShowDialog();
        }

        private void Objeto_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Tabla6 tabla = new Tabla6(IdUser, connectionString);
            this.Hide();
            tabla.ShowDialog();
        }

        private void Region_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Tabla7 tabla = new Tabla7(IdUser, connectionString);
            this.Hide();
            tabla.ShowDialog();
        }

        private void Ruta_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Tabla8 tabla = new Tabla8(IdUser, connectionString);
            this.Hide();
            tabla.ShowDialog();
        }

        private void Medalla_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Tabla9 tabla = new Tabla9(IdUser, connectionString);
            this.Hide();
            tabla.ShowDialog();
        }

        private void Clima_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Tabla10 tabla = new Tabla10(IdUser, connectionString);
            this.Hide();
            tabla.ShowDialog();
        }
    }
}
