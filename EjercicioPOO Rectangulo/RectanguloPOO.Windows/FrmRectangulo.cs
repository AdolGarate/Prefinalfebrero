using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RectanguloPOO.Datos;
using RectanguloPOO.Entidades;

namespace RectanguloPOO.Windows
{
    public partial class FrmRectangulo : Form
    {
        public FrmRectangulo()
        {
            InitializeComponent();
        }
        private List<Rectangulo> lista;
        private RepositorioDeRectangulos repositorio;
        private int CantidadDeRegistros;

        private void FrmRectangulo_Load(object sender, EventArgs e)
        {
            repositorio = new RepositorioDeRectangulos();
            CantidadDeRegistros = repositorio.GetCantidad();
            if (CantidadDeRegistros>0)
            {
                lista = repositorio.GetList();
                MostrarDatosEnGrilla();
                ActualizarCantidadDeRegistros(CantidadDeRegistros);
            }
        }

        private void ActualizarCantidadDeRegistros(int cantidadDeRegistros)
        {
            RegistrosLabel.Text = cantidadDeRegistros.ToString();
        }

        private void MostrarDatosEnGrilla()
        {
            DatosDataGridView.Rows.Clear();
            foreach (var rectangulo in lista)
            {
                DataGridViewRow r= ConstruirFila();
                SetFila(r, rectangulo);
                AgregarFila(r);
            }
        }

        private void AgregarFila(DataGridViewRow r)
        {
            DatosDataGridView.Rows.Add(r);
        }

        private DataGridViewRow ConstruirFila()
        {
            var r = new DataGridViewRow();
            r.CreateCells(DatosDataGridView);
            return r;
        }

        private void SetFila(DataGridViewRow r, Rectangulo rectangulo)
        {
            r.Cells[colLadoMayor.Index].Value = rectangulo.LadoMayor;
            r.Cells[colLadoMenor.Index].Value = rectangulo.LadoMenor;
            r.Cells[colArea.Index].Value = rectangulo.GetArea();
            r.Cells[colPerimetro.Index].Value = rectangulo.GetPerimetro();

            r.Tag=rectangulo;
        }

        private void SalirToolStripButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void NuevoToolStripButton_Click(object sender, EventArgs e)
        {
            FrmRectanguloEdit frm = new FrmRectanguloEdit() { Text = "Agregar Rectangulo" };
            DialogResult dr = frm.ShowDialog(this);
            if (dr==DialogResult.Cancel)
            {
                return;
            }

            Rectangulo rectangulo = frm.GetRectangulo();
            if (repositorio.Existe(rectangulo))
            {
                MessageBox.Show("rectangulo repetido", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            repositorio.Agregar(rectangulo);
            var r = ConstruirFila();
            SetFila(r, rectangulo);
            AgregarFila(r);
            MessageBox.Show("Rectangulo agregado correctamente", "mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void EditarToolStripButton_Click(object sender, EventArgs e)
        {
            if (DatosDataGridView.SelectedRows.Count == 0)
            {
                return;
            }

            var r = DatosDataGridView.SelectedRows[0];
            Rectangulo rectanguloSeleccionado = (Rectangulo)r.Tag;
            Rectangulo copiaRectangulo = (Rectangulo)rectanguloSeleccionado.Clone();
            FrmRectanguloEdit frm = new FrmRectanguloEdit();
            frm.SetRectangulo(copiaRectangulo);
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }

            copiaRectangulo = frm.GetRectangulo();
            if (repositorio.Existe(copiaRectangulo))
            {
                MessageBox.Show("Rectángulo existente!!!");
                return;

            }
            else
            {
                repositorio.Editar(rectanguloSeleccionado, copiaRectangulo);
                SetFila(r, copiaRectangulo);
                MessageBox.Show("Registro agregado");
            }

        }

        private void BorrarToolStripButton_Click(object sender, EventArgs e)
        {
            if (DatosDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow r = DatosDataGridView.SelectedRows[0];
                Rectangulo rectangulo = (Rectangulo)r.Tag;
                DialogResult dr = MessageBox.Show("¿desea borrar el regisrto?", "mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    repositorio.Borrar(rectangulo);
                    DatosDataGridView.Rows.Remove(r);
                    MessageBox.Show("registro borrado", "mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //ActualizarCantidadDeRegistros(repositorio.GetCantidad(predicado));
                }
            }
        }
    }
}
