using AutoMapper;
using JobCandidateHub.API.Controllers;
using JobCandidateHub.Database.Models;
using JobCandidateHub.Service.Business;
using JobCandidateHub.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace JobCandidateHub.Tests.UnitTest;
public class CandidateControllerTests
{
    private Mock<ICandidateBusiness> _mockCandidateBusiness;
    private Mock<IMapper> _mockMapper;
    private CandidateController _controller;

    public CandidateControllerTests()
    {
        _mockCandidateBusiness = new Mock<ICandidateBusiness>();
        _mockMapper = new Mock<IMapper>();
        _controller = new CandidateController(_mockCandidateBusiness.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Save_Should_ReturnBadRequest_When_ModelStateIsInvalid()
    {
        // Arrange
        var candidateDto = new CandidateDto { Email = "invalid-email" };
        _controller.ModelState.AddModelError("Email", "Invalid email format");

        // Act
        var result = await _controller.Save(candidateDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
    }

    [Fact]
    public async Task Save_Should_ReturnOk_When_CandidateIsCreatedSuccessfully()
    {
        // Arrange
        var candidateDto = new CandidateDto { Email = "phuyalmanoj259@gmail.com", FirstName = "Manoj", LastName = "Phuyal" };
        var candidate = new Candidate { Email = "phuyalmanoj259@gmail.com", FirstName = "Manoj", LastName = "Phuyal" };
        _mockMapper.Setup(m => m.Map<Candidate>(candidateDto)).Returns(candidate);
        _mockCandidateBusiness.Setup(b => b.CreateOrUpdate(It.IsAny<Candidate>()))
                              .ReturnsAsync(new Response { IsSuccess = true, Message = "Candidate created successfully." });

        // Act
        var result = await _controller.Save(candidateDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task Save_Should_ReturnBadRequest_When_CandidateCreationFails()
    {
        // Arrange
        var candidateDto = new CandidateDto { Email = "phuyalmanoj259@gmail.com", FirstName = "Manoj", LastName = "Phuyal" };
        var candidate = new Candidate { Email = "phuyalmanoj259@gmail.com", FirstName = "Manoj", LastName = "Phuyal" };
        _mockMapper.Setup(m => m.Map<Candidate>(candidateDto)).Returns(candidate);
        _mockCandidateBusiness.Setup(b => b.CreateOrUpdate(It.IsAny<Candidate>()))
                              .ReturnsAsync(new Response { IsSuccess = false, Message = "Error occurred." });

        // Act
        var result = await _controller.Save(candidateDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
    }
    [Fact]
    public async Task Save_Should_ReturnCorrectResponseStructure_When_CreationSucceeds()
    {
        // Arrange
        var candidateDto = new CandidateDto { Email = "phuyalmanoj259@gmail.com", FirstName = "Manoj", LastName = "Phuyal" };
        var candidate = new Candidate { Email = "phuyalmanoj259@gmail.com", FirstName = "Manoj", LastName = "Phuyal" };
        _mockMapper.Setup(m => m.Map<Candidate>(candidateDto)).Returns(candidate);
        _mockCandidateBusiness.Setup(b => b.CreateOrUpdate(It.IsAny<Candidate>()))
                               .ReturnsAsync(new Response { IsSuccess = true, Message = "Candidate created successfully.", Data = candidate });

        // Act
        var result = await _controller.Save(candidateDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);

        var responseData = Assert.IsType<Candidate>(okResult.Value);
        Assert.Equal(candidateDto.Email, responseData.Email);
    }
}
