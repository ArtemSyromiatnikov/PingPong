using PingPong.Blazor.Validators;
using Xunit;

namespace PingPong.UnitTests
{
    public class ScoreValidationTests
    {
        [Theory]
        [InlineData(0, 0, false)]
        [InlineData(1, 0, false)]
        [InlineData(5, 0, false)]
        [InlineData(10, 0, false)]
        [InlineData(11, 0, true)]
        [InlineData(11, 5, true)]
        [InlineData(11, 9, true)]
        [InlineData(11, 10, false)]
        [InlineData(11, 11, false)]
        [InlineData(12, 10, true)]
        [InlineData(13, 11, true)]
        [InlineData(99, 97, true)]
        [InlineData(10, 3, false)]
        [InlineData(15, 12, false)]
        [InlineData(15, 13, true)]
        [InlineData(15, 14, false)]
        [InlineData(15, 15, false)]
        [InlineData(int.MaxValue, int.MinValue, false)]
        [InlineData(int.MaxValue, int.MaxValue-2, true)]
        public void TestScores(int score1, int score2, bool expected)
        {
            bool isValid = GameValidator.IsValidScore(score1, score2);
            Assert.Equal(expected, isValid);

            // if order of parameters is reversed, the game result should still be valid
            bool isValidReversedOrder = GameValidator.IsValidScore(score2, score1);
            Assert.Equal(expected, isValidReversedOrder);
        }
    }
}