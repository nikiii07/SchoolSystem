namespace SchoolSystem.Models.EntityModels
{
    using System.Collections;
    using System.Collections.Generic;
    using Contracts;

    public class Subject : IThumbnail
    {
        public Subject()
        {
            this.Teachers = new HashSet<Teacher>();
            this.Students = new HashSet<Student>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Difficulty { get; set; }

        public string ThumbnailUrl { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
