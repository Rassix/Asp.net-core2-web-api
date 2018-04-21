using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public String Username { get; set; }

        [Required]
        [MinLength(7)]
        public String Password { get; set; }
    }
}
