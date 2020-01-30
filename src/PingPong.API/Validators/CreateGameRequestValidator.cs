using System;
using FluentValidation;
using PingPong.Sdk.Models.Games;

namespace PingPong.API.Validators
{
    public class CreateGameRequestValidator: AbstractValidator<CreateGameRequestDto>
    {
        public CreateGameRequestValidator()
        {
            RuleFor(x => x.Player1Id)
                .NotEmpty().WithMessage("Select Player 1");

            RuleFor(x => x.Player2Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Select Player 2")
                .Must(BeDifferentPlayers).WithMessage("Players must be different");
            
            RuleFor(x => x.Player1Score)
                .GreaterThanOrEqualTo(0).WithMessage("Score can't be lower than zero")
                .LessThanOrEqualTo(100).WithMessage("I highly doubt you've reached score that high");

            RuleFor(x => x.Player2Score)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(0).WithMessage("Score can't be lower than zero")
                .LessThanOrEqualTo(100).WithMessage("I highly doubt you've reached score that high")
                .Must(BeAValidScore).WithMessage("Game cannot end with such score");
        }

        private bool BeDifferentPlayers(CreateGameRequestDto game, int player2Id)
        {
            return game.Player1Id != player2Id;
        }

        private bool BeAValidScore(CreateGameRequestDto game, int score2)
        {
            int score1 = game.Player1Score;

            return IsValidScore(score1, score2);
        }

        // Valid scores:
        //   11:0
        //   11:5
        //   11:9
        //   12:10
        //   13:11
        //   99:97
        // Invalid scores:
        //   10:3
        //   11:10
        //   15:12
        //   15:14
        // Validation rules
        //   Rule 1: if higher score is 11, the lower should be in range of [0..9]
        //   Rule 2: if higher score is >11, the lower should be exactly 2 points lower
        //   else -> invalid
        public static bool IsValidScore(int score1, int score2)
        {
            int higherScore = Math.Max(score1, score2);
            int lowerScore = Math.Min(score1, score2);

            bool isValid = (higherScore, lowerScore) switch
            {
                (11, int lower)                             => lower >=0 && lower <= 9,
                var (higher, lower) when higher > 11 => lower == higher - 2,
                _                                           => false
            };
            
            return isValid;
        }
    }
}