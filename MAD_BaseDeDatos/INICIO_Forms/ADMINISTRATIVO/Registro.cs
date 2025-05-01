using ClasesData;
using ClasesData.BD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace INICIO_Forms.ADMINISTRATIVO
{
    public partial class Registro: Form
    {
        public Registro()
        {
            InitializeComponent();
        }

        private void Registro_Load(object sender, EventArgs e)
        {
            CargarUsuariosEnListBox();
        }


        private void CargarUsuariosEnListBox()
        {
            listBox1.Items.Clear();
            var usuarios = BD_Usuario.ObtenerUsuariosOperativos();
            foreach (var (id, nombre) in usuarios)
            {
                listBox1.Items.Add(new ListItem(nombre, id));
            }
        }

        private void CrearCuenta_Click(object sender, EventArgs e)
        {
            Usuario nuevo = new Usuario
            {
                CorreoElectronico = textCorreo.Text,
                NombreCompleto = textNombre.Text,
                NumeroNomina = long.Parse(textNomina.Text),
                FechaNacimiento = FechaNacimiento.Value,
                TelefonoCasa = long.Parse(textPhoneCasa.Text),
                TelefonoCelular = long.Parse(textPhone.Text),
                FechaRegistro = DateTime.Now,
                ID_UsuarioRegistro = Sesion.ID_Usuario
            };

            string contrasena = textContra.Text;

            BD_Usuario.CrearUsuarioOperativo(nuevo, contrasena);

            MessageBox.Show("Usuario creado exitosamente.");
            CargarUsuariosEnListBox();
        }
        //Eliminar boton
        private void iconButton2_Click(object sender, EventArgs e)
        {

        }
        //Modificar
        private void iconButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
