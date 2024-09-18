using Microsoft.AspNetCore.Http;
using Tennis.Services.Utils;
using Moq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Tennis.Controllers;
using Tennis.Services;

public class ProblemDetailsHelperTests
{
    private readonly TournamentController _controller = new TournamentController(new Mock<ITournamentService>().Object);

    [Fact]
    public void CreateProblemDetails_ShouldReturnProblemDetails_WithCorrectProperties()
    {
        var mockHttpContext = new Mock<HttpContext>();
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = mockHttpContext.Object
        };

        var httpRequestMock = new Mock<HttpRequest>();
        var httpResponseMock = new Mock<HttpResponse>();

        mockHttpContext.SetupGet(c => c.Request).Returns(httpRequestMock.Object);
        mockHttpContext.SetupGet(c => c.Response).Returns(httpResponseMock.Object);
        httpRequestMock.SetupGet(r => r.Path).Returns("/test-path");
        httpResponseMock.SetupGet(r => r.StatusCode).Returns((int)HttpStatusCode.BadRequest);

        string detailMessage = "Test detail message";
        int? httpStatusCode = (int)HttpStatusCode.BadRequest;

        var result = ProblemDetailsHelper.CreateProblemDetails(mockHttpContext.Object, detailMessage, httpStatusCode);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("https://tools.ietf.org/html/rfc9110#section-15.5.1", result.Type);
        Assert.Equal("One or more validation errors occurred.", result.Title);
        Assert.Equal(httpStatusCode, result.Status);
        Assert.Equal(detailMessage, result.Detail);
        Assert.Equal("/test-path", result.Instance);
        Assert.NotNull(result.Extensions["traceId"]);
    }
}
