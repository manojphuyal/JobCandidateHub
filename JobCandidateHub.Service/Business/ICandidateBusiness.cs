using JobCandidateHub.Database.Models;
using JobCandidateHub.Service.Models;

namespace JobCandidateHub.Service.Business;
public interface ICandidateBusiness
{
    Task<Response> CreateOrUpdate(Candidate candidate);
}
