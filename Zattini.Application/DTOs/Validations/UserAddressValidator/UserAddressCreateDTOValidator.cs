using FluentValidation;
using FluentValidation.Results;
using Zattini.Application.DTOs.Validations.Interfaces;
using Zattini.Domain.EnumHelper;
using Zattini.Domain.Enums;

namespace Zattini.Application.DTOs.Validations.UserAddressValidator
{
    public class UserAddressCreateDTOValidator : AbstractValidator<UserAddressDTO>, IUserAddressCreateDTOValidator
    {
        public UserAddressCreateDTOValidator()
        {
            RuleFor(x => x.Cep)
                .NotEmpty().WithMessage("Cep must not be empty.")
                .NotNull().WithMessage("Cep must not be null.")
                .Matches(@"^\d{5}-\d{3}$")
                .WithMessage("Cep format is invalid. Expected format: 99999-999.");

            RuleFor(x => x.TypeAddress)
                .NotEmpty().WithMessage("TypeAddress must not be empty.")
                .Must(type => EnumHelper.GetEnumDescriptions<AddressTypeAddressType>().Contains(type?.ToLower()))
                .WithMessage("TypeAddress must be one of the following: avenida, rua, praça, quadra, estrada, alameda, ladeira, travessa, rodovia, praia, outros.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address must not be empty.")
                .NotNull().WithMessage("Address must not be null.");

            RuleFor(x => x.Number)
                .NotNull().WithMessage("Number must not be null.");

            //RuleFor(x => x.Complement);

            RuleFor(x => x.Neighborhood)
                .NotEmpty().WithMessage("Neighborhood must not be empty.")
                .NotNull().WithMessage("Neighborhood must not be null.");

            RuleFor(x => x.State)
                .NotEmpty().WithMessage("State must not be empty.")
                .NotNull().WithMessage("State must not be null.")
                .MinimumLength(3).WithMessage("State must be at least 3 characters long.");

            RuleFor(x => x.City)
               .NotEmpty().WithMessage("City must not be empty.")
               .NotNull().WithMessage("City must not be null.");

            //RuleFor(x => x.ReferencePoint);
        }

        public ValidationResult ValidateDTO(UserAddressDTO userAddressDTO)
        {
            return Validate(userAddressDTO);
        }
    }
}
