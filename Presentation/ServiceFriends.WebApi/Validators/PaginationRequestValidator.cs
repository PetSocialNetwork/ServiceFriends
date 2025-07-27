using FluentValidation;
using ServiceFriends.WebApi.Models.Requests;

namespace ServicePhoto.WebApi.Validator
{
    public class PaginationRequestValidator : AbstractValidator<PaginationRequest>
    {
        public PaginationRequestValidator()
        {
            RuleFor(s => s.Take)
               .NotEmpty()
               .WithMessage("Параметр take должен быть обязательно заполнен.");

            RuleFor(s => s.Offset)
               .NotEmpty()
               .WithMessage("Параметр offset должен быть обязательно заполнен.");
        }
    }
}
