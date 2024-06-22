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

namespace Car_Entry
{
    public partial class Form1 : Form
    {
        SqlConnection cnct=new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=CarModels;Integrated Security=True");
        DataTable dt;
        SqlDataAdapter adapter;
        SqlCommand cmd;
        int carid;
        public Form1()
        {
            InitializeComponent();
            myCode();
            dgvt.Columns[0].Visible = false;
        
        }
        void myCode()
        {
            cnct.Open();
            dt = new DataTable();
            adapter = new SqlDataAdapter("select id,Carname,CarModel,Year from carDetails", cnct);
            adapter.Fill(dt);
            dgvt.DataSource = dt;
            cnct.Close();
        }
        void Clear()
        {
            textBoxCarName.Clear();
            textBoxModel.Clear();
            numericUpDownYear.Value = 1;
            textBoxCarName.Select();
        }
        private void buttonInsert_Click(object sender, EventArgs e)
        {
            
            if (textBoxCarName.Text!=string.Empty && textBoxModel.Text!=string.Empty && numericUpDownYear.Value!=0)
            {
                cnct.Open();
                cmd = new SqlCommand("INSERT INTO carDetails(CarName,CarModel,Year) Values(@name,@model,@year)", cnct);
                cmd.Parameters.AddWithValue("@name", textBoxCarName.Text);
                cmd.Parameters.AddWithValue("@model", textBoxModel.Text);
                cmd.Parameters.AddWithValue("@year", numericUpDownYear.Value);
                cmd.ExecuteNonQuery();
                cnct.Close();
                MessageBox.Show("insert Successfully!");
                myCode();
                
            }
            else
            {
                MessageBox.Show("Please Enter All Details!");
            }
            
            
            

        }

        private void dgvt_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            cnct.Open();
            carid =int.Parse(dgvt.Rows[e.RowIndex].Cells[0].Value.ToString());
            textBoxCarName.Text = dgvt.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBoxModel.Text= dgvt.Rows[e.RowIndex].Cells[2].Value.ToString();
            numericUpDownYear.Value=int.Parse(dgvt.Rows[e.RowIndex].Cells[3].Value.ToString());
            cnct.Close();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            
            if (textBoxCarName.Text != string.Empty && textBoxModel.Text != string.Empty && numericUpDownYear.Value != 0)
            {
                cnct.Open();
                cmd = new SqlCommand("UPDATE carDetails SET CarName=@name,CarModel=@model,Year=@year WHERE id=@Id", cnct);
                cmd.Parameters.AddWithValue("@Id", carid);
                cmd.Parameters.AddWithValue("@name", textBoxCarName.Text);
                cmd.Parameters.AddWithValue("@model", textBoxModel.Text);
                cmd.Parameters.AddWithValue("@year", numericUpDownYear.Value);
                cmd.ExecuteNonQuery();
                cnct.Close();
                MessageBox.Show("Updated Successfully!");
                myCode();
                Clear();


            }
            else
            {
                MessageBox.Show("Please Enter All Details!");
            }

            

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            
            if (textBoxCarName.Text != string.Empty && textBoxModel.Text != string.Empty)
            {
                cnct.Open();
                cmd = new SqlCommand("DELETE carDetails  WHERE Id=@Id", cnct);
                cmd.Parameters.AddWithValue("@Id", carid);
                cmd.Parameters.AddWithValue("@name", textBoxCarName.Text);
                cmd.Parameters.AddWithValue("@model", textBoxModel.Text);
                cmd.Parameters.AddWithValue("@year", numericUpDownYear.Value);
                cmd.ExecuteNonQuery();
                cnct.Close();
                MessageBox.Show("Data Deleted Successfully!");
                myCode();
                Clear();
            }
            else
            {
                MessageBox.Show("Please Select any data!");
                textBoxCarName.Select();
            }
            
        }
    }
}
