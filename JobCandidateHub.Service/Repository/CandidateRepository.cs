using JobCandidateHub.Database.Context;
using JobCandidateHub.Database.Models;
using JobCandidateHub.Service.Business;
using Microsoft.EntityFrameworkCore;

namespace JobCandidateHub.Service.Repository;
public class CandidateRepository : ICandidateBusiness
{
    private readonly AppDBContext _context;
    public CandidateRepository(AppDBContext context)
    {
        _context = context;
    }
    public async Task<bool> CreateOrUpdate(Candidate candidate)
    {
        var isSuccess = true;
        try
        {
            var existingCandidate = await _context.Candidate.FirstOrDefaultAsync(c => c.Email == candidate.Email);
            if (existingCandidate != null)
            {
                existingCandidate.FirstName = candidate.FirstName;
                existingCandidate.LastName = candidate.LastName;
                existingCandidate.PhoneNumber = candidate.PhoneNumber;
                existingCandidate.PreferredCallTime = candidate.PreferredCallTime;
                existingCandidate.LinkedInProfile = candidate.LinkedInProfile;
                existingCandidate.GitHubProfile = candidate.GitHubProfile;
                existingCandidate.Comments = candidate.Comments;
                _context.Candidate.Update(existingCandidate);
            }
            else
            {
                await _context.Candidate.AddAsync(candidate);
            }
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            isSuccess = false;
        }
        return isSuccess;
    }
}
