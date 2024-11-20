using JobCandidateHub.Database.Context;
using JobCandidateHub.Database.Models;
using JobCandidateHub.Service.Repository;
using Microsoft.EntityFrameworkCore;

namespace JobCandidateHub.Tests.UnitTest;
public class CandidateRepositoryTests
{
    private readonly AppDBContext _context;
    private CandidateRepository _candidateRepository;

    public CandidateRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AppDBContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AppDBContext(options);
        _candidateRepository = new CandidateRepository(_context);
    }

    [Fact]
    public async Task CreateOrUpdate_Should_CreateNewCandidate_When_CandidatePhuyalsNotExist()
    {
        // Arrange
        var candidate = new Candidate
        {
            Email = "phuyalmanoj259@gmail.com",
            FirstName = "Manoj",
            LastName = "Phuyal",
            Comments = "Initial comment"
        };

        // Act
        var response = await _candidateRepository.CreateOrUpdate(candidate);

        // Assert
        Assert.True(response.IsSuccess);
        Assert.Equal("Candidate created successfully.", response.Message);
        Assert.NotNull(await _context.Candidate.FirstOrDefaultAsync(c => c.Email == candidate.Email));
    }

    [Fact]
    public async Task CreateOrUpdate_Should_UpdateCandidate_When_CandidateExists()
    {
        // Arrange
        var candidate = new Candidate
        {
            Email = "phuyalmanoj259@gmail.com",
            FirstName = "Manoj",
            LastName = "Phuyal",
            Comments = "Initial comment"
        };

        await _context.Candidate.AddAsync(candidate);
        await _context.SaveChangesAsync();

        var updatedCandidate = new Candidate
        {
            Email = "phuyalmanoj259@gmail.com",
            FirstName = "UpdatedManoj",
            LastName = "UpdatedPhuyal",
            Comments = "Updated comment"
        };

        // Act
        var response = await _candidateRepository.CreateOrUpdate(updatedCandidate);

        // Assert
        Assert.True(response.IsSuccess);
        Assert.Equal("Candidate updated successfully.", response.Message);

        var savedCandidate = await _context.Candidate.FirstOrDefaultAsync(c => c.Email == candidate.Email);
        Assert.NotNull(savedCandidate);
        Assert.Equal("UpdatedManoj", savedCandidate.FirstName);
        Assert.Equal("UpdatedPhuyal", savedCandidate.LastName);
        Assert.Equal("Updated comment", savedCandidate.Comments);
    }

    [Fact]
    public async Task CreateOrUpdate_Should_ReturnError_When_SaveChangesFails()
    {
        // Arrange
        var candidate = new Candidate
        {
            Email = "phuyalmanoj259@gmail.com",
            FirstName = "Manoj",
            LastName = "Phuyal",
            Comments = "Comment"
        };

        await _context.DisposeAsync();

        var candidateRepository = new CandidateRepository(_context);

        // Act
        var response = await candidateRepository.CreateOrUpdate(candidate);

        // Assert
        Assert.False(response.IsSuccess);
        Assert.StartsWith("An unexpected error occurred:", response.Message);
        Assert.Null(response.Data);
    }

    [Fact]
    public async Task CreateOrUpdate_Should_NotCreateDuplicateCandidates()
    {
        // Arrange
        var candidate = new Candidate
        {
            Email = "phuyalmanoj259@gmail.com",
            FirstName = "Manoj",
            LastName = "Phuyal",
            Comments = "Comment"
        };

        await _context.Candidate.AddAsync(candidate);
        await _context.SaveChangesAsync();

        var duplicateCandidate = new Candidate
        {
            Email = "phuyalmanoj259@gmail.com",
            FirstName = "DuplicateManoj",
            LastName = "DuplicatePhuyal",
            Comments = "Duplicate comment"
        };

        // Act
        var response = await _candidateRepository.CreateOrUpdate(duplicateCandidate);

        // Assert
        Assert.True(response.IsSuccess);
        Assert.Equal("Candidate updated successfully.", response.Message);

        var savedCandidate = await _context.Candidate.FirstOrDefaultAsync(c => c.Email == candidate.Email);
        Assert.NotNull(savedCandidate);
        Assert.Equal("DuplicateManoj", savedCandidate.FirstName);
    }

    [Fact]
    public async Task CreateOrUpdate_Should_Handle_NoChangesForExistingCandidate()
    {
        // Arrange
        var candidate = new Candidate
        {
            Email = "phuyalmanoj259@gmail.com",
            FirstName = "Manoj",
            LastName = "Phuyal",
            Comments = "Comment"
        };

        await _context.Candidate.AddAsync(candidate);
        await _context.SaveChangesAsync();

        // Act
        var response = await _candidateRepository.CreateOrUpdate(candidate);

        // Assert
        Assert.True(response.IsSuccess);
        Assert.Equal("Candidate updated successfully.", response.Message);
    }

    [Fact]
    public async Task CreateOrUpdate_Should_ReturnValidResponseStructure()
    {
        // Arrange
        var candidate = new Candidate
        {
            Email = "phuyalmanoj259@gmail.com",
            FirstName = "Manoj",
            LastName = "Phuyal",
            Comments = "Comment"
        };

        // Act
        var response = await _candidateRepository.CreateOrUpdate(candidate);

        // Assert
        Assert.NotNull(response);
        Assert.True(response.IsSuccess);
        Assert.Equal("Candidate created successfully.", response.Message);
        Assert.NotNull(response.Data);
        Assert.IsType<Candidate>(response.Data);
    }
}