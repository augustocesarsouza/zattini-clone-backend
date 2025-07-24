namespace Zattini.Application.DTOs.Validations.Interfaces
{
    public interface IUserAddressCreateDTOValidator
    {
        public FluentValidation.Results.ValidationResult ValidateDTO(UserAddressDTO userAddressDTO);
    }
}
