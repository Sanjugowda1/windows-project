using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        // insertion datagrid
        public void populateGrid()
        {
           
            try
            {

                SqlConnection con = new SqlConnection(@"Data Source=MA-4YNHZW2\SQLEXPRESS;Initial Catalog=MyPro;Integrated Security=True");
                string sql = "SELECT * FROM LOCATION_INVENTORY ";
               SqlDataAdapter dt = new SqlDataAdapter(sql, con);
                DataTable dta = new DataTable();
                dt.Fill(dta);
                DGView.DataSource = dta;
                DataGridViewCheckBoxColumn chkColumn = new DataGridViewCheckBoxColumn();
                chkColumn.HeaderText = "";
                chkColumn.Width = 35;
                chkColumn.Name = "chkColumn";
                DGView.Columns.Insert(0, chkColumn);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
                return;
            }


        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in DGView.Rows)
            {
                bool isSelected = Convert.ToBoolean(row.Cells["chkColumn"].Value);
                if (isSelected)
                {
                    using (SqlConnection cn = new SqlConnection(@"Data Source=MA-4YNHZW2\SQLEXPRESS;Initial Catalog=MyPro;Integrated Security=True"))
                    {

                        cn.Open();
                        using (DataTable dt = new DataTable("LOCATION_INVENTORY"))
                        {
                            String on_Hand = tbSave.Text;
                            using (SqlCommand cmd = new SqlCommand(" UPDATE LOCATION_INVENTORY SET ON_HAND_QUANTITY = '" + on_Hand + "' WHERE ON_HAND_QUANTITY=@ON_HAND_QUANTITY"
                               , cn))
                            {
                                //cmd.Parameters.AddWithValue("@INTERNAL_LOCATION_INV", row.Cells["INTERNAL_LOCATION_INV"].Value);
                                //cmd.Parameters.AddWithValue("@LOCATION", row.Cells["LOCATION"].Value);
                                //cmd.Parameters.AddWithValue("@ITEM", row.Cells["ITEM"].Value);
                                //cmd.Parameters.AddWithValue("@COMPANY", row.Cells["COMPANY"].Value);
                                //cmd.Parameters.AddWithValue("@WAREHOUSE", row.Cells["WAREHOUSE"].Value);
                                //cmd.Parameters.AddWithValue("@LOT", row.Cells["LOT"].Value);
                                //cmd.Parameters.AddWithValue("@EXPIRING_DATE", row.Cells["EXPIRING_DATE"].Value);
                                cmd.Parameters.AddWithValue("@ON_HAND_QUANTITY", tbSave.Text);
                                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                                adapter.Update(dt);
                                DGView.DataSource = dt;
                            }
                        }

                    }
                }
            }
               
            
        }

        private void DGView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }


        // SEARCH FUNCTION 
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(@"Data Source=MA-4YNHZW2\SQLEXPRESS;Initial Catalog=MyPro;Integrated Security=True"))
                {

                    cn.Open();
                    using (DataTable dt = new DataTable("LOCATION_INVENTORY"))
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT * FROM LOCATION_INVENTORY " +
                            "WHERE LOCATION like @LOCATION or ITEM LIKE @ITEM OR LOT=@LOT or ON_HAND_QUANTITY=@ON_HAND_QUANTITY"
                           , cn))
                        {
                            cmd.Parameters.AddWithValue("LOCATION", tbLocation.Text);
                            cmd.Parameters.AddWithValue("ITEM", string.Format("%{0}%", tbItem.Text));
                            cmd.Parameters.AddWithValue("LOT", tbLot.Text);
                            cmd.Parameters.AddWithValue("ON_HAND_QUANTITY", tbOnHandQuantity.Text);
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            adapter.Fill(dt);
                            DGView.DataSource = dt;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            populateGrid();
        }

        private void dbBtn_Click(object sender, EventArgs e)
        {

        }

        private void tbLocation_TextChanged(object sender, EventArgs e)
        {


        }

        private void tbLocation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                btnSearch.PerformClick();
        }
        // COPY FUNCTION  
        private void btnCopy_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in DGView.Rows)
            {
                bool isSelected = Convert.ToBoolean(row.Cells["chkColumn"].Value);
                if (isSelected)
                {

                    string connectionString = "Data Source=MA-4YNHZW2\\SQLEXPRESS;Initial Catalog=MyPro;Integrated Security=True";
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();

                        using (SqlCommand cmd = new SqlCommand("INSERT INTO INVENTORY_INFO values (@INTERNAL_LOCATION_INV, @LOCATION, @ITEM, @COMPANY, @WAREHOUSE, @LOT, @EXPIRING_DATE, @ON_HAND_QUANTITY)", con))
                        //using (SqlCommand cmd = new SqlCommand("UPDATE LOCATION_INVENTORY SET INTERNAL_LOCATION_INV = 1 where INTERNAL_LOCATION_INV = 19 ", con))

                        {
                            cmd.Parameters.AddWithValue("@INTERNAL_LOCATION_INV", row.Cells["INTERNAL_LOCATION_INV"].Value);
                            cmd.Parameters.AddWithValue("@LOCATION", row.Cells["LOCATION"].Value);
                            cmd.Parameters.AddWithValue("@ITEM", row.Cells["ITEM"].Value);
                            cmd.Parameters.AddWithValue("@COMPANY", row.Cells["COMPANY"].Value);
                            cmd.Parameters.AddWithValue("@WAREHOUSE", row.Cells["WAREHOUSE"].Value);
                            cmd.Parameters.AddWithValue("@LOT", row.Cells["LOT"].Value);
                            cmd.Parameters.AddWithValue("@EXPIRING_DATE", row.Cells["EXPIRING_DATE"].Value);
                            cmd.Parameters.AddWithValue("@ON_HAND_QUANTITY", row.Cells["ON_HAND_QUANTITY"].Value);

                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }

            }
            MessageBox.Show("copied");
        }

       
        } 
    }



