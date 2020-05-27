using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace QRede.Model.Validator
{
    public class PasswordValidator : AbstractValidator<string>
    {
        public PasswordValidator()
        {
            RuleFor(p => p).Matches(@"^[a-zA-Z0-9]*$").WithMessage(Language.Language.PasswordIsInvalid);
        }
    }
}
