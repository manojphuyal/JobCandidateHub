using System.ComponentModel.DataAnnotations;

namespace JobCandidateHub.Database.Models;
public class Candidate
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string PreferredCallTime { get; set; }
    public string LinkedInProfile { get; set; }
    public string GitHubProfile { get; set; }
    [Required]
    public string Comments { get; set; }
}
