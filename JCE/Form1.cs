namespace JCE
{
    public partial class Form1 : Form
    {
        List<Persona> Ciudadanos = new List<Persona>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Save() {
            var ciudadano = new Persona
            {
                Nombre = textBoxNombre.Text,
                PrimerAp = textBoxApellido1.Text,
                SegundoAp = textBoxApellido2.Text,
                NroCed = textBoxNroCed.Text,
                LugNaci = textBoxLugNaci.Text,
                FechaNaci = dateTimePickerNacimiento.Value,
                Nacionalidad = textBoxNacionalidad.Text,
                Sexo = radioButtonM.Checked ? 'M' : 'F',
                TipoSangre = comboBox1TipoSangre.Text,
                EstadoCvil = comboBoxEstCivil.Text, 
                Ocupacion = textBoxOcupación.Text,
                FechaExpi = dateTimePickerFechaExpiracion.Value,
                Foto = new Bitmap(UsuariopictureBox.Image)
            };

           Ciudadanos.Add(ciudadano);

            MessageBox.Show("Ciudadano Agregado");

            Clear();
            MostrarPaciente();
            }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image File(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK) { 
                UsuariopictureBox.Image = new Bitmap(openFileDialog.FileName);
            }
        }

        private void Clear() { 
        
            foreach (Control c in groupBox1.Controls) {
                c.Text = string.Empty;
        }
    }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void MostrarPaciente() {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = Ciudadanos;
        }

        private void buttonCrear_Click_1(object sender, EventArgs e)
        {
            buttonCrear.Enabled = false;
            buttonCancelar.Enabled = true;
            buttonGuardar.Enabled = true;
            groupBox1.Enabled = true;
        }

        private void buttonGuardar_Click_1(object sender, EventArgs e)
        {
            Save();
            buttonGuardar.Enabled = false;
            buttonCrear.Enabled = true;
            buttonCancelar.Enabled = false;
            groupBox1.Enabled = false;
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            Clear();
            buttonGuardar.Enabled = false;
            buttonCrear.Enabled = true;
            buttonCancelar.Enabled = false;
            groupBox1.Enabled = false;
        }

        private void dateTimePickerNacimiento_ValueChanged(object sender, EventArgs e)
        {
            int age = DateTime.Now.Year - dateTimePickerNacimiento.Value.Year;

            this.BackColor = age <= 18 ? Color.Yellow : Color.Tomato;
        }
    }

    class Persona {
        public string Nombre { get; set; }
        public string PrimerAp { get; set; }
        public string SegundoAp { get; set; }
        public string NroCed { get; set; }
        public string LugNaci { get; set; }
        public DateTime FechaNaci { get; set; }
        public string Nacionalidad { get; set; }
        public char Sexo { get; set; }
        public string TipoSangre { get; set; }
        public string EstadoCvil { get; set; }
        public string Ocupacion { get; set; }
        public DateTime FechaExpi { get; set; }
        public Image Foto { get; set; }
    }
}