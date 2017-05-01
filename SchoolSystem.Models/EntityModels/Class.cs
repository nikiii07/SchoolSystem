namespace SchoolSystem.Models.EntityModels
{
    using System.Collections;
    using System.Collections.Generic;

    public class Class
    {
        public Class()
        {
            this.Students = new HashSet<Student>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual Teacher HeadTeacher { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
