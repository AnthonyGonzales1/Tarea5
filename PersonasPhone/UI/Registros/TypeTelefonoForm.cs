using PersonasPhone.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonasPhone.UI.Registros
{
    public partial class TypeTelefonoForm : Form
    {
        public TypeTelefonoForm()
        {
            InitializeComponent();
        }

        private void Limpiar()
        {
            IdnumericUpDown.Value = 0;
            TelefonotextBox.Clear();
        }

        private bool Validar(int error)
        {
            bool paso = false;

            if (error == 1 && IdnumericUpDown.Value == 0)
            {
                errorProvider.SetError(IdnumericUpDown,
                   "Debe introducir un Id");
                paso = true;
            }
            if (error == 2 && TelefonotextBox.Text == string.Empty)
            {
                errorProvider.SetError(TelefonotextBox,
                   "Debe ingresar una Descripcion");
                paso = true;
            }

            return paso;
        }

        private void LlenarCampos()
        {
            TelefonoDetalle telefonoDetalle = new TelefonoDetalle();

            TelefonotextBox.Text = telefonoDetalle.Telefonos.ToString();
        }

        private TelefonoDetalle LlenaClase()
        {
            TelefonoDetalle tipoPartido = new TelefonoDetalle();
            if (IdnumericUpDown.Value == 0)
            {
                tipoPartido.id = 0;
            }
            else
            {
                tipoPartido.TipoPartidoId = Convert.ToInt32(IdnumericUpDown.Value);

            }
            tipoPartido.Descripcion = DescripciontextBox.Text;

            return tipoPartido;
        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            if (Validar(1))
            {
                MessageBox.Show("Favor de llenar la casilla para poder Buscar");
            }
            else
            {
                int id = Convert.ToInt32(IdnumericUpDown.Value);
                tipoPartido = BLL.TipoPartidosBLL.Buscar(id);

                if (tipoPartido != null)
                {
                    IdnumericUpDown.Value = tipoPartido.TipoPartidoId;
                    DescripciontextBox.Text = tipoPartido.Descripcion.ToString();

                }
                else
                {
                    MessageBox.Show("No Fue Encontrado!", "Fallido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                errorProvider.Clear();
            }
        }

        private void Nuevobutton_Click(object sender, EventArgs e)
        {

        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            if (Validar(2))
            {
                MessageBox.Show("Llenar Campos vacios");
                errorProvider.Clear();
                return;
            }
            else
            {
                tipoPartido = LlenaClase();
                if (IdnumericUpDown.Value == 0)
                {
                    if (BLL.TipoPartidosBLL.Guardar(tipoPartido))
                    {
                        MessageBox.Show("Guardado!", "Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo Guardar!!");
                    }
                }
                else
                {
                    var result = MessageBox.Show("Seguro de Modificar?", "+TipoPartidos",
                     MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        if (BLL.TipoPartidosBLL.Modificar(LlenaClase()))
                        {
                            MessageBox.Show("Modificado!!");
                            Limpiar();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo Guardar!!");
                        }
                    }
                }
                Limpiar();
                errorProvider.Clear();
            }
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            if (Validar(1))
            {
                MessageBox.Show("Favor de llenar casilla para poder Eliminar");
            }
            var result = MessageBox.Show("Seguro de  Eliminar?", "+Tipo de Partidos",
                     MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (BLL.PartidosBLL.Eliminar(Convert.ToInt32(IdnumericUpDown.Value)))
                {
                    MessageBox.Show("Eliminado");
                    Limpiar();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar");
                }
            }
        }
    }
    }
}
