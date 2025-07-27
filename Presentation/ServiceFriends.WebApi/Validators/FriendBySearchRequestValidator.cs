using FluentValidation;
using ServiceFriends.WebApi.Models.Requests;

namespace ServiceFriends.WebApi.Validators
{
    public class FriendBySearchRequestValidator : AbstractValidator<FriendBySearchRequest>
    {
        public FriendBySearchRequestValidator()
        {
            RuleFor(p => p.UserId)
                .NotEmpty()
                .WithMessage("UserId не заполнен.");
        }
    }
}
