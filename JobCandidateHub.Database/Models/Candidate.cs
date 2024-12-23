﻿using System.ComponentModel.DataAnnotations;

namespace JobCandidateHub.Database.Models;
public class Candidate: BaseEntity
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string PreferredCallTime { get; set; } = string.Empty;
    public string LinkedInProfile { get; set; } = string.Empty;
    public string GitHubProfile { get; set; } = string.Empty;
    [Required]
    public string Comments { get; set; } = string.Empty;
}
