using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace GameBackend_Challenge.Entities
{
    public class Bug : Ticket
    {

        [Required]
        public DateTime ResolutionTime { get; set; }

        [Required]
        [Range(1, 5)]
        public int SeverityLvl { get; set; }

        [Required]
        public int Status { get; set; }

        public string Attachment { get; set; }

    }
}