using LeagueInformer.Utils;
using Xunit;

namespace LeagueInformer_UnitTests.Utils
{
    public class DateHandlerTests
    {
        private readonly DateHandler _dateHandler;

        public DateHandlerTests()
        {
            _dateHandler = new DateHandler();
        }

        [Fact]
        public void ParseTimeToDate_UnparseableData_ReturnEmptyString()
        {
            //Arrange
            string data = "abcd";

            //Act
            var result = _dateHandler.ParseTimeToDate(data);
            
            //Assert
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void ParseTimeToDate_NegativeData_ReturnEmptyString()
        {
            //Arrange
            string data = "-300";

            //Act
            var result = _dateHandler.ParseTimeToDate(data);

            //Assert
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void ParseTimeToDate_TodayInMilliseconds_ReturnCorrectData()
        {
            //Arrange
            string data = "1545652090381";

            //Act
            var result = _dateHandler.ParseTimeToDate(data);

            //Assert
            Assert.Equal("24 grudnia 2018", result);
        }
    }
}