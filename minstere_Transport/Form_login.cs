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
    public partial class Form_login : Form
    {
        public Form_login()
        {
            InitializeComponent();
        }

        private void Form_login_Load(object sender, EventArgs e)
        {
            //TextBox1.Select();
            //TextBox1.Visible = false;
        }

        private void TextBox1_Click(object sender, EventArgs e)
        {

        }
        private void TxtUsername_Click(object sender, EventArgs e)
        {
            //TxtUsername.Text = "";
        }

        private void TxtPassword_Click(object sender, EventArgs e)
        {
            //TxtPassword.Text = "";
        }

        private void Label2_Click(object sender, EventArgs e)
        {
            
                Form_Principal Form1 = new Form_Principal();
                Form1.Show();
                this.Hide();
        }

        private void PnlEnter_Click(object sender, EventArgs e)
        {
            Form_Principal Form1 = new Form_Principal();
            Form1.Show();
            this.Hide();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
