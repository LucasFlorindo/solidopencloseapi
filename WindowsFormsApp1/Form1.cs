using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Classes;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        SWAPI objSW = new SWAPI();
        string nameArcPRT = @"C:\Users\Lucas Rodrigues\SKA AUTOMACAO DE ENGENHARIAS LTDA\Desenvolvimento Dassault - General\Documentos sobre o time\Integração de colaboradores\Treinamentos\5 - Treinamento API\Códigos fonte\SolidAPI\Peças\Garra.SLDPRT";
        string nameArcDRW = @"C:\Users\Lucas Rodrigues\SKA AUTOMACAO DE ENGENHARIAS LTDA\Desenvolvimento Dassault - General\Documentos sobre o time\Integração de colaboradores\Treinamentos\5 - Treinamento API\Códigos fonte\SolidAPI\Peças\Garra.SLDDRW";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAbrirSW_Click(object sender, EventArgs e)
        {
         
            // Substituir (Visivel.Checked, Versão do seu SW);
           

            try
            {
                labelSW.Text = "Abrindo SolidWorks...";
                objSW.AbrirSolidworks(Visivel.Checked, 31);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                labelSW.Text = "Pronto!";
            }
        }

        private void chkinv_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnFecharSW_Click(object sender, EventArgs e)
        {
            try
            {
                labelSW.Text = "Fechando SolidWorks...";
                objSW.FecharSolidworks();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "ERRO" , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                labelSW.Text = "Pronto!";
            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            try
            {
                objSW.AbrirArquivo(nameArcPRT, Path.GetExtension(nameArcPRT));
                MessageBox.Show(objSW.ObtemArquivoAtivo());

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            try
            {
                labelSW.Text = "Fechando arquivo...";
                objSW.FecharArquivo(nameArcPRT);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                labelSW.Text = "Pronto!";
            }
        }
    }
}
