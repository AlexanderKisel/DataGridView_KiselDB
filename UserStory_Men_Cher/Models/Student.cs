using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGridView_Kisel.Models
{
    /// <summary>
    /// Сущность студента
    /// </summary>
    public class Student
    {
        public int KolvoPalcev { get; set; }
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// ФИО
        /// </summary>
        public FormStudy formStudy { get; set; }

        public string FullName { get; set; }
        /// <summary>
        /// Пол
        /// </summary>
        public Gender Gender { get; set; }
        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime BirthDay { get; set; }

        public int Math { get; set; }

        public int Russia { get; set; }

        public int Inform { get; set; }

        public int Sum { get; set; }

    }
}
