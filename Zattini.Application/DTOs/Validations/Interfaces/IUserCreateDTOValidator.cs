namespace Zattini.Application.DTOs.Validations.Interfaces
{
    public interface IUserCreateDTOValidator
    {
        public FluentValidation.Results.ValidationResult ValidateDTO(UserCreateDTO userCreateDTO);
    }
}
