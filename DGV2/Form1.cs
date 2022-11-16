﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DVG2
{
    public partial class Form1 : Form
    {
        private Nigget.dvg23<Abiturient> dvg23 = new Nigget.dvg23<Abiturient>();
        private readonly BindingSource bindingSource;
        public Form1()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            bindingSource = new BindingSource();
            bindingSource.DataSource = dvg23.abiturientst();
            dataGridView1.DataSource = bindingSource;
        }
        private void dataGridView1_SelectionChanged (object sender, EventArgs e)
        {
            Change.Enabled = Delete.Enabled = dataGridView1.SelectedRows.Count > 0;
            Change2.Enabled = Delete2.Enabled = dataGridView1.SelectedRows.Count > 0;
        }
        private void dataGridView1_CellFormat (object sender, DataGridViewCellFormattingEventArgs e)
        {
            var abitur = (Abiturient)dataGridView1.Rows[e.RowIndex].DataBoundItem;
            if (dataGridView1.Columns[e.RowIndex].Name == "Gender_Column")
            {
                var val = (Gender)e.Value;
                switch (val)
                {
                    case Gender.Male:
                        e.Value = "Мужской";
                        break;
                    case Gender.Female:
                        e.Value = "Женкский";
                        break;
                }
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "FormaObychenia_Column")
            {
                var vall = (FormaObychenia)e.Value;
                switch (vall)
                {
                    case (FormaObychenia.Ochnoe):
                        e.Value = "Очное";
                        break;
                    case (FormaObychenia.Ocno_zaochnoe):
                        e.Value = "Очно=заочное";
                        break;
                    case (FormaObychenia.Zaochnoe):
                        e.Value = "Заочное";
                        break;
                }
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Sum_Column")
            {
                e.Value = Math.Round(abitur.Matem + abitur.Rus + abitur.Inf);
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void About_Program_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Разработчик: Горбулич Дмитрий Евгеньевич. Группа: ИП-20-3", "Программа",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Add_Click(object sender, EventArgs e)
        {
            var infoform = new Abiturienttinfo_form();
            infoform.Text = "Добавить абитуриента";
            if (infoform.ShowDialog(this) == DialogResult.OK)
            {
                dvg23.Addbit(infoform.Abiturient);
                infoform.Abiturient.Id = Guid.NewGuid();
                bindingSource.ResetBindings(false);
                CalculateStatus();
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            var data = (Abiturient)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].DataBoundItem;
            if (MessageBox.Show($"Вы точно хотите удалить? '{data.FullName}'?",
                "Удаление записи",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                
                dvg23.Deletebit(data);
                bindingSource.ResetBindings(false);
                CalculateStatus();
            }
        }

        private void Change_Click(object sender, EventArgs e)
        {
            var data = (Abiturient)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].DataBoundItem;
            var infoForm = new Abiturienttinfo_form(data);
            infoForm.Text = "Редактирование абитуриента";
            if (infoForm.ShowDialog(this) == DialogResult.OK)
            {
                dvg23.Changebit(infoForm.Abiturient, data);
                bindingSource.ResetBindings(false);
                CalculateStatus();
            }
        }
        public void CalculateStatus()
        {
            lblRang150.Text = $"Студенты, набравшие больше 150 баллов: " +
            dvg23.abiturientst().Where(abitur => (abitur.Matem + abitur.Rus + abitur.Inf) > 150)
            .Count()
            .ToString();
        }

        private void Change2_Click(object sender, EventArgs e)
        {
            Change.PerformClick();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
