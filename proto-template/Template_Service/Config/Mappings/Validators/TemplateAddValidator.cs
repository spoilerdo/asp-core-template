using Template_Service.Proto;
using FluentValidation;

namespace Template_Service.Config.Mappings.Validators {
    public class TemplateAddValidator : AbstractValidator<TemplateAddRequest> {
        public TemplateAddValidator() {
            RuleFor(request => request.Name).NotEmpty().WithMessage("Name is mandatory.");
        }
    }
}