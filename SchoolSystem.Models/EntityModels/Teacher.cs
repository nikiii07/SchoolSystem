namespace SchoolSystem.Models.EntityModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Teacher
    {
        public Teacher()
        {
            this.Subjects = new HashSet<Subject>();
            this.TaughtClasses = new HashSet<Class>();
        }
        
        public int Id { get; set; }
        
        /// <summary>
        /// All subjects the teacher teaches
        /// </summary>
        public virtual ICollection<Subject> Subjects { get; set; }

        /// <summary>
        /// All classes the teacher teaches
        /// </summary>
        public virtual ICollection<Class> TaughtClasses { get; set; }

        /// <summary>
        /// Class which the teacher is head of
        /// </summary>
        public virtual Class Class { get; set; }

        public virtual ApplicationUser Profile { get; set; }
    }
}
