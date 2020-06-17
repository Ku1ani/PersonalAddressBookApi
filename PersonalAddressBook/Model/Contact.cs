using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalAddressBook.Model
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string FirstName { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Surname { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Tel { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Cell { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
