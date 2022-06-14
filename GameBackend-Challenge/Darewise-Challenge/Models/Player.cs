using System.ComponentModel.DataAnnotations;

namespace GameBackend_Challenge.Entities
{
    public class Player
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public int Level { get; set; }

        public string AccountType { get; set; }

    }
}