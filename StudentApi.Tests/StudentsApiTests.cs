using System.Net;
using System.Net.Http.Json;
using StudentApi.Dtos;

namespace StudentApi.Tests;

public sealed class StudentsApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public StudentsApiTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Create_returns_201_location_and_persisted_student()
    {
        var request = new StudentDto { Name = "Jane Doe", Age = 22, Email = "jane@example.com" };

        var response = await _client.PostAsJsonAsync("/api/students", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(response.Headers.Location);
        var created = await response.Content.ReadFromJsonAsync<StudentDto>();
        Assert.NotNull(created);
        Assert.True(created.Id > 0);

        var getResponse = await _client.GetAsync(response.Headers.Location);
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
    }

    [Fact]
    public async Task Create_rejects_invalid_student()
    {
        var request = new StudentDto { Name = "J", Age = 17, Email = "not-an-email" };

        var response = await _client.PostAsJsonAsync("/api/students", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Missing_student_returns_404()
    {
        var response = await _client.GetAsync("/api/students/999999");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
