using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaLogica;

namespace CapaPresentacion
{
    public partial class FrmModuloEspecialista : Form
    {
        public FrmModuloEspecialista()
        {
            InitializeComponent();
        }

        // Mostrar mensaje de éxito
        private void MostrarMensajeExito(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema Clínico",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // Mostrar mensaje de error
        private void MostrarMensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema Clínico",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        // Limpiar los campos del formulario
        private void LimpiarCampos()
        {
            this.txtCmpEspecialista.Text = string.Empty;
            this.txtNombreEspecialista.Text = string.Empty;
            this.txtApellidoEspecialista.Text = string.Empty;
            this.txtEspecialidad.Text = string.Empty; // Cambiado a txtEspecialidad
        }

        // Cargar datos de los especialistas en el DataGridView
        private void CargarDatosEspecialista()
        {
            try
            {
                DataTable dtEspecialistas = NEspecialista.ListarEspecialista();
                if (dtEspecialistas != null && dtEspecialistas.Rows.Count > 0)
                {
                    this.dgvEspecialista.DataSource = dtEspecialistas;
                }
                else
                {
                    MessageBox.Show("No se encontraron datos de especialistas.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los datos de especialistas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Buscar datos de un especialista por CMP
        private void BuscarDatosEspecialista()
        {
            int cmpEspecialista;
            if (!int.TryParse(txtCmpEspecialista.Text.Trim(), out cmpEspecialista))
            {
                MessageBox.Show("El CMP debe ser un número entero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.dgvEspecialista.DataSource = NEspecialista.ListarEspecialistaCMP(cmpEspecialista);
        }

        private void FrmModuloEspecialista_Load(object sender, EventArgs e)
        {
            CargarDatosEspecialista();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarDatosEspecialista();
        }

        private void InsertarEspecialista()
        {
            int especialistaCMP;
            string especialistaNombre = txtNombreEspecialista.Text.Trim();
            string especialistaApellido = txtApellidoEspecialista.Text.Trim();
            int especialidadCodigo;

            // Validación básica
            if (string.IsNullOrEmpty(especialistaNombre) || string.IsNullOrEmpty(especialistaApellido))
            {
                MessageBox.Show("Los campos nombre y apellido son obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validación del CMP
            if (!int.TryParse(txtCmpEspecialista.Text.Trim(), out especialistaCMP))
            {
                MessageBox.Show("El CMP debe ser un número entero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validación de la Especialidad
            if (!int.TryParse(txtEspecialidad.Text.Trim(), out especialidadCodigo))
            {
                MessageBox.Show("La Especialidad debe ser un número entero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Inserta el especialista con los datos proporcionados
            NEspecialista.Insertar(especialistaCMP, especialistaNombre, especialistaApellido, especialidadCodigo);
            // ... (Maneja la respuesta de la función Insertar)
        }



        private void ActualizarEspecialista()
        {
            int especialistaCMP;
            string especialistaNombre = txtNombreEspecialista.Text.Trim();
            string especialistaApellido = txtApellidoEspecialista.Text.Trim();
            int especialidadCodigo;

            // Validación básica
            if (string.IsNullOrEmpty(especialistaNombre) || string.IsNullOrEmpty(especialistaApellido))
            {
                MessageBox.Show("Los campos nombre y apellido son obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validación del CMP
            if (!int.TryParse(txtCmpEspecialista.Text.Trim(), out especialistaCMP))
            {
                MessageBox.Show("El CMP debe ser un número entero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validación de la Especialidad
            if (!int.TryParse(txtEspecialidad.Text.Trim(), out especialidadCodigo))
            {
                MessageBox.Show("La Especialidad debe ser un número entero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            NEspecialista.Actualizar(especialistaCMP, especialistaNombre, especialistaApellido, especialidadCodigo);
            // ... (Maneja la respuesta de la función Actualizar)
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            InsertarEspecialista();
            CargarDatosEspecialista();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarEspecialista();
            CargarDatosEspecialista();
        }

        private void EliminarEspecialista()
        {
            // Obtener el CMP del especialista a eliminar
            int especialistaCMP;
            if (!int.TryParse(txtCmpEspecialista.Text.Trim(), out especialistaCMP))
            {
                MessageBox.Show("El CMP debe ser un número entero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Confirmar la eliminación
            if (MessageBox.Show("¿Está seguro de eliminar el especialista?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Eliminar el especialista
                string respuesta = NEspecialista.Eliminar(especialistaCMP);

                // Mostrar mensaje de éxito o error
                if (respuesta == "Ok")
                {
                    MessageBox.Show("Especialista eliminado exitosamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error al eliminar el especialista: " + respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarEspecialista();
            CargarDatosEspecialista();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertarEspecialista();
            CargarDatosEspecialista();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ActualizarEspecialista();
            CargarDatosEspecialista();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EliminarEspecialista();
            CargarDatosEspecialista();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BuscarDatosEspecialista();
        }
    }



}
