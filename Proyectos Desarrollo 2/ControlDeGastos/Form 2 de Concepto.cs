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
    public partial class Form2 : Form
    {
        private bool Adding { get; set; } = false;
        private bool Updating { get; set; } = false;
        private bool Deleting { get; set; } = false;
        private string IdUp { get; set; }
        private bool Update2 { get; set; } = false;
        private List<string> Msg { get; set; } = new List<string>();
        public Form2()
        {
            InitializeComponent();
            GetRecord();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Adding = true;
            buttonNew.Enabled = false;
            NombreTxtBox.Enabled = true;
            DescriptionTxtBox.Enabled = true;
            checkBoxVisible.Enabled = true;
            buttonCancel.Enabled = true;
            buttonSave.Enabled = true;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Clear();
            buttonNew.Enabled = true;
            NombreTxtBox.Enabled = false;
            DescriptionTxtBox.Enabled = false;
            checkBoxVisible.Enabled = false;
            buttonCancel.Enabled = false;
            buttonSave.Enabled = false;
        }

        private void Clear() {
            foreach (Control c in groupBox1.Controls)
            {
                if (c is Button || c is Label || c is CheckBox)
                {
                }
                else
                {
                    c.Text = string.Empty;
                }
                buttonSave.Enabled = false;
                buttonCancel.Enabled= false;
                DescriptionTxtBox.Enabled= false;
                NombreTxtBox.Enabled = false;
                checkBoxVisible.Enabled = false;
                GetRecord();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                Adding = true;
                SaveRecord();
                buttonNew.Enabled = true;
                NombreTxtBox.Enabled = false;
                DescriptionTxtBox.Enabled = false;
                checkBoxVisible.Enabled = false;
                buttonCancel.Enabled = false;
                buttonSave.Enabled = false;
                buttonDelete.Enabled = true;
                buttonUpdate.Enabled = true;
                Adding = false;
            }
            else {
                string msg = string.Empty;
                foreach (var item in Msg)
                {
                    msg += item.ToString() + "\r";
                }
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            textBoxID.Text = IdCount().ToString();
            
        }

        private bool ValidateForm()
        {
            Msg = new List<string>();
            bool result = true;
            if (string.IsNullOrEmpty(NombreTxtBox.Text)) {
                result = false;
                Msg.Add("El Campo de Nombre está vacio");
            }
            return result;
        }

        private void SaveRecord()   {

            var json = string.Empty;
            var conceptList = new List<Concept>();
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\concepts.json";

            if (File.Exists(pathFile) && !Update2)
            {
                json = File.ReadAllText(pathFile, Encoding.UTF8);
                conceptList = JsonConvert.DeserializeObject<List<Concept>>(json);
            }
            
            var concept = new Concept();
            if (Adding == true) { //Adding
                var exist = conceptList.Count(x => x.Name == NombreTxtBox.Text.Trim());
                if (exist != 0)
                {
                    MessageBox.Show("EL CONCEPTO YA EXISTE", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                concept = new Concept
                {
                    ID = int.Parse(textBoxID.Text),
                    Name = NombreTxtBox.Text,
                    Description = DescriptionTxtBox.Text,
                    IsActive = checkBoxVisible.Checked ? true : false,
                    CreatedDate = DateTime.Now
                };
                conceptList.Add(concept);
            }

            else if (Updating)//Updating
            {
                var Id = LoadID();
                concept = conceptList.FirstOrDefault(x => x.ID == Id);
                NombreTxtBox.Text = concept.Name;
                DescriptionTxtBox.Text = concept.Description;
                checkBoxVisible.Checked = concept.IsActive;
                textBoxID.Text = concept.ID.ToString();
                IdUp = concept.Name;
                Update2 = true;
                return;
             } else if (Deleting) {
                var Id = LoadID();
                concept = conceptList.FirstOrDefault(x => x.ID == Id);
                conceptList.Remove(concept);
            }


            if (Update2) {
                var name = LoadName();
                concept = conceptList.FirstOrDefault(x => x.Name == name);
                conceptList.Remove(concept);
                Update2 = false;
            }
            json = JsonConvert.SerializeObject(conceptList);
            var save = new StreamWriter(pathFile, false, Encoding.UTF8);
            save.Write(json);
            save.Close();
            dvgConcept.DataSource = conceptList;

            MessageBox.Show("Archivo Guardado", "Agencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Clear();
        }

        private void GetRecord()
        {
            textBoxID.Text = "1";
            var json = string.Empty;
            var conceptList = new List<Concept>();
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\concepts.json";

            if (File.Exists(pathFile) && conceptList != null)
            {
                json = File.ReadAllText(pathFile, Encoding.UTF8);
                conceptList = JsonConvert.DeserializeObject<List<Concept>>(json);
                dvgConcept.DataSource = conceptList;
                {
                    textBoxID.Text = IdCount().ToString();

                }
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            Updating = true;
            buttonSave.Enabled = false;
            buttonNew.Enabled = false;
            buttonDelete.Enabled = false;
            buttonCancel.Enabled = true;
            SaveRecord();
            NombreTxtBox.Enabled = true;
            DescriptionTxtBox.Enabled = true;
            checkBoxVisible.Enabled = true;
            buttonCancel.Enabled = false;
            buttonSave.Enabled = true;
            buttonUpdate.Enabled = false;
            Updating = false;
        }
        private int LoadID() {
           var id = int.Parse(dvgConcept.CurrentCell.Value.ToString());
            return id;
        }
        private string LoadName()
        {
            var name = dvgConcept.CurrentRow.Cells[1].Value.ToString();
            return name;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Deleting = true;
            SaveRecord();
        }

        private void NombreTxtBox_TextChanged(object sender, EventArgs e)
        {

        }

        private int IdCount()
        {
            int max = 0;
            for (int i = 0; i < dvgConcept.Rows.Count;i++) {

                if (max < int.Parse(dvgConcept.Rows[i].Cells[0].Value.ToString())) {
                    max = int.Parse(dvgConcept.Rows[i].Cells[0].Value.ToString());
                }
            }
            return max + 1;
            {

            }
        }
    }

}


