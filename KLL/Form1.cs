using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace KLL
{
    public partial class Form1 : Form
    {
        //holders for id
        int nextId;
        int nextClassId;
  

        public Form1()
        {
            InitializeComponent();
        }

        private void staffBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
          

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            try
            { 
                this.classInfoTableAdapter.Fill(this.lknobbe285DataSet.ClassInfo);
                this.clientTableAdapter.Fill(this.lknobbe285DataSet.Client);
                this.staffTableAdapter.Fill(this.lknobbe285DataSet.Staff);
               //gets max ids in each table 
                clientIDGetter();
                classIDGetter();

                //fills client dgv
                //loadDataIntoDVG();

                //makes week the default for the DGV while showing the data on startup
                radioButton2.Checked = true;

            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
           
            }



        }

        private void staffBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            this.staffBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.lknobbe285DataSet);

        }


        //add client 
        private void button1_Click(object sender, EventArgs e)
        {
            nextId++;
            try {
                string clientname = clientNameTextBox1.Text;
                int age = int.Parse(ageTextBox1.Text);
                string gender = genderTextBox1.Text;

                String strCnn = "Data Source=CISSQL;Initial Catalog=lknobbe285;Integrated Security=True";
                using (SqlConnection cnn = new SqlConnection(strCnn))
                {

                    cnn.Open();
                    String cmd = "INSERT INTO [Client] (ClientID, ClientName, Age, Gender)"
                        + "VALUES (" + nextId + ", '"+ clientname + "', " + age + ", '" + gender + "' " + ")";
                    SqlCommand sqlcmd = new SqlCommand(cmd, cnn);
                    sqlcmd.ExecuteNonQuery();
                    cnn.Close();

                }
                
                MessageBox.Show("You have added " + clientname);

            }  catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }

        //Day view
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            DateTime todayNow = DateTime.Today.Date;
            string todayStr = todayNow.ToString("yyyy-MM-dd");
            int staff = int.Parse(comboBox1.Text);
            try
            {

                if (radioButton1.Checked)
                {
                    
                    string strCnn = "Data Source=CISSQL;Initial Catalog=lknobbe285;Integrated Security=True";

                    using (SqlConnection cnn = new SqlConnection(strCnn))
                    {
                        cnn.Open();

                        string cmd = "SELECT [Date], [Time], [ClientName]" +
                                     " FROM [ClassInfo]" +
                                     " JOIN [Client] ON ClassInfo.ClientID = Client.ClientID" +
                                     " WHERE [StaffID] = " + staff +
                                     " AND [Date] = '" + todayStr + "'";
                        SqlDataAdapter da = new SqlDataAdapter(cmd, cnn);

                   
                        DataTable dt = new DataTable();

                        da.Fill(dt);

                        dataGridView1.DataSource = dt;

                        cnn.Close();
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //week view
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            DateTime todayNow = DateTime.Today.Date;
            DateTime nextWeek = todayNow.AddDays(7);
            string nextWeekStr = nextWeek.ToString("yyyy-MM-dd");
            string todayStr = todayNow.ToString("yyyy-MM-dd");
            int staff = int.Parse(comboBox1.Text);

            try
            {

                if (radioButton2.Checked)
                {

                    string strCnn = "Data Source=CISSQL;Initial Catalog=lknobbe285;Integrated Security=True";

                    using (SqlConnection cnn = new SqlConnection(strCnn))
                    {
                        cnn.Open();

                        string cmd = "SELECT [Date], [Time], [ClientName]" +
                                     " FROM [ClassInfo]" +
                                     " JOIN [Client] ON ClassInfo.ClientID = Client.ClientID" +
                                     " WHERE [StaffID] = " + staff +
                                     " AND [Date] BETWEEN '" + todayStr + "' AND '" + nextWeekStr + "'";

                        SqlDataAdapter da = new SqlDataAdapter(cmd, cnn);


                        DataTable dt = new DataTable();

                        da.Fill(dt);

                        dataGridView1.DataSource = dt;

                        cnn.Close();

                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //month view
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            DateTime todayNow = DateTime.Today.Date;
            DateTime nextMonth = todayNow.AddDays(30);
            string todayStr = todayNow.ToString("yyyy-MM-dd");
            string nextMonthStr = nextMonth.ToString("yyyy-MM-dd");
            int staff = int.Parse(comboBox1.Text);
            try
            {

                if (radioButton3.Checked)
                {

                    string strCnn = "Data Source=CISSQL;Initial Catalog=lknobbe285;Integrated Security=True";

                    using (SqlConnection cnn = new SqlConnection(strCnn))
                    {
                        cnn.Open();

                        string cmd = "SELECT [Date], [Time], [ClientName]" +
                                     " FROM [ClassInfo]" +
                                     " JOIN [Client] ON ClassInfo.ClientID = Client.ClientID" +
                                     " WHERE [StaffID] = " + staff +
                                     " AND [Date] BETWEEN '" + todayStr + "' AND '" + nextMonthStr + "'";
                        SqlDataAdapter da = new SqlDataAdapter(cmd, cnn);


                        DataTable dt = new DataTable();

                        da.Fill(dt);

                        dataGridView1.DataSource = dt;

                        cnn.Close();
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //all time view
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            int staff = int.Parse(comboBox1.Text);
            try
            {

                if (radioButton4.Checked)
                {

                    string strCnn = "Data Source=CISSQL;Initial Catalog=lknobbe285;Integrated Security=True";

                    using (SqlConnection cnn = new SqlConnection(strCnn))
                    {
                        cnn.Open();

                        string cmd = "SELECT [Date], [Time], [ClientName]" +
                                     " FROM [ClassInfo]" +
                                     " JOIN [Client] ON ClassInfo.ClientID = Client.ClientID" +
                                     " WHERE [StaffID] = " + staff;
                        SqlDataAdapter da = new SqlDataAdapter(cmd, cnn);


                        DataTable dt = new DataTable();

                        da.Fill(dt);

                        dataGridView1.DataSource = dt;

                        cnn.Close();
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        

      //add class
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                nextClassId++;
                DateTime date = dateTimePicker1.Value;
                string staffName = (string)comboBox5.Text;
                /*string year = (string)comboBox6.Text;
                string month = (string)comboBox9.Text;
                string day = (string)comboBox7.Text;
                string date = year + "-" + month + "-" + day;*/

                int time = int.Parse((string)comboBox8.Text);
                int ClientID = int.Parse((string)comboBox3.Text);
                int staffID = int.Parse((string)comboBox10.Text);

                //validation 
                bool staffValidations = StaffValidation(time, date, staffID);
                bool clientValidations = clientValidation(time, date, ClientID);

                if (staffValidations && clientValidations)
                {
                    String strCnn = "Data Source=CISSQL;Initial Catalog=lknobbe285;Integrated Security=True";
                    using (SqlConnection cnn = new SqlConnection(strCnn))
                    {

                        cnn.Open();
                        
                        String cmd = "INSERT INTO [ClassInfo] (ClassID, [Date], [Time], ClientID, StaffID)"
                            + "VALUES (" + nextClassId + ", '" + date + "', " + time + ", " + ClientID + ", " + staffID + " " + ")";
                        SqlCommand sqlcmd = new SqlCommand(cmd, cnn);
                        sqlcmd.ExecuteNonQuery();

                        cnn.Close();
                        MessageBox.Show("Class has been successfully added!");
                        
                    }
                } else
                {
                    MessageBox.Show("Sorry, this time and date has already been booked!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //load data into client dgv
       /* private void loadDataIntoDVG() {
            int client = int.Parse(comboBox3.Text);
            String strCnn = "Data Source=CISSQL;Initial Catalog=lknobbe285;Integrated Security=True";
            using (SqlConnection cnn = new SqlConnection(strCnn))
            {
                cnn.Open();

                string cmd = "SELECT [Date], [Time], [StaffName]" +
                             " FROM [ClassInfo]" +
                             " JOIN [Staff] ON ClassInfo.StaffID = Staff.StaffID" +
                             " WHERE [ClientID] = " + client;
                SqlDataAdapter da = new SqlDataAdapter(cmd, cnn);


                DataTable dt = new DataTable();

                da.Fill(dt);

                classInfoDataGridView.DataSource = dt;

                cnn.Close();
            }


        }*/

        //validation for staff
        private bool StaffValidation(int time, DateTime date, int staffid) {
            try
            {
                
                string strCnn = "Data Source=CISSQL;Initial Catalog=lknobbe285;Integrated Security=True";
                int rowcount = 0;

                using (SqlConnection cnn = new SqlConnection(strCnn))
                {
                    cnn.Open();

                    string sqlcmd = "SELECT [StaffName]" +
                                 " FROM [ClassInfo]" +
                                 " JOIN [Staff] ON ClassInfo.StaffID = Staff.StaffID" +
                                 " WHERE Staff.StaffID = " + staffid +
                                 " AND ([Date] = '" +  date +
                                 "' AND [Time] = " + time + ")";
                    SqlCommand cmd = new SqlCommand(sqlcmd, cnn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows) { 
                        while (reader.Read()) {
                            rowcount++;
                        }
                    }

                    cnn.Close();

                    if (rowcount == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            
        }


        //validation for client
        private bool clientValidation(int time, DateTime date, int clientid)
        {
            try
            {

                string strCnn = "Data Source=CISSQL;Initial Catalog=lknobbe285;Integrated Security=True";
                int rowcount = 0;

                using (SqlConnection cnn = new SqlConnection(strCnn))
                {
                    cnn.Open();

                    string sqlcmd = "SELECT [ClientName]" +
                                 " FROM [ClassInfo]" +
                                 " JOIN [Client] ON ClassInfo.ClientID = Client.ClientID" +
                                 " WHERE Client.ClientID = " + clientid +
                                 " AND ([Date] = '" + date +
                                 "' AND [Time] = " + time + ")";
                    SqlCommand cmd = new SqlCommand(sqlcmd, cnn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            rowcount++;
                        }
                    }

                    cnn.Close();

                    if (rowcount == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }


        //get max class id 
        public void classIDGetter() {
            try
            {

                string strCnn = "Data Source=CISSQL;Initial Catalog=lknobbe285;Integrated Security=True";

                using (SqlConnection cnn = new SqlConnection(strCnn))
                {
                    cnn.Open();

                    string sqlcmd = "SELECT MAX([ClassID])" +
                                 " FROM [ClassInfo]";
                    SqlCommand cmd = new SqlCommand(sqlcmd, cnn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        nextClassId = int.Parse(reader[0].ToString());
                    }

                    cnn.Close();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //get max class id 
        public void clientIDGetter()
        {
            try
            {

                string strCnn = "Data Source=CISSQL;Initial Catalog=lknobbe285;Integrated Security=True";

                using (SqlConnection cnn = new SqlConnection(strCnn))
                {
                    cnn.Open();

                    string sqlcmd = "SELECT MAX([ClientID])" +
                                 " FROM [Client]";
                    SqlCommand cmd = new SqlCommand(sqlcmd, cnn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        nextId = int.Parse(reader[0].ToString());
                    }

                    cnn.Close();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int client = int.Parse(comboBox3.Text);
            String strCnn = "Data Source=CISSQL;Initial Catalog=lknobbe285;Integrated Security=True";
            using (SqlConnection cnn = new SqlConnection(strCnn))
            {
                cnn.Open();

                string cmd = "SELECT [Date], [Time], [StaffName]" +
                             " FROM [ClassInfo]" +
                             " JOIN [Staff] ON ClassInfo.StaffID = Staff.StaffID" +
                             " WHERE [ClientID] = " + client;
                SqlDataAdapter da = new SqlDataAdapter(cmd, cnn);


                DataTable dt = new DataTable();

                da.Fill(dt);

                classInfoDataGridView.DataSource = dt;

                cnn.Close();
            }
        }
    }
}
