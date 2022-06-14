using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Darewise_Challenge.DTOs
{
    public class BugCreationDTO
    {

        [Required(ErrorMessage = "{0} is a required input field")]
        public int PlayerId { get; set; }

        [Required(ErrorMessage = "{0} is a required input field")]
        [StringLength(maximumLength: 50, ErrorMessage = "{0} text exceded. Maximum length 50")]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(maximumLength: 250, ErrorMessage = "{0} text exceded. Maximum length 250")]
        public string Description { get; set; }

        [Required]
        [Range(1, 5)]
        public int SeverityLvl { get; set; }

        public IFormFile Attachment { get; set; } 

    }
}
