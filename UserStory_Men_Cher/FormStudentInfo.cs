using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataGridView_Kisel.Models;

namespace DataGridView_Kisel
{
    public partial class FormStudentInfo : Form
    {
        public Student student;
        public FormStudentInfo()
        {
            InitializeComponent();
            FillGender();
            FillStudy();
            student = new Student
            {
                Gender = Gender.Female,
                formStudy = FormStudy.Och
            };
            comboBoxGender.SelectedItem = student.Gender;
            comboBox1.SelectedItem = student.formStudy;
        }

        public FormStudentInfo(Student source)
            : this()
        {
            textBoxFio.Text = source.FullName;
            comboBoxGender.SelectedItem = source.Gender;
            dateTimePickerDayB.Value = source.BirthDay;
            comboBox1.SelectedItem = source.formStudy;
            numericUpDownMath.Value = source.Math;
            numericUpDownRussia.Value = source.Russia;
            numericUpDownInform.Value = source.Inform;
        }
        public Student Student => student;
        private void FillStudy()
        {
            foreach (var item in Enum.GetValues(typeof(FormStudy)))
            {
                comboBox1.Items.Add(item);
            }
        }
        private void FillGender()
        {
            foreach (var item in Enum.GetValues(typeof(Models.Gender)))
            {
                comboBoxGender.Items.Add(item);
            }
        }

        private void comboBoxDayB_DrawItem(object sender, DrawItemEventArgs e)
        {
            var parent = sender as ComboBox;
            if (parent != null)
            {
                e.DrawBackground();
                Brush brush = new SolidBrush(parent.ForeColor);
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    brush = SystemBrushes.HighlightText;
                }

                if (e.Index >= 0)
                {
                    if (parent.Items[e.Index] is Gender gender)
                    {
                        var text = gender == Gender.Male //условие if сахар
                            ? "Мужской"
                            : "Женский";

                        e.Graphics.DrawString(
                            text,
                            parent.Font,
                            brush,
                            e.Bounds);
                    }
                    else
                    {
                        e.Graphics.DrawString(
                            parent.Items[e.Index].ToString(),
                            parent.Font,
                            brush,
                            e.Bounds);
                    }
                }
            }
        }
        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            var parent = sender as ComboBox;
            if (parent != null)
            {
                e.DrawBackground();
                Brush brush = new SolidBrush(parent.ForeColor);
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    brush = SystemBrushes.HighlightText;
                }

                if (e.Index >= 0)
                {
                    if (parent.Items[e.Index] is FormStudy formstudy)
                    {
                        string text = "";
                        switch (formstudy)
                        {
                            case (FormStudy.Och):
                                text = "Очная";
                                break;
                            case (FormStudy.Zaoch):
                                text = "Заочная";
                                break;
                            case (FormStudy.Och_Zaoch):
                                text = "Очно-заочная";
                                break;
                        }
                        e.Graphics.DrawString(
                            text,
                            parent.Font,
                            brush,
                            e.Bounds);
                    }
                    else
                    {
                        e.Graphics.DrawString(
                            parent.Items[e.Index].ToString(),
                            parent.Font,
                            brush,
                            e.Bounds);
                    }
                }
            }
        }

        private void textBoxFio_TextChanged(object sender, EventArgs e)
        {
            student.FullName = textBoxFio.Text.Trim();
            Validate();
        }

        private void comboBoxGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            student.Gender = (Gender)comboBoxGender.SelectedItem;
        }

        private void numericUpDownAvg_ValueChanged(object sender, EventArgs e)
        {
            student.Math = (int)numericUpDownMath.Value;
        }

        private void dateTimePickerDayB_ValueChanged(object sender, EventArgs e)
        {
            student.BirthDay = dateTimePickerDayB.Value;
        }

        public void Validate()
        {
            buttonSave.Enabled = !string.IsNullOrEmpty(student.FullName);
        }

        private void numericUpDownRussia_ValueChanged(object sender, EventArgs e)
        {
            student.Russia = (int)numericUpDownRussia.Value;
        }

        private void numericUpDownInform_ValueChanged(object sender, EventArgs e)
        {
            student.Inform = (int)numericUpDownInform.Value;
            student.Sum = student.Inform + student.Russia + student.Math;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            student.formStudy = (FormStudy)comboBox1.SelectedItem;
        }

        private void FormStudentInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
