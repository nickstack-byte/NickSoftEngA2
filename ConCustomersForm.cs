using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace NickSoftEngA2
{
    public partial class ConCustomers : Form
    {
        System.Data.SqlClient.SqlConnection newCon;
        DataSet dsClients;
        System.Data.SqlClient.SqlDataAdapter daClient;
        public int whichRec = 0;
        public int countRecs = 0;

        public ConCustomers()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        { 
            String connectString = ConfigurationManager.ConnectionStrings["BayWynConnectionString"].ConnectionString;
            newCon = new System.Data.SqlClient.SqlConnection(connectString);
            
            dsClients = new DataSet();

            newCon.Close();

            dsClients = new DataSet();

            String sqlGetClient;
            sqlGetClient = "SELECT * From ContractCustomers";

            daClient = new System.Data.SqlClient.SqlDataAdapter(sqlGetClient, newCon);

            daClient.Fill(dsClients, "Clients");
            countRecs = dsClients.Tables["Clients"].Rows.Count;

            MoveRecords();

        }

        private void MoveRecords() // populates form with data from cabin table
        {
            DataRow clients = dsClients.Tables["Clients"].Rows[whichRec];
             
            textBoxID.Text = clients[0].ToString();
            textBoxBizName.Text = clients[1].ToString();
            textBoxAdd1.Text = clients[2].ToString();
            textBoxAdd2.Text = clients[3].ToString();
            textBoxPoCo.Text = clients[4].ToString();
            textBoxPhone.Text = clients[5].ToString();
            textBoxEmail.Text = clients[6].ToString();
            textBoxJobID.Text = clients[7].ToString();
            textBoxMCost.Text = clients[8].ToString();
            textBoxJCost.Text = clients[9].ToString();
            textBoxNotes.Text = clients[10].ToString();



        }

        private void button1st_Click(object sender, EventArgs e)
        {
            whichRec = 0;
            MoveRecords();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (whichRec < countRecs - 1)
            {
                whichRec++;
                MoveRecords();
            }
            else
            {
                MessageBox.Show("You have reached the last entry.");
            }
        }

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            if (whichRec > 0)
            {
                whichRec--;
                MoveRecords();
            }
            else
            {
                MessageBox.Show("You are at the first entry.");
            }
        }

        private void buttonLast_Click(object sender, EventArgs e)
        {
            whichRec = countRecs - 1;
            MoveRecords();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            textBoxID.Clear();     //clears text boxes
            textBoxBizName.Clear();
            textBoxAdd1.Clear();
            textBoxAdd2.Clear();
            textBoxPoCo.Clear();
            textBoxPhone.Clear();
            textBoxEmail.Clear();
            textBoxJobID.Clear();
            textBoxMCost.Clear();
            textBoxJCost.Clear();
            textBoxNotes.Clear();

            textBoxID.Hide(); // hides ID as auto-increment
            button1st.Enabled = false; // stops button functions
            buttonNext.Enabled = false;
            buttonPrev.Enabled = false;
            buttonLast.Enabled = false;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            DataRow clients = dsClients.Tables["Clients"].NewRow();

            clients[1] = textBoxBizName.Text;
            clients[2] = textBoxAdd1.Text;
            clients[3] = textBoxAdd2.Text;
            clients[4] = textBoxPoCo.Text;
            clients[5] = textBoxPhone.Text;
            clients[6] = textBoxEmail.Text;
            //clients[7] = textBoxJobID.Text;
            clients[8] = textBoxMCost.Text;
            clients[9] = textBoxJCost.Text;
            clients[10] = textBoxNotes.Text;

            dsClients.Tables["Clients"].Rows.Add(clients);

            countRecs++;
            whichRec = countRecs - 1;

            textBoxID.Show(); // hides ID as auto-increment
            button1st.Enabled = true; // stops button functions
            buttonNext.Enabled = true;
            buttonPrev.Enabled = true;
            buttonLast.Enabled = true;

            System.Data.SqlClient.SqlCommandBuilder myUpdateDB; // updates database
            myUpdateDB = new System.Data.SqlClient.SqlCommandBuilder(daClient);
            myUpdateDB.DataAdapter.Update(dsClients.Tables["Clients"]);

            MoveRecords();

        }
    }
}
