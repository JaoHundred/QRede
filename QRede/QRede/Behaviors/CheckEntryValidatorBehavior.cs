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
                ValidationResult validation = await _validator.ValidateAsync(e.NewTextValue);

                if (!validation.IsValid)
                {
                    entry.Text = e.OldTextValue == null ? string.Empty : e.OldTextValue;
                    DependencyService.Get<IToastService>().ToastLongMessage($"{validation.Errors[0].ErrorMessage} {e.NewTextValue.Last()}");
                }
            }
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= Bindable_TextChanged;
            base.OnDetachingFrom(bindable);
        }
    }
}
