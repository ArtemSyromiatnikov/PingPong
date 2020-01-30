using FluentValidation;
using PingPong.Sdk.Models.Players;

namespace PingPong.API.Validators
{
    public class CreatePlayerRequestValidator: AbstractValidator<CreatePlayerRequestDto>
    {
        public CreatePlayerRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Provide the First Name");
            
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Provide the Last Name");
        }
    }
}