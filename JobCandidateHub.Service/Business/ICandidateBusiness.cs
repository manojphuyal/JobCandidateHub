using JobCandidateHub.Database.Models;

namespace JobCandidateHub.Service.Business;
public interface ICandidateBusiness
{
    Task<bool> CreateOrUpdate(Candidate candidate);
}
