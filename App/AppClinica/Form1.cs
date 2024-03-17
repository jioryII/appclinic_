using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaPresentacion;

namespace AppClinica
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        CapaPresentacion.FrmModuloPaciente frmModuloPacientes = new CapaPresentacion.FrmModuloPaciente();
        private void registroDePacientesToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            frmModuloPacientes.MdiParent = this;
            frmModuloPacientes.Show();
        }
        
        CapaPresentacion.FrmModuloEspecialista frmModuloEspecialistas = new CapaPresentacion.FrmModuloEspecialista();
        private void registroDeEspecialistaToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            frmModuloEspecialistas.MdiParent = this;
            frmModuloEspecialistas.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = DateTime.Now.ToLongTimeString();
            toolStripStatusLabel2.Text = DateTime.Now.ToLongDateString();
            toolStripProgressBar1.Increment(10);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
    }
}
