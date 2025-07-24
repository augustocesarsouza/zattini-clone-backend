using FluentValidation;
using FluentValidation.Results;
using Zattini.Application.DTOs.Validations.Interfaces;
using Zattini.Domain.EnumHelper;
using Zattini.Domain.Enums;

namespace Zattini.Application.DTOs.Validations.UserValidator
{
    public class UserCreateDTOValidator : AbstractValidator<UserCreateDTO>, IUserCreateDTOValidator
    {
        public UserCreateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name must not be empty.")
                .NotNull().WithMessage("Name must not be null.")
                .MinimumLength(3).WithMessage("name must be at least 3 characters long.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("LastName must not be empty.")
                .NotNull().WithMessage("LastName must not be null.")
                .MinimumLength(3).WithMessage("LastName must be at least 3 characters long.");

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Gender must not be empty.")
                .Must(gender => EnumHelper.GetEnumDescriptions<UserGenderType>().Contains(gender?.ToLower()))
                .WithMessage("Gender must be either 'female' or 'male'");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email must not be empty.")
                .NotNull().WithMessage("Email must not be null.")
                .Matches(@"^[a-zA-Z0-9._%+-]+@gmail\.com$")
                .WithMessage("matches invalid Email");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("BirthDate must not be empty.")
                .NotNull().WithMessage("BirthDate must not be null.")
                .Matches(@"^\d{2}/\d{2}/\d{4}$")
                .WithMessage("matches invalid BirthDate");

            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage("Cpf must not be empty.")
                .NotNull().WithMessage("Cpf must not be null.")
                //.Matches(@"^\d{14}$")
                .Matches(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$")
                .WithMessage("matches invalid Cpf");

            RuleFor(x => x.CellPhone)
                .NotEmpty().WithMessage("CellPhone must not be empty.")
                .NotNull().WithMessage("CellPhone must not be null.")
                .Matches(@"^\(\d{2}\) \d{5}-\d{4}$")
                .WithMessage("matches invalid CellPhone");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password must not be empty.")
                .NotNull().WithMessage("Password must not be null.")
                .Length(6, 30).WithMessage("Password must be between 6 and 30 length");
        }

        public ValidationResult ValidateDTO(UserCreateDTO userCreateDTO)
        {
            return Validate(userCreateDTO);
        }
    }
}
