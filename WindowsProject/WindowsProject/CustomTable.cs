using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsProject
{
    public partial class CustomTable : Form
    {
        public CustomTable(string INTERNAL_LOCATION_INV, string LOCATION,string ITEM, string COMPANY, string WAREHOUSE , string LOT,  int ON_HAND_QUANTITY)
        {
            InitializeComponent();
            DGView2.Rows.Add();
            DGView2.Rows[0].Cells[0].Value = INTERNAL_LOCATION_INV;
            DGView2.Rows[0].Cells[1].Value = LOCATION;
            DGView2.Rows[0].Cells[2].Value = ITEM;
            DGView2.Rows[0].Cells[3].Value = COMPANY;
            DGView2.Rows[0].Cells[4].Value = WAREHOUSE;
            DGView2.Rows[0].Cells[5].Value = LOT;
            DGView2.Rows[0].Cells[6].Value = ON_HAND_QUANTITY;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
