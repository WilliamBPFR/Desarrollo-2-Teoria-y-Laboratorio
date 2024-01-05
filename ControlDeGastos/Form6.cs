using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlDeGastos
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
            var Form3 = new Form3();
            Form3.Show();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Error()) {
                var Form3 = new Form3();
                Form3.LoadTypes(textBoxNewType.Text);
                Close();
                Form3.Show();
            }
        }

        private bool Error() {
            if (textBoxNewType.Text == string.Empty || textBoxNewType.Text == null)
            {
                MessageBox.Show("El nombre no ha sido llenada. \nColoque un nombre válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
