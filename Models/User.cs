using System.ComponentModel.DataAnnotations;

namespace BidOne.Models;

public class User
{
    public int Id { get; set; }
    [Required]
    [Display(Name = "First Name")]
    [MaxLength(50)]
    [MinLength(2)]
    public string? FirstName { get; set; }
    [Display(Name = "Last Name")]
    [Required]
    [MaxLength(50)]
    [MinLength(2)]
    public string? LastName { get; set; }

}