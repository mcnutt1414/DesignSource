using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DesignSource.Models
{
    public class ContactModels
    {
        [Required(ErrorMessage = "First name is Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is Required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }

        public string Phone { get; set; }

        public string Comment { get; set; }
    }
}