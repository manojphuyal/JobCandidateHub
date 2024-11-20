using System.ComponentModel.DataAnnotations;

namespace JobCandidateHub.Database.Models;
public class BaseEntity
{
    [Key]
    public Guid Id { get; set; }
}
