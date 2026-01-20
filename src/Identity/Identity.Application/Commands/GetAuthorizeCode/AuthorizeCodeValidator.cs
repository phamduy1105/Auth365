using FluentValidation;

namespace Identity.Application.Commands.GetAuthorizeCode;

public class AuthorizeCodeValidator : AbstractValidator<GetAuthorizeCodeCommand>
{
    public AuthorizeCodeValidator() {
        RuleFor(x 
            => x.AuthorizeCodeRequestDto.ClientId).NotEmpty().WithMessage("invalid_request");
        
        RuleFor(x 
            => x.AuthorizeCodeRequestDto.RedirectUri).NotEmpty().WithMessage("invalid_redirect_uri");
    }
}