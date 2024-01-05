using ControlDeGastos.Modelos;
using Newtonsoft.Json;
using System.Text;

namespace ControlDeGastos
{
    public partial class FormPrincipal : Form
    {
        public bool Adding { get; set; } = false;
        public bool Deleting { get; set; } = false;
        public bool Update { get; set; } = false;
        private int deleteId { get; set; }
        public FormPrincipal()
        {
            InitializeComponent();
            GetRecord();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var From2 = new Form2();
            From2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clear();
            GetRecord();
        }

        public void GetConcepts()
        {
            var json = string.Empty;
            var conceptList = new List<Concept>();
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\concepts.json";

            if (File.Exists(pathFile) && conceptList != null)
            {
                json = File.ReadAllText(pathFile, Encoding.UTF8);
                conceptList = JsonConvert.DeserializeObject<List<Concept>>(json);
            }

            ConcepttxtBox.DataSource = conceptList.Where(x => x.IsActive == true).ToList();
            ConcepttxtBox.DisplayMember = "Name";
            ConcepttxtBox.ValueMember = "ID";
        }

        public void GetReferences() { 
            var json = string.Empty;
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\references.json";
            var referencesList = new List<References>();
            var references = new References();

            if (File.Exists(pathFile)) {
                json = File.ReadAllText(pathFile, Encoding.UTF8);
                referencesList = JsonConvert.DeserializeObject<List<References>>(json);
            }
            comboBoxReferences.DataSource = referencesList.Where(x => x.Visible == true).ToList();
            comboBoxReferences.DisplayMember = "Name";
            comboBoxReferences.ValueMember = "Id";

        }
        private void ConcepttxtBox_MouseClick(object sender, MouseEventArgs e)
        {
            GetConcepts();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var Form3 = new Form3();
            Form3.Show();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBoxReferences_MouseClick(object sender, MouseEventArgs e)
        {
            GetReferences();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Validar()) {
                Adding = true;
                SaveRecord();
                Adding = false;
            } 
        }

        private bool Validar()
        {
            foreach (Control item in groupBox1.Controls)
            {
                    if ((item.Text == string.Empty || item.Text == null) && item.Name != textBoxDescripcion.Name) {
                        MessageBox.Show("Ha dejado campos vacíos.\nPor favor llenarlos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
            }
            return true;
        }

        private void SaveRecord()
        {
            var jsonTrans = string.Empty;
            var pathFileTrans = $"{AppDomain.CurrentDomain.BaseDirectory}\\transaction.json";
            var transactionList = new List<Transactions>();


            var transactions = new Transactions();
            if (File.Exists(pathFileTrans)) {
                jsonTrans = File.ReadAllText(pathFileTrans, Encoding.UTF8);
                transactionList = JsonConvert.DeserializeObject<List<Transactions>>(jsonTrans);
            }
            string palabra = comboBoxReferences.Text;
            if (Adding) {
                transactions = new Transactions
                {
                    ID = int.Parse(textBoxID.Text),
                    TypeTrans = comboBoxTypeTrans.Text,
                    Concept = ConcepttxtBox.Text,
                    Reference = comboBoxReferences.Text,
                    Amount = float.Parse(textBoxAmount.Text),
                    Moneda = comboBoxMoneda.Text,
                    DateOfTrans = dateTimeTrans.Value,
                    Descripcion = textBoxDescripcion.Text
                };
                transactionList.Add(transactions);
            } else if (Deleting) {
                int ID = int.Parse(dvgNormal.CurrentRow.Cells[1].Value.ToString());
                transactions = transactionList.FirstOrDefault(x => x.ID == ID);
                transactionList.Remove(transactions);
            } else if (Update && !Adding) {
                int ID = int.Parse(dvgNormal.CurrentRow.Cells[1].Value.ToString());
                deleteId = ID;
                transactions = transactionList.FirstOrDefault(x => x.ID == ID);

                ConcepttxtBox.Text = transactions.Concept.ToString();
                comboBoxReferences.Text = transactions.Reference.ToString();
                comboBoxMoneda.Text = transactions.Moneda.ToString();
                textBoxID.Text = transactions.ID.ToString();
                textBoxAmount.Text = transactions.Amount.ToString();
                comboBoxTypeTrans.Text = transactions.TypeTrans.ToString();
                try
                {
                    textBoxDescripcion.Text = transactions.Descripcion.ToString();
                }
                catch (NullReferenceException e) { 
                    textBoxDescripcion.Text = string.Empty;
                };
                return;
            }

            if (Update) {
                transactions = transactionList.FirstOrDefault(x => x.ID == deleteId);
                transactionList.Remove(transactions);
                Update = false;
                deleteId = 0;
            }

            jsonTrans = JsonConvert.SerializeObject(transactionList);
            var save = new StreamWriter(pathFileTrans, false, Encoding.UTF8);
            save.Write(jsonTrans);
            save.Close();


            dvgNormal.DataSource = transactionList.ToList();
            Clear();
            textBoxID.Text = GetMaxId(transactionList);
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dvgNormal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Deleting = true;
            SaveRecord();
            Deleting = false;
        }
        private void GetRecord() {
            textBoxID.Text = "1";
            var json = string.Empty;
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\transaction.json";

            if (File.Exists(pathFile))
            {
                json = File.ReadAllText(pathFile, Encoding.UTF8);
                var transList = JsonConvert.DeserializeObject<List<Transactions>>(json);
                dvgNormal.DataSource = transList;

                textBoxID.Text = GetMaxId(transList);
            }
        }

        private string GetMaxId(List<Transactions> transList)
        {
            int max = 0;
            foreach (var item in transList)
            {
                if (max<item.ID) { 
                    max = item.ID;
                }
            }
            return (max + 1).ToString();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            Update = true;
            comboBoxTypeTrans.Enabled = true;
            dateTimeTrans.Enabled = true;
            ConcepttxtBox.Enabled = true;
            comboBoxReferences.Enabled = true;
            textBoxDescripcion.Enabled = true;
            comboBoxMoneda.Enabled = true;
            textBoxAmount.Enabled = true;
            buttonNew.Enabled = false;
            buttonSave.Enabled = true;
            buttonDelete.Enabled = false;
            SaveRecord();
            buttonSave.Enabled = true;
        }

        private void Clear() {

            foreach (Control item in groupBox1.Controls)
            {
                if (item is not Label && item is not Button)
                {
                    item.Text = String.Empty;
                }
            }
            buttonUpdate.Enabled = true;
            buttonNew.Enabled = true;
            buttonSave.Enabled = false;
            buttonDelete.Enabled = true;
            Update = false;
            Adding = false;
            deleteId = 0;
            Deleting = false;
            comboBoxTypeTrans.Enabled = false;
            dateTimeTrans.Enabled = false;
            ConcepttxtBox.Enabled = false;
            comboBoxReferences.Enabled = false;
            textBoxDescripcion.Enabled = false;
            comboBoxMoneda.Enabled = false;
            textBoxAmount.Enabled = false;
            buttonNew.Enabled = true;
            buttonSave.Enabled = false;
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            comboBoxTypeTrans.Enabled = true;
            dateTimeTrans.Enabled = true;
            ConcepttxtBox.Enabled = true;
            comboBoxReferences.Enabled = true;
            textBoxDescripcion.Enabled = true;
            comboBoxMoneda.Enabled = true;
            textBoxAmount.Enabled = true;
            buttonNew.Enabled = false;
            buttonSave.Enabled = true;
        }
    }
}