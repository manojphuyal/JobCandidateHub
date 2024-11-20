using AutoMapper;
using JobCandidateHub.Database.Models;
using JobCandidateHub.Service.Models;

namespace JobCandidateHub.API.Extensions;

public class MapperProfiles:Profile
{
    public MapperProfiles()
    {
        CreateMap<CandidateDto, Candidate>();
    }
}
