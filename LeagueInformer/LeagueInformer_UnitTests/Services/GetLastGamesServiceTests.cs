using LeagueInformer;
using LeagueInformer.Api.Interfaces;
using LeagueInformer.Models;
using LeagueInformer.Services;
using LeagueInformer.Utils.Interfaces;
using Moq;
using Xunit;

namespace LeagueInformer_UnitTests.Services
{
    public class GetLastGamesServiceTests
    {
        private readonly Mock<IApiClient> _apiClientMock;
        private readonly Mock<IErrorHandler> _errorHandlerMock;
        private readonly Mock<IHttpClient> _httpClientMock;
        private readonly GetLastGamesService _service;

        public GetLastGamesServiceTests()
        {
            _apiClientMock = new Mock<IApiClient>();
            _errorHandlerMock = new Mock<IErrorHandler>();
            _httpClientMock = new Mock<IHttpClient>();
            _service = new GetLastGamesService(
                _apiClientMock.Object,
                _errorHandlerMock.Object);
        }

        [Fact]
        public async void GetLastTenGames_ResponseIsEmpty_ReturnsUnsuccessful()
        {
            _apiClientMock.Setup(x => x.GetJsonFromUrl("abc")).ReturnsAsync(string.Empty);

            var result = await _service.GetLastTenGames("1", "eun1");

            Assert.False(result.IsSuccess);
        }
    }
}