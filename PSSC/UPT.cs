﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PSSC
{
    public partial class UPT : Form
    {
        public UPT()
        {
            InitializeComponent();
        }

        private void saveStudenti(object sender, EventArgs e)
        {
            this.Validate();
            this.studentiBindingSource.EndEdit();
            this.studentiTableAdapter.Update(this.databaseDataSet.Studenti);
        }

        private void saveProfesori(object sender, EventArgs e)
        {
            this.Validate();
            this.profesoriBindingSource.EndEdit();
            this.profesoriTableAdapter.Update(this.databaseDataSet.Profesori);
            this.materii_predateTableAdapter.Update(this.databaseDataSet.Materii_predate);
        }

        private void UPTLoad(object sender, EventArgs e)
        {
            this.materii_predateTableAdapter.Fill(this.databaseDataSet.Materii_predate);
            this.profesoriTableAdapter.Fill(this.databaseDataSet.Profesori);
            this.noteTableAdapter.Fill(this.databaseDataSet.Note);
            this.studentiTableAdapter.Fill(this.databaseDataSet.Studenti);
            label3.Text = "Medie: " + medie(noteDataGridView);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            studentiBindingSource.Filter = "CONVERT(Nr_matricol, 'System.String') LIKE '" + textBox1.Text + "%'";
            label4.Text= "Medie: " + medie(noteDataGridView1);
            if (textBox1.Text != "")
                panel1.Hide();
            else
                panel1.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            studentiBindingSource.Filter = "CONVERT(Nr_matricol, 'System.String') LIKE '" + textBox2.Text + "%'";
            if (textBox2.Text != "")
                panel2.Hide();
            else
                panel2.Show();
        }

        private double medie(DataGridView dgv)
        {
            int sum = 0;
            for (int i = 0; i < dgv.Rows.Count; i++)
                sum += Convert.ToInt32(dgv.Rows[i].Cells[1].Value);
            return Math.Round((double)sum / (dgv.RowCount - 1),2);
        }

        private void studentiPositionChanged(object sender, EventArgs e)
        {
            label3.Text = "Medie: " + medie(noteDataGridView);
        }

        private void save(object sender, EventArgs e)
        {
            this.Validate();
            this.noteBindingSource.EndEdit();
            this.noteTableAdapter.Update(this.databaseDataSet.Note);
        }

        private void afiseazaStudentiCazati(object sender, EventArgs e)
        {
            int nr = Convert.ToInt32(textBox3.Text);
            double min = Convert.ToDouble(textBox4.Text);
            double max = Convert.ToDouble(textBox5.Text);
            List<FisaCazare> fise = new List<FisaCazare>();
            foreach (DataRowView drv in studentiBindingSource.List)
            {
                int suma = 0;
                int n = 0;
                foreach (DataRow dr in databaseDataSet.Tables[1].Rows)
                    if (drv["Nr_matricol"].ToString().Equals(dr["Nr_matricol"].ToString()))
                    {
                        suma += Convert.ToInt32(dr["Nota"].ToString());
                        n++;
                    }
                FisaCazare fisa = new FisaCazare(drv["Nume"].ToString(), drv["Prenume"].ToString(), drv["Facultate"].ToString(), Convert.ToInt32(drv["An"].ToString()), Math.Round((double)suma / n,2));
                fise.Add(fisa);
            }
            List<FisaCazare> SortedList = fise.OrderByDescending(o => o.Medie).ToList().FindAll(c => (c.Medie<=max) && (c.Medie>=min));
            while (SortedList.Count > nr)
                SortedList.RemoveAt(SortedList.Count - 1);
            var bindingList = new BindingList<FisaCazare>(SortedList);
            var source = new BindingSource(bindingList, null);
            dataGridView1.DataSource = source;
        }
    }
}
