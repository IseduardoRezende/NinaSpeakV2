using Microsoft.AspNetCore.Http;
using NinaSpeakV2.Data.Models;
using NinaSpeakV2.Domain.ViewModels.Institutions;

namespace NinaSpeakV2.Domain.Validators
{
    public class InstitutionValidator : BaseEnumValidator
    {
        public const int NameMaxLength = 80;
        public const int NameMinLength = 1;

        public const int ImageFileNameMaxLength = 30;
        public const int ImageFileNameMinLength = 1;

        public const string StandardName = "ChatBot de Graça";

        public static bool IsValidName(string name)
        {
            return IsValid(name) && IsBetween(name.Length, NameMinLength, NameMaxLength) && name is not StandardName;
        }

        public static bool IsValidImage(IFormFile? image)
        {            
            return image is null ||
                 (IsValid(image) && IsBetween(image.FileName.Length, ImageFileNameMinLength, ImageFileNameMaxLength));
        }

        public static bool IsEqual(Institution institution, UpdateInstitutionViewModel updateModel)
        {
            ArgumentNullException.ThrowIfNull(institution, nameof(institution));
            ArgumentNullException.ThrowIfNull(updateModel, nameof(updateModel));

            return institution.Name == updateModel.Name &&
                   institution.Image == updateModel.FileName &&
                   institution.Description == updateModel.Description;
        }
    }
}
