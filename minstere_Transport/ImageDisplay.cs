using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace minstere_Transport
{
    public partial class ImageDisplay : Form
    {
        public string ReturnImageFile { get; set; }

        public ImageDisplay()
        {
            InitializeComponent();
        }
        public void Receiveparam(string param)
        {
            // this.pictureBox1.Load(this.ReturnImageFile);
            ReturnImageFile = param.ToString();
            
        }
        
        
        private void btnPrintImage_Click(object sender, EventArgs e)
        {

        }

        private void btnCloseImage_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(String.Equals(this.ReturnImageFile, "pas de Fichier Signé", StringComparison.Ordinal))
            {
                MessageBox.Show("Pas de Fichier Signé", "Visualisation du Fichier Signé", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                pictureBox1.Load(this.ReturnImageFile);
            }
            
            
        }
    }
}
