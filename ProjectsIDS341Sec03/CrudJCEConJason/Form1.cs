using CrudJCEConJason.Modelos;
using Newtonsoft.Json;
using System.Text;

namespace CrudJCEConJason
{
    public partial class Form1 : Form
    {
        public bool Adding { get; set; } = false;
        public bool Deleting { get; set; } = false;
        public bool Update { get; set; } = false;
        public string CedulaUp { get; set; }
        public string pathPhoto { get; set; } = string.Empty;
        public Form1()
        {
            InitializeComponent();
            GetRecord();
        }

        private void GetRecord()
        {
            var json = string.Empty;
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\ciudadanos.json";
            var ciudadanosList = new List<Ciudadano>();

            if (File.Exists(pathFile)) {
                json = File.ReadAllText(pathFile);
                ciudadanosList = JsonConvert.DeserializeObject<List <Ciudadano>>(json);

                dataGridView1.DataSource = ciudadanosList;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                Adding = true;

                SaveRecord();
                Adding = false;
            }
        }

        private bool Validar()
        {
            foreach (Control item in groupBox1.Controls)
            {
                if ((item.Text == string.Empty || item.Text is null) && item.Name is not "textBoxOcupacion" && item.Name is not "pictureBox2") {
                    MessageBox.Show("Hay un campo necesario vacío.\n\n Solo puede dejar vacío el campo de Foto y Ocupación", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        private void SaveRecord()
        {
            var json = string.Empty;
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\ciudadanos.json";
            var ciudadanosList = new List<Ciudadano>();
            var ciudadano = new Ciudadano();
            if (File.Exists(pathFile))
            {
                json = File.ReadAllText(pathFile);
                ciudadanosList = JsonConvert.DeserializeObject<List<Ciudadano>>(json);
            }

            if (Adding)
            {
                ciudadano = new Ciudadano
                {
                    Name = textBoxNombre.Text,
                    Apellido = textBoxApellido.Text,
                    Cedula = textBoxNroCed.Text,
                    Nacionalidad = textBoxNacionalidad.Text,
                    LugNacimiento = textBoxLugNaci.Text,
                    FechNaci = Convert.ToDateTime(dateTimeFechaNaci.Value),
                    EstCivil = comboBoxEstCivil.Text,
                    Sexo = radioButtonHombre.Checked ? "Hombre" : "Mujer",
                    TipoSangre = comboBoxTipoSangre.Text,
                    Ocupacion = textBoxOcupacion.Text,
                    ColegioElectoral = textBoxColegioElec.Text,
                    DireccionResi = textBoxDirecResi.Text,
                    Sector = textBoxSector.Text,
                    Municipio = textBoxMunicipio.Text,
                    CodPostal = textBoxCodPost.Text,
                };
                if (pathPhoto == string.Empty)
                {
                    ciudadano.Foto = "NoPhoto";
                }
                else { 
                    ciudadano.Foto = pathPhoto;
                    pathPhoto = string.Empty;
                }
                ciudadanosList.Add(ciudadano);
            }
            else if (Deleting)
            {
                string cedula = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                ciudadano = ciudadanosList.FirstOrDefault(x => x.Cedula == cedula);
                ciudadanosList.Remove(ciudadano);
            }
            else if (Update && !Adding)
            {
                string cedula = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                CedulaUp = cedula;
                CaegarCiudadano(ciudadanosList.FirstOrDefault(x => x.Cedula == cedula));
                return;
            }

            if (Update)
            {
                ciudadano = ciudadanosList.FirstOrDefault(x => x.Cedula == CedulaUp);
                ciudadanosList.Remove(ciudadano);
                Update = false;
                CedulaUp = string.Empty;
            }
            json = JsonConvert.SerializeObject(ciudadanosList);
            var save = new StreamWriter(pathFile, false, Encoding.UTF8);
            save.Write(json);
            save.Close();

            dataGridView1.DataSource = ciudadanosList;
            Clear();
        }

        private void CaegarCiudadano(Ciudadano ciudadano)
        {
            textBoxNombre.Text = ciudadano.Name;
            textBoxApellido.Text = ciudadano.Apellido;
            textBoxNroCed.Text = ciudadano.Cedula;
            textBoxNacionalidad.Text = ciudadano.Nacionalidad;
            textBoxLugNaci.Text = ciudadano.LugNacimiento;
            dateTimeFechaNaci.Value = ciudadano.FechNaci;
            comboBoxEstCivil.Text = ciudadano.EstCivil;
            comboBoxTipoSangre.Text = ciudadano.TipoSangre;
            textBoxOcupacion.Text = ciudadano.Ocupacion;
            textBoxColegioElec.Text = ciudadano.ColegioElectoral;
            textBoxDirecResi.Text = ciudadano.DireccionResi;
            textBoxSector.Text = ciudadano.Sector;
            textBoxMunicipio.Text = ciudadano.Municipio;
            textBoxCodPost.Text = ciudadano.CodPostal;

            if (ciudadano.Foto != "NoPhoto")
            {
                pictureBox2.Image = Image.FromFile(ciudadano.Foto);
            }
            else {
                pictureBox2.Image = pictureBox1.Image;
            }
            }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image File(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                pictureBox2.Image = new Bitmap(openFileDialog.FileName);
                pathPhoto = openFileDialog.FileName;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           textBoxApellido.Enabled = true;
           textBoxCodPost.Enabled = true;
           textBoxColegioElec.Enabled = true;
           textBoxDirecResi.Enabled = true;
           textBoxLugNaci.Enabled = true;
           textBoxMunicipio.Enabled = true;
           textBoxNacionalidad.Enabled = true;
           textBoxNombre.Enabled = true;
           textBoxNroCed.Enabled = true;
           textBoxOcupacion.Enabled = true;
           textBoxSector.Enabled = true;
           comboBoxEstCivil.Enabled = true;
           comboBoxTipoSangre.Enabled = true;
           dateTimeFechaNaci.Enabled = true;
           radioButtonHombre.Enabled = true;
           radioButtonMujer.Enabled = true;
           buttonFoto.Enabled = true;
           buttonNew.Enabled = false;
           buttonFoto.Enabled = true;
           buttonCancel.Enabled = true;
           buttonDelete.Enabled = false;
           buttonUpdate.Enabled = false;
            buttonSave.Enabled = true;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            foreach (Control item in groupBox1.Controls) {
                if (item is not Label && item is not Button & item is not RadioButton) { 
                    item.Text = String.Empty;
                }
            }

            textBoxApellido.Enabled = false;
            textBoxCodPost.Enabled = false;
            textBoxColegioElec.Enabled = false;
            textBoxDirecResi.Enabled = false;
            textBoxLugNaci.Enabled = false;
            textBoxMunicipio.Enabled = false;
            textBoxNacionalidad.Enabled = false;
            textBoxNombre.Enabled = false;
            textBoxNroCed.Enabled = false;
            textBoxOcupacion.Enabled = false;
            textBoxSector.Enabled = false;
            comboBoxEstCivil.Enabled = false;
            comboBoxTipoSangre.Enabled = false;
            dateTimeFechaNaci.Enabled = false;
            radioButtonHombre.Enabled = false;
            radioButtonMujer.Enabled = false;
            buttonFoto.Enabled = false;
            buttonNew.Enabled = true;
            buttonFoto.Enabled = false;
            buttonCancel.Enabled = false;
            buttonDelete.Enabled = true;
            buttonUpdate.Enabled = true;
            buttonSave.Enabled = false;
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            Update = true;
            SaveRecord();
            textBoxApellido.Enabled = true;
            textBoxCodPost.Enabled = true;
            textBoxColegioElec.Enabled = true;
            textBoxDirecResi.Enabled = true;
            textBoxLugNaci.Enabled = true;
            textBoxMunicipio.Enabled = true;
            textBoxNacionalidad.Enabled = true;
            textBoxNombre.Enabled = true;
            textBoxNroCed.Enabled = true;
            textBoxOcupacion.Enabled = true;
            textBoxSector.Enabled = true;
            comboBoxEstCivil.Enabled = true;
            comboBoxTipoSangre.Enabled = true;
            dateTimeFechaNaci.Enabled = true;
            radioButtonHombre.Enabled = true;
            radioButtonMujer.Enabled = true;
            buttonFoto.Enabled = true;
            buttonNew.Enabled = false;
            buttonFoto.Enabled = true;
            buttonCancel.Enabled = true;
            buttonDelete.Enabled = false;
            buttonUpdate.Enabled = false;
            buttonSave.Enabled = true;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Deleting = true;
            SaveRecord();
            Deleting = false;
        }

        private void dateTimeFechaNaci_ValueChanged(object sender, EventArgs e)
        {
            if ((DateTime.Now.Year - dateTimeFechaNaci.Value.Year ) > 18)
            {
                BackColor = Color.Coral;
            }
            else {
                BackColor = Color.LightBlue;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}