using FluentValidation;
using ServiceFriends.WebApi.Models.Requests;

namespace ServiceFriends.WebApi.Validators
{
    public class FriendRequestValidator : AbstractValidator<FriendRequest>
    {
        public FriendRequestValidator()
        {
            RuleFor(p => p.UserId)
                .NotEmpty()
                .WithMessage("UserId не заполнен.");

            RuleFor(p => p.FriendId)
                .NotEmpty()
                .WithMessage("FriendId не заполнен.");
        }
    }
}
