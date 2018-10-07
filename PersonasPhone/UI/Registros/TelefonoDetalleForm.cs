using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PersonasPhone.Entidades;
using PersonasPhone.BLL;
using System.Linq.Expressions;
using PersonasPhone.UI.Registros;
using PersonasPhone.DAL;

namespace PersonasPhone.UI
{
    public partial class TelefonoDetalleForm : Form
    {
        public List<TelefonoDetalle> Detalle { get; set; }

        Expression<Func<TelefonoDetalle, bool>> filtrar = x => true;
        public TelefonoDetalleForm()
        {
            InitializeComponent();
            this.Detalle = new List<TelefonoDetalle>();
            LlenarComboBox();
            Llenaclase();
        }

        private void CargarGrid()
        {
            DetalledataGridView.DataSource = null;
            DetalledataGridView.DataSource = this.Detalle;
        }

        public void Limpiar()
        {
            errorProvider.Clear();

            IdnumericUpDown.Value = 0;
            NombretextBox.Clear();
            CedulamaskedTextBox.Text = string.Empty;
            DirecciontextBox.Clear();
            FechadateTimePicker.Value = DateTime.Now;

            this.Detalle = new List<TelefonoDetalle>();
            CargarGrid();
        }

        private void LlenarComboBox()
        {
            Repositorio<Persona> persona = new Repositorio<Persona>(new Contexto());
            TypecomboBox.DataSource = persona.GetList(c => true);
            TypecomboBox.ValueMember = "TypeTelefono";
            TypecomboBox.DisplayMember = "TypeTelefono";
          
        }

        private void LlenarCampos(Persona persona)
        {
            IdnumericUpDown.Value = persona.IdPersona;
            NombretextBox.Text = persona.Nombre;
            CedulamaskedTextBox.Text = persona.Cedula.ToString();
            DirecciontextBox.Text = persona.Direccion;
            FechadateTimePicker.Value = persona.FechaNacimiento;

            this.Detalle = persona.Telefonos;
            CargarGrid();
        }

        private Persona Llenaclase()
        {
            Persona persona = new Persona();
            if (IdnumericUpDown.Value == 0)
            {
                persona.IdPersona = 0;
            }
            else
            {
                persona.IdPersona = Convert.ToInt32(IdnumericUpDown.Value);
            }
            persona.Nombre = NombretextBox.Text;
            persona.Cedula = Convert.ToInt32(CedulamaskedTextBox.Text);
            persona.Direccion = DirecciontextBox.Text;
            persona.FechaNacimiento = FechadateTimePicker.Value;

            return persona;
        }

        private bool Validar(int error)
        {
            bool paso = false;

            if (error == 1 && IdnumericUpDown.Value == 0)
            {
                IderrorProvider.SetError(IdnumericUpDown, "Llenar Id");
                paso = true;
            }
            if (error == 2 && TypecomboBox.Text == string.Empty)
            {
                errorProvider.SetError(TypecomboBox,
                   "Debes seleccionar un Tipo");
                paso = true;
            }
            if (error == 2 && CedulamaskedTextBox.Text == string.Empty)
            {
                errorProvider.SetError(CedulamaskedTextBox,
                   "Debes ingresar una Cedula");
                paso = true;
            }
            if (error == 2 && NombretextBox.Text == string.Empty)
            {
                errorProvider.SetError(NombretextBox,
                   "Debes ingresar un Nombre");
                paso = true;
            }
            if (error == 2 && DirecciontextBox.Text == string.Empty)
            {
                errorProvider.SetError(DirecciontextBox,
                    "Debes ingresar una Direccion");
                paso = true;
            }

            if (error == 2 && TelefonomaskedTextBox.Text == string.Empty)
            {
                errorProvider.SetError(TelefonomaskedTextBox,
                    "Debes ingresar un numero de Telefono");
                paso = true;
            }

            return paso;
        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            if (Validar(1))
            {
                MessageBox.Show("Favor de llenar casilla para poder Buscar");
            }
            else
            {
                int id = Convert.ToInt32(IdnumericUpDown.Value);
                Persona persona = BLL.PersonaBLL.Buscar(id);

                if (persona != null)
                {
                    IdnumericUpDown.Value = persona.IdPersona;
                    NombretextBox.Text = persona.Nombre;
                    CedulamaskedTextBox.Text = persona.Cedula.ToString();
                    DirecciontextBox.Text = persona.Direccion.ToString();
                    FechadateTimePicker.Value = persona.FechaNacimiento;

                    CargarGrid();
                }
                else
                {
                    MessageBox.Show("No Fue Encontrado!", "Fallido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                errorProvider.Clear();
            }

        }

        private void NuevoButtton_Click(object sender, EventArgs e)
        {
            IdnumericUpDown.Value = 0;
            NombretextBox.Clear();
            DirecciontextBox.Clear();
            FechadateTimePicker.Value = DateTime.Now;
            TelefonomaskedTextBox.Clear();
            TypecomboBox.SelectedValue = null;
            TelefonomaskedTextBox.Clear();
            DetalledataGridView.DataSource = null;
            
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            Persona persona = new Persona();
            if (Validar(2))
            {
                MessageBox.Show("Llenar Campos vacios");
                errorProvider.Clear();
                return;
            }
            else
            {
                persona = Llenaclase();
                if (IdnumericUpDown.Value == 0)
                {
                    if (BLL.PersonaBLL.Guardar(persona))
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
                    var result = MessageBox.Show("Seguro de Modificar?", "+Persona",
                         MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        if (BLL.PersonaBLL.Modificar(Llenaclase()))
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
            }
        }

        private void EliminarButton_Click(object sender, EventArgs e)
        {
            if (Validar(1))
            {
                MessageBox.Show("Llenar campos Vacios");
                return;
            }
            var result = MessageBox.Show("Seguro de  Eliminar?", "+Partidos",
                     MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (BLL.PersonaBLL.Eliminar(Convert.ToInt32(IdnumericUpDown.Value)))
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

        private void Addbutton_Click(object sender, EventArgs e)
        {
            TypeTelefonoForm typeTelefono = new TypeTelefonoForm();
                typeTelefono.Show();
        }

        private void Agregarbutton_Click(object sender, EventArgs e)
        {
            List<TelefonoDetalle> telefonoDetalles = new List<TelefonoDetalle>();
            if (Validar(2))
            {
                MessageBox.Show("Llene los Campos", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Detalle.Add(new TelefonoDetalle(
                        id: 0,
                        idPersona: (int)IdnumericUpDown.Value,
                        telefonos:TelefonomaskedTextBox.Text,
                        typeTelefono: TypecomboBox.Text

                    ));

                //Cargar el detalle al Grid
                CargarGrid();
                TelefonomaskedTextBox.Focus();
                TelefonomaskedTextBox.Clear();
                TypecomboBox.ResetText();
                DetalledataGridView.DataSource = null;
            }

        }

        private void Borrarbutton_Click(object sender, EventArgs e)
        {
            if (DetalledataGridView.Rows.Count > 0 && DetalledataGridView.CurrentRow != null)
            {
                Detalle.RemoveAt(DetalledataGridView.CurrentRow.Index);
                CargarGrid();
            }
        }
    }
}
