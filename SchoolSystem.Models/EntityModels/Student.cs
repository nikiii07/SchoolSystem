namespace SchoolSystem.Models.EntityModels
{
    using System.Collections.Generic;

    public class Student
    {
        public Student()
        {
            this.Subjects = new HashSet<Subject>();
        }

        public int Id { get; set; }

        public virtual ApplicationUser Profile { get; set; }

        public virtual Class Class { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
