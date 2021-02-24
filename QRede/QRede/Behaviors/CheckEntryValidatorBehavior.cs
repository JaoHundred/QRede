using FluentValidation;
using FluentValidation.Results;
using QRede.Interfaces;
using QRede.Model.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace QRede.Behaviors
{
    public class CheckEntryValidatorBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry bindable)
        {
            _validator = new PasswordValidator();
            bindable.TextChanged += Bindable_TextChanged;
            base.OnAttachedTo(bindable);
        }

        private IValidator _validator;
        private async void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is Entry entry)
            {
                ValidationResult validation = await _validator.ValidateAsync(new ValidationContext<string>(e.NewTextValue));

                if (!validation.IsValid)
                {
                    var cList = new List<char>();
                    foreach (var ch in e.NewTextValue.ToCharArray())
                    {
                        foreach (var item in GetInvalidCharacters())
                        {
                            if (ch == item)
                                cList.Add(ch);
                        }
                    }

                    var builder = new StringBuilder();
                    foreach (var item in cList)
                    {
                        builder.Append(item);
                        builder.Append(" ");
                    }

                    entry.Text = e.OldTextValue == null ? string.Empty : e.OldTextValue;
                    DependencyService.Get<IToastService>()
                        .ToastLongMessage($"{validation.Errors[0].ErrorMessage} {builder}");
                }
            }
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= Bindable_TextChanged;
            base.OnDetachingFrom(bindable);
        }


        private char[] GetInvalidCharacters()
        {
            return new[]
            {
                '\\' ,
            };
        }
    }
}
