using JobCandidateHub.Database.Models;
using JobCandidateHub.Service.Business;
using Microsoft.AspNetCore.Mvc;

namespace JobCandidateHub.API.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class CandidateController : ControllerBase
{
    private readonly ICandidateBusiness _candidateBusiness;

    public CandidateController(ICandidateBusiness candidateBusiness)
    {
        _candidateBusiness = candidateBusiness;
    }

    [HttpPost]
    public async Task<IActionResult> Save(Candidate candidate)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var isSuccess = await _candidateBusiness.CreateOrUpdate(candidate);
        if (isSuccess)
        {
            return Ok(candidate);
        }
        return BadRequest();
    }
}

