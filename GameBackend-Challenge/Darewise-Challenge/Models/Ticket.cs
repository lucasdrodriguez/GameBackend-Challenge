using System;
using System.ComponentModel.DataAnnotations;

namespace GameBackend_Challenge.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
     
        [Required(ErrorMessage ="Player is a required input field")]
        public Player Player { get; set; }

        [Required]
        public DateTime CreationTime { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "{0} is a required input field")]
        [StringLength(maximumLength:50,ErrorMessage ="{0} text exceded. Maximum length 50")]
        public string Title { get; set; } 
    }
}
