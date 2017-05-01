namespace SchoolSystem.Models.Contracts
{
    using System;

    public interface IPerson : IVisible, IThumbnail
    {
        string FirstName { get; set; }

        string MiddleName { get; set; }
        
        string LastName { get; set; }
        
        string Pin { get; set; }

        string BirthPlace { get; set; }

        string Address { get; set; }

        int? Age { get; set; }

        DateTime? BirthDate { get; set; }
    }
}
