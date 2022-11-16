using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataGridView_Kisel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DataGridView_Kisel
{
    public partial class Form1 : Form
    {
        public DbContextOptions<Context> opt;
        public Form1()
        {
            InitializeComponent();
            opt = Json.Option();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = ReadDb(opt);
        }
        private void ToolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ToolStripMenuItemProg_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа, сделанная Кисель А.И. ", "О программе",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            var stdInfoForm = new FormStudentInfo();
            stdInfoForm.Text = "Добавление студента";
            if (stdInfoForm.ShowDialog(this) == DialogResult.OK)
            {
                CreateDb(opt, stdInfoForm.Student);
                dataGridView1.DataSource = ReadDb(opt);
                stdInfoForm.Student.Id = Guid.NewGuid();
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "sumbal")
            {
                var data = (Student)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                e.Value = data.Russia + data.Math + data.Inform;
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "GenderColumn")
            {
                var val = (Gender)e.Value;
                switch (val)
                {
                    case Gender.Male:
                        e.Value = "Мужчина";
                        break;
                    case Gender.Female:
                        e.Value = "Женщина";
                        break;
                }
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "FormStudyCollumn")
            {
                var val = (FormStudy)e.Value;
                switch (val)
                {
                    case FormStudy.Och:
                        e.Value = "Очная";
                        break;
                    case FormStudy.Zaoch:
                        e.Value = "Заочная";
                        break;
                    case FormStudy.Och_Zaoch:
                        e.Value = "Очно-заочная";
                        break;
                }
            }
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 &&
                e.RowIndex >= 0 &&
                dataGridView1.Columns[e.ColumnIndex].Name == "AvgRColumn")
            {
                e.PaintBackground(e.ClipBounds, true);
                var val = decimal.Parse(e.Value.ToString());
                var resultW = (float)(val * e.CellBounds.Width) / 5.0f;
                if (val <= 2)
                {
                    e.Graphics.FillRectangle(Brushes.Red, e.CellBounds.X, e.CellBounds.Y, resultW, e.CellBounds.Height);
                }
                else if (2 < val && val <= 4)
                {
                    e.Graphics.FillRectangle(Brushes.Yellow, e.CellBounds.X, e.CellBounds.Y, resultW, e.CellBounds.Height);
                }
                else if (val > 4)
                {
                    e.Graphics.FillRectangle(Brushes.Green, e.CellBounds.X, e.CellBounds.Y, resultW - 1, e.CellBounds.Height - 1);
                }
                //e.Graphics.DrawRectangle(Pens.Red, e.CellBounds.X, e.CellBounds.Y, resultW, e.CellBounds.Height);
                e.Handled = true;
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            toolStripButtonChange.Enabled =
                toolStripButtonDelete.Enabled =
                ToolStripMenuItemChange.Enabled =
                ToolStripMenuItemDelete.Enabled =
                dataGridView1.SelectedRows.Count > 0;
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            var data = (Student)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].DataBoundItem;
            if (MessageBox.Show($"Вы действительно хотите удалить '{data.FullName}'?",
                    "Удаление записи",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                RemoveDb(opt, data);
                dataGridView1.DataSource = ReadDb(opt);
            }
        }

        private void toolStripButtonChange_Click(object sender, EventArgs e)
        {
            var data = (Student)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].DataBoundItem;
            var infoForm = new FormStudentInfo(data);
            infoForm.Text = "Редактирование студента";
            if (infoForm.ShowDialog(this) == DialogResult.OK)
            {
                data.FullName = infoForm.Student.FullName;
                data.Gender = infoForm.Student.Gender;
                data.BirthDay = infoForm.Student.BirthDay;
                data.formStudy = infoForm.Student.formStudy;
                data.Math = infoForm.Student.Math;
                data.Russia = infoForm.Student.Russia;
                data.Inform = infoForm.Student.Inform;
                UpdateDb(opt, data);
                dataGridView1.DataSource = ReadDb(opt);
            }
        }

        private void ToolStripMenuItemChange_Click(object sender, EventArgs e)
        {
            var data = (Student)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].DataBoundItem;
            var infoForm = new FormStudentInfo(data);
            infoForm.Text = "Редактирование студента";
            if (infoForm.ShowDialog(this) == DialogResult.OK)
            {
                data.FullName = infoForm.Student.FullName;
                data.Gender = infoForm.Student.Gender;
                data.BirthDay = infoForm.Student.BirthDay;
                data.formStudy = infoForm.Student.formStudy;
                data.Math = infoForm.Student.Math;
                data.Russia = infoForm.Student.Russia;
                data.Inform = infoForm.Student.Inform;
                UpdateDb(opt, data);
                dataGridView1.DataSource = ReadDb(opt);
            }
        }

        private void ToolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            var data = (Student)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].DataBoundItem;
            if (MessageBox.Show($"Вы действительно хотите удалить '{data.FullName}'?",
                    "Удаление записи",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                RemoveDb(opt, data);
                dataGridView1.DataSource = ReadDb(opt);
            }
        }
        private void dataGridView1DataBingingComplete(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = $"Кол-во студентов с суммой баллов больше 150: {ReadDb(opt).Where(ball => ball.Math + ball.Russia + ball.Inform > 150).Count()}";

            toolStripStatusLabel2.Text = $"Количество абитуриентов: {ReadDb(opt).Count}";
        }
        private static void UpdateDb(DbContextOptions<Context> opt, Student student)
        {
            using (var db = new Context(opt))
            {
                var value = db.BasaKisel.FirstOrDefault(u => u.Id == student.Id);
                if (value != null)
                {
                    value.FullName = student.FullName;
                    value.Gender = student.Gender;
                    value.BirthDay = student.BirthDay;
                    value.formStudy = student.formStudy;
                    value.Math = student.Math;
                    value.Russia = student.Russia;
                    value.Inform = student.Inform;
                    db.SaveChanges();
                }
            }
        }
        private static void RemoveDb(DbContextOptions<Context> opt, Student student)
        {
            using (var db = new Context(opt))
            {
                var value = db.BasaKisel.FirstOrDefault(u => u.Id == student.Id);
                if (value != null)
                {
                    db.BasaKisel.Remove(value);
                    db.SaveChanges();
                }
            }

        }
        private static void CreateDb(DbContextOptions<Context> opt, Student student)
        {
            using (var db = new Context(opt))
            {
                Student std = new Student();
                student.Id = std.Id;
                db.BasaKisel.Add(student);
                db.SaveChanges();
            }
        }
        private static List<Student> ReadDb(DbContextOptions<Context> opt)
        {
            using (Context db = new Context(opt))
            {
                return db.BasaKisel
                    .OrderByDescending(x => x.Id)
                    .ToList();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
