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
            RuleFor(p => p).Matches(@"^[^ \\]*$").WithMessage(Language.Language.PasswordIsInvalid);
        }
    }
}
