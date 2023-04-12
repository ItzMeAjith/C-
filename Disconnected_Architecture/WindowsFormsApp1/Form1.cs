﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        DataSet ds;
        SqlDataAdapter ad;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con = new SqlConnection("data source=DESKTOP-GL7RKG7\\SQLEXPRESS; database=CIET;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true");
            cmd = new SqlCommand("SELECT * FROM Students", con);
            ad = new SqlDataAdapter(cmd);
            ds = new DataSet();
            DataTable tbl=new DataTable();
            ad.Fill(tbl);
            //ad.Fill(ds, "Students");
            //tbl = ds.Tables["Students"];
            if (!ds.Tables.Contains("Students"))
            {
                // Create the Employee table
                DataTable employeeTable = new DataTable("Students");
                employeeTable.Columns.Add("Rollno", typeof(int));
                employeeTable.Columns.Add("Name", typeof(string));
                employeeTable.Columns.Add("Dept", typeof(string));
                employeeTable.Columns.Add("CGPA", typeof(double));
                ds.Tables.Add(employeeTable);
            }
            dataGridView1.DataSource = tbl;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            con = new SqlConnection("data source=DESKTOP-GL7RKG7\\SQLEXPRESS; database=CIET;Integrated security=SSPI");
            
            DataRow dr = ds.Tables["Students"].NewRow();
            dr[0] = Int32.Parse(roll.Text);
            dr[1] = name.Text;
            dr[2] = dept.Text;
            dr[3] = Double.Parse(cgpa.Text);

            ds.Tables["Students"].Rows.Add(dr);
            SqlCommandBuilder bld = new SqlCommandBuilder(ad);
            ad.Update(ds, "Students");

            MessageBox.Show("New record added successfully.");

        }
    }
}
