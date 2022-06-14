using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Darewise_Challenge.DTOs
{
    public class PlayerCreationDTO
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
