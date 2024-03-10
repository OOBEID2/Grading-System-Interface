using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace FinalProject
{  
    public partial class Form2 : Form
    {
        SQLiteConnection db_connect;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
                db_connect = new SQLiteConnection("Data Source = vp-project_new.sqlite; Version = 3;");
                db_connect.Open();
                MessageBox.Show("Database connected");
            
            tabPage1.BackColor = Color.LightSteelBlue;
            tabPage2.BackColor = Color.LightSteelBlue;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
            try
            {
                string sql;
                sql = $"DELETE FROM Information WHERE ID = '{int.Parse(txtSearch.Text)}'";
                SQLiteCommand cmd = new SQLiteCommand(sql, db_connect);
                int p = cmd.ExecuteNonQuery();
                MessageBox.Show(p + " Deleted Correctly");
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                Student n = new Student();

                if (string.IsNullOrEmpty(txtFName.Text) || string.IsNullOrEmpty(txtLName.Text) || string.IsNullOrEmpty(txtLName.Text) || string.IsNullOrEmpty(txtID.Text) || string.IsNullOrEmpty(txtEmail.Text) || (!rbSW.Checked && !rbComputer.Checked && !rbGraphic.Checked))
                {
                    MessageBox.Show("Please fill in all required fields.");
                    
                }
                else
                {
                    n.FirstName = txtFName.Text;
                    n.LastName = txtLName.Text;
                    n.Id = int.Parse(txtID.Text);
                    n.Email = txtEmail.Text;

                    if (rbSW.Checked)
                    {
                        n.Major = "Software Engineering";
                    }
                    else if (rbComputer.Checked)
                    {
                        n.Major = "Computer Science";
                    }
                    else if (rbGraphic.Checked)
                    {
                        n.Major = "Graphic Design";
                    }

                }

                n.Assessments = int.Parse(numericAss.Value.ToString());
                n.Midterm = int.Parse(numericMid.Value.ToString());
                n.Final = int.Parse(numericFinal.Value.ToString());
                n.Grade = n.Assessments + n.Midterm + n.Final;
                
                string sql;
                sql = $"INSERT INTO Information values ('{n.FirstName}', '{n.LastName}', '{n.Id}', '{n.Major}', '{n.Email}', '{n.Assessments}', '{n.Midterm}','{n.Final}', '{n.Grade}')";
                SQLiteCommand cmd = new SQLiteCommand(sql, db_connect);
                int p = cmd.ExecuteNonQuery();
                MessageBox.Show(p + " Insereted Successfully");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try 
            {
                Student update = new Student();
                update.Assessments = int.Parse(numericAss.Value.ToString());
                update.Midterm = int.Parse(numericMid.Value.ToString());
                update.Final = int.Parse(numericFinal.Value.ToString());
                update.Grade = update.Assessments + update.Midterm + update.Final;
                string sql;
                sql = $"UPDATE Information SET Assessments = '{update.Assessments}',Midterm = '{update.Midterm}',Final = '{update.Final}',FinalGrade = '{update.Grade} ' WHERE ID = '{int.Parse(txtSearch.Text)}'";
                SQLiteCommand cmd = new SQLiteCommand(sql, db_connect);
                int p = cmd.ExecuteNonQuery();
                MessageBox.Show(p + " Updated Successfully");
            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void readFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var reader = new StreamReader("Std_info.txt");
                int i = 0;
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    string[] cell = line.Split(',');

                    string fname = cell[0];
                    string lname = cell[1];
                    int id = int.Parse(cell[2]);
                    string major = cell[3];
                    string email = cell[4];
                    int assessments = int.Parse(cell[5]);
                    int midterm = int.Parse(cell[6]);
                    int final = int.Parse(cell[7]);
              
                    int total = midterm + final + assessments;
                    
                    string sql = $"insert into Information values ('{fname}', '{lname}', '{id}', '{major}', '{email}', '{assessments}', '{midterm}','{final}', '{total}')"; 
                    SQLiteCommand command = new SQLiteCommand(sql, db_connect);
                    //int x =
                    command.ExecuteNonQuery();
                    i++;
                }
                MessageBox.Show($"{i} Students where inserted successfuly from std_info.txt");
                reader.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void writeToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var reader = new StreamReader("std_info.txt");
                var writer = new StreamWriter("std_from_db.txt");
                int i = 0;

                while (!reader.EndOfStream)
                {
                    string s = reader.ReadLine();
                    writer.WriteLine(s);

                }

                MessageBox.Show("Data inserted successfuly");
                reader.Close();
                writer.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message);
            }
        }

        private void showRecordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "select * from Information order by ID;";
                SQLiteCommand command = new SQLiteCommand(sql, db_connect);
                SQLiteDataReader reader = command.ExecuteReader();

                SQLiteDataAdapter adap = new SQLiteDataAdapter(sql, db_connect);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Refresh();



                MessageBox.Show("Showing records executed successfully");
            }
            catch (Exception ex) 
            { MessageBox.Show(ex.Message); 
            }
        }

        private void deleteRecordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sql = "delete from Information";
            SQLiteCommand command = new SQLiteCommand(sql, db_connect);
            
            int x = command.ExecuteNonQuery();
            MessageBox.Show($"{x} Deleted successfully");
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void exitProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void contactUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("+962 7841 263", "Our phone number", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }


        private void btnHighest_Click(object sender, EventArgs e)
        {
            string sql = "select FirstName, LastName, Major, Assessments, Midterm, Final, FinalGrade from Information where FinalGrade = (select max(FinalGrade) from Information);";
            SQLiteCommand command = new SQLiteCommand(sql, db_connect);
            SQLiteDataReader reader = command.ExecuteReader();

            txtNameRead.Text = reader[0] + " " + reader[1];
            txtMajorRead.Text = reader[2].ToString();
            txtAssessmentsRead.Text = reader[3].ToString();
            txtMidtermRead.Text = reader[4].ToString();
            txtFinalRead.Text = reader[5].ToString();
            txtTotalRead.Text = reader[6].ToString();

        }

        private void btnLowest_Click(object sender, EventArgs e)
        {
            string sql = "select FirstName, LastName, Major, Assessments, Midterm, Final, FinalGrade from Information where FinalGrade = (select min(FinalGrade) from Information);";
            SQLiteCommand command = new SQLiteCommand(sql, db_connect);
            SQLiteDataReader reader = command.ExecuteReader();

            txtNameRead.Text = reader[0] + " " + reader[1];
            txtMajorRead.Text = reader[2].ToString();
            txtAssessmentsRead.Text = reader[3].ToString();
            txtMidtermRead.Text = reader[4].ToString();
            txtFinalRead.Text = reader[5].ToString();
            txtTotalRead.Text = reader[6].ToString();
        }

        private void btnAvg_Click(object sender, EventArgs e)
        {
            string sql = "select FinalGrade from Information;";
            SQLiteCommand command = new SQLiteCommand(sql, db_connect);
            SQLiteDataReader reader = command.ExecuteReader();

            int sum = 0, cntr = 0;
            while(reader.Read())
            {
                sum += int.Parse(reader[0].ToString());
                cntr++;
            }

            float avg = (float)sum / cntr;

            txtAvgRead.Text = avg.ToString();  
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dataGridToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
