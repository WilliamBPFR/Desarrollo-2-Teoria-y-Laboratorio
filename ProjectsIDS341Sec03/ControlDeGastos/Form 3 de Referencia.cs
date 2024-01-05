using ControlDeGastos.Modelos;
using Newtonsoft.Json;
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
    public partial class Form3 : Form
    {
        private bool Adding { get; set; } = false;
        private bool Delete { get; set; } = false;
        private bool Update { get; set; } = false;
        private bool Updated2 { get; set; } = false;
        private string NameUp { get; set; }
        public List<string> Tipos = new List<string> { "Tarjeta de Crédito", "Tarjeta de Débito", "Cuenta de Ahorro", "Cuenta Corriente", "Otros" };
        public Form3()
        {
            InitializeComponent();
            GetRecords();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void DescriptionTxtBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void SaveRecord()
        {

            var json = string.Empty;
            var referencesList = new List<References>();
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\references.json";

            if (File.Exists(pathFile))
            {
                json = File.ReadAllText(pathFile, Encoding.UTF8);
                referencesList = JsonConvert.DeserializeObject<List<References>>(json);
            }
            var references = new References();

            if (Adding) {
                var exist = referencesList.Count(x => x.Name == NombreTxtBox.Text.Trim());
                if (exist != 0 && !Updated2)
                {
                    MessageBox.Show("EL CONCEPTO YA EXISTE", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                references = new References
                {
                    SourceType = comboBoxType.Text,
                    Id = int.Parse(textBoxID.Text),
                    Name = NombreTxtBox.Text,
                    Description = DescriptionTxtBox.Text,
                    Visible = checkBoxVisible.Checked ? true : false,
                    CreatedDate = DateTime.Now,
                };
                referencesList.Add(references);
            }
            else if (Delete)
            {
                var pos = dvgReferences.CurrentRow.Cells[1].Value.ToString();
                if (pos != null)
                {
                    references = referencesList.FirstOrDefault(x => int.Parse(x.Id.ToString()) == int.Parse(pos));
                    referencesList.Remove(references);
                    MessageBox.Show($"El elemento con nombre {references.Name} ha sido borrado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            } else if (Update) {
                int Id = int.Parse(dvgReferences.CurrentRow.Cells[1].Value.ToString());
                references = referencesList.FirstOrDefault(x => x.Id == Id);
                textBoxID.Text = references.Id.ToString();
                NombreTxtBox.Text = references.Name;
                DescriptionTxtBox.Text = references.Description;
                checkBoxVisible.Checked = references.Visible;
                comboBoxType.Text = references.SourceType;
                NameUp = references.Name;
                Updated2 = true;
            } 
            if (Updated2) {
                references = referencesList.FirstOrDefault(x => x.Name == NameUp);
                referencesList.Remove(references);
                Updated2 = false;
            }

            json = JsonConvert.SerializeObject(referencesList);

            var save = new StreamWriter(pathFile, false, Encoding.UTF8);
            save.Write(json);
            save.Close();
            dvgReferences.DataSource = referencesList;
        }

        private int IDtext()
        {
            int max = 0;
            for (int i = 0; i < dvgReferences.Rows.Count; i++)
            {
                if (max < int.Parse(dvgReferences.Rows[i].Cells[1].Value.ToString()))
                {
                    max = int.Parse(dvgReferences.Rows[i].Cells[1].Value.ToString());
                }
            }
            return max+1;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (ValidateInfo()) {
                Adding = true;
                SaveRecord();
                Adding = false;
                buttonCancel.Enabled = false;
                buttonNew.Enabled = true;
                buttonSave.Enabled = false;
                buttonUpdate.Enabled = true;
                comboBoxType.Enabled = false;
                NombreTxtBox.Enabled = false;
                DescriptionTxtBox.Enabled = false;
                buttonNewType.Enabled = true;
                buttonDelete.Enabled = true;
                buttonNewType.Enabled = true;
                Clear();
                textBoxID.Text = IDtext().ToString();
            }
        }

        private bool ValidateInfo()
        {
            if (comboBoxType.Text == string.Empty || comboBoxType.Text is null || VerifyTypes()) {
                MessageBox.Show("El Tipo de Activo ingresado no es válido. Ingrese ino nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool VerifyTypes()
        {
            foreach (var item in comboBoxType.Items)
            {
                if (item.ToString() == comboBoxType.Text)
                {
                    return false;
                }
            }
            return true;
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            buttonCancel.Enabled = true;
            buttonNew.Enabled = false;
            buttonSave.Enabled = true;
            buttonUpdate.Enabled = false;
            comboBoxType.Enabled = true;
            NombreTxtBox.Enabled = true;
            DescriptionTxtBox.Enabled = true;
            checkBoxVisible.Enabled = true;
            buttonNewType.Enabled = false;
            buttonDelete.Enabled = false;
        }

        public void Clear() {
            foreach (Control c in groupBox1.Controls)
            {
                if (c is ComboBox || c is TextBox) {
                    c.Text = string.Empty;
                }
            }
            textBoxID.Text = IDtext().ToString();

        }

        public void GetRecords() {
            textBoxID.Text = "1";
            var json = string.Empty;
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\references.json";
            var referenceList = new List<References>();

            if (File.Exists(pathFile)) {
                 
                json = File.ReadAllText(pathFile, Encoding.UTF8);
                referenceList = JsonConvert.DeserializeObject<List<References>>(json);
                dvgReferences.DataSource = referenceList;

                textBoxID.Text = IDtext().ToString();
            }
            pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\types.json";
            var typeList = new List<string>();

            if (File.Exists(pathFile))
            {
                json = File.ReadAllText(pathFile, Encoding.UTF8);
                typeList = JsonConvert.DeserializeObject<List<string>>(json);
                comboBoxType.DataSource = typeList;
            }
            else {
                LoadTypes("xd");
            }
        }

        public void LoadTypes(string newType) {
            var json = string.Empty;
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\types.json";
            var typeList = new List<string>();

            if (File.Exists(pathFile))
            {
                json = File.ReadAllText(pathFile, Encoding.UTF8);
                typeList = JsonConvert.DeserializeObject<List<string>>(json);
                typeList.Add(newType);
            }
            else
            {
                foreach (var item in Tipos)
                {
                    typeList.Add(item);
                }
            }
                comboBoxType.DataSource = typeList.ToArray();
                json = JsonConvert.SerializeObject(typeList);
                var save = new StreamWriter(pathFile,false,Encoding.UTF8);
                save.Write(json);
                save.Close();
            }

        private void button4_Click(object sender, EventArgs e)
        {
            var Form6 = new Form6();
            Form6.ShowDialog();
            Close();
        }

        private void dvgReferences_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Delete = true;
            SaveRecord();
            Delete = false;
            textBoxID.Text = IDtext().ToString();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            Update = true;
            SaveRecord();
            Update = false;
            NombreTxtBox.Enabled = true;
            DescriptionTxtBox.Enabled = true;
            checkBoxVisible.Enabled = true;
            buttonCancel.Enabled = true;
            buttonDelete.Enabled = false;
            buttonNew.Enabled = false;
            buttonUpdate.Enabled = false;
            buttonSave.Enabled = true;
            comboBoxType.Enabled = true;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Clear();
            comboBoxType.Enabled = false;
            NombreTxtBox.Enabled = false;
            DescriptionTxtBox.Enabled = false;
            checkBoxVisible.Enabled = false;
            buttonNew.Enabled = true;
            buttonUpdate.Enabled = true;
            buttonDelete.Enabled = false;
            buttonSave.Enabled = false;
            buttonDelete.Enabled = true;
            buttonNewType.Enabled = true;
            buttonCancel.Enabled = false;
        }
    }


    }
