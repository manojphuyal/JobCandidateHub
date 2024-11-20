using AutoMapper;
using JobCandidateHub.Database.Models;
using JobCandidateHub.Service.Business;
using JobCandidateHub.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobCandidateHub.API.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class CandidateController : ControllerBase
{
    private readonly ICandidateBusiness _candidateBusiness;
    private readonly IMapper _mapper;

    public CandidateController(ICandidateBusiness candidateBusiness, IMapper mapper)
    {
        _candidateBusiness = candidateBusiness;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Save([FromBody]CandidateDto candidateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var candidate = _mapper.Map<Candidate>(candidateDto);
        var response = await _candidateBusiness.CreateOrUpdate(candidate);
        if (response.IsSuccess)
        {
            return Ok(candidate);
        }
        return BadRequest(candidate);
    }
}

