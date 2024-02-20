using RectanguloPOO.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RectanguloPOO.Windows
{
    public partial class FrmRectanguloEdit : Form
    {
        private Rectangulo rectangulo;
        public FrmRectanguloEdit()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (rectangulo != null)
            {
                LadoMayorTextBox.Text = rectangulo.LadoMayor.ToString();
                LadoMenorTextBox.Text = rectangulo.LadoMenor.ToString();
            }
        }

        private void FrmRectanguloEdit_Load(object sender, EventArgs e)
        {

        }

        internal Rectangulo GetRectangulo()
        {
            return rectangulo;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            //if (ValidarDatos())
            //{
            //    if (rectangulo == null)
            //    {
            //        rectangulo = new Rectangulo();
            //    }

            //    rectangulo.LadoMayor = int.Parse(LadoMayorTextBox.Text);
            //    rectangulo.LadoMenor = int.Parse(LadoMenorTextBox.Text);
            //    if (!rectangulo.Validar())
            //    {
            //        MessageBox.Show("El objeto no puede ser validado!!!");
            //        LadoMayorTextBox.Focus();
            //        return;
            //    }

            //    DialogResult = DialogResult.OK;
            //}

            if (ValidarDatos())
            {
                rectangulo = new Rectangulo();
                rectangulo.LadoMayor = int.Parse(LadoMayorTextBox.Text);
                rectangulo.LadoMenor = int.Parse(LadoMenorTextBox.Text);

                if (rectangulo.Validar())
                {
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("lados mal ingresados", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidarDatos()
        {
            bool esValido = true;
            errorProvider1.Clear();
            if (!int.TryParse(LadoMayorTextBox.Text, out int ladoMayor))
            {
                esValido = false;
                errorProvider1.SetError(LadoMayorTextBox, "Lado mal ingresado");
            }

            if (!int.TryParse(LadoMenorTextBox.Text, out int ladoMenor))
            {
                esValido = false;
                errorProvider1.SetError(LadoMenorTextBox, "Lado mal ingresado");
            }

            return esValido;
        }

        internal void SetRectangulo(Rectangulo copiaRectangulo)
        {
            this.rectangulo = copiaRectangulo;
        }
    }
}
