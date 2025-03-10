using System;
using System.ComponentModel.DataAnnotations;

namespace User.Management.API.Models
{
    public class Stay
    {
        public int? StayId { get; set; }
        [Required(ErrorMessage = "Stay name is required.")]
        public string? Name { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? ImageUrl { get; set; }
        public int? MaxGuests { get; set; }

        public string? ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
