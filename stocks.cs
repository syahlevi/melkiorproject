﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using System.Globalization;

namespace AKUNTING
{
    public partial class stocks : Form
    {
        public stocks()
        {
            InitializeComponent();
        }
        public int jmlstocks { get; set; }

        public void loaddata()
        {

            NpgsqlConnection ncon = new NpgsqlConnection(stringkoneksi.connection);
            NpgsqlCommand ncom = new NpgsqlCommand();
            ncom.Connection = ncon;
            ncom.CommandType = CommandType.Text;
            ncom.CommandText = "select*from stocks";
            DataSet ds = new DataSet();
            NpgsqlDataAdapter nda = new NpgsqlDataAdapter(ncom);
            nda.Fill(ds, "akunting");
            gridaccounts.DataSource = ds;
            gridaccounts.DataMember = "akunting";

            aturdatagrid();



        }

       

        private void aturdatagrid()
        {
            try
            {
                //mengatur tampilan panjang kolom otomatis menyesuaikan panjang character string data cell
                for (int i = 0; i < gridaccounts.Columns.Count - 1; i++)
                {
                    gridaccounts.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                gridaccounts.Columns[gridaccounts.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                for (int i = 0; i < gridaccounts.Columns.Count; i++)
                {
                    int colw = gridaccounts.Columns[i].Width;
                    gridaccounts.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    gridaccounts.Columns[i].Width = colw;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void stocks_Load(object sender, EventArgs e)
        {
            counts();
            loaddata();
        }

        public void counts()
        {
            //int a = DateTime.Now.Year;
            //int m = DateTime.ParseExact("Oktober", "MMMM", CultureInfo.CurrentCulture).Month;
            //int tot = Convert.ToInt32(this.lbtotop.Text);
            NpgsqlConnection ncon = new NpgsqlConnection(stringkoneksi.connection);
            ncon.Open();
            //var sql = "select sum(amount)  from costs where extract(year from tanggal) ='" + dttanggal.Value.Year + "' and extract(month from tanggal) ='" + dttanggal.Value.Month + "'";

            var sql = "select sum(amount)  from stocks";
            NpgsqlCommand ncom = new NpgsqlCommand(sql, ncon);
            NpgsqlDataReader dr = ncom.ExecuteReader();


            while (dr.Read())
            {
                if (!dr.IsDBNull(0))
                {
                    jmlstocks = dr.GetInt32(0);
                    lbjmlstocks.Text = jmlstocks.ToString("N0", new CultureInfo("en-US"));

                }
                else
                {

                    jmlstocks = 0;

                    lbjmlstocks.Text = jmlstocks.ToString("N0", new CultureInfo("en-US"));


                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            loaddata();
            counts();
        }
    }
}
