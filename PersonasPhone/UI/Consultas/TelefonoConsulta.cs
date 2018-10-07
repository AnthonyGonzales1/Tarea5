using PersonasPhone.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PersonasPhone.BLL;

namespace PersonasPhone.UI.Consultas
{
    public partial class TelefonoConsulta : Form
    {
        Expression<Func<Persona, bool>> filtrar = x => true;

        public TelefonoConsulta()
        {
            InitializeComponent();
        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            int id;

            switch (TipocomboBox.SelectedIndex)
            {
                //ID
                case 0:
                    id = int.Parse(CriteriotextBox.Text);
                    filtrar = t => t.IdPersonas == id;
                    break;
                //Nombre
                case 1:

                    filtrar = t => t.Nombre.Contains(CriteriotextBox.Text);
                    break;
                //fecha
                case 2:
                    filtrar = t => (t.Fecha >= DesdedateTimePicker.Value) && (t.Fecha <= HastadateTimePicker.Value);
                    break;
            }


            ConsultadataGridView.DataSource = BLL.PersonaBLL.GetList(filtrar);

        }
    }
}
