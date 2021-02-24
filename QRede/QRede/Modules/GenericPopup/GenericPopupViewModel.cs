using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FFImageLoading;
using MvvmHelpers;
using QRede.Interfaces;
using QRede.Model;
using QRede.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QRede.Modules
{
    class GenericPopupViewModel : BaseViewModel
    {
        public GenericPopupViewModel(string Title, string Message, string confirmationText, string refuseText, Action confirmAction, Action refuseAction)
        {
            Startup(Title, Message, confirmationText, refuseText, confirmAction);
            RefuseAction = refuseAction;
        }

        public GenericPopupViewModel(string Title, string Message, string confirmationText, string refuseText, Action confirmAction)
        {
            Startup(Title, Message, confirmationText, refuseText, confirmAction);
            RefuseAction = null;
        }

        private void Startup(string Title, string Message, string confirmationText, string refuseText, Action confirmAction)
        {
            PopupTitle = Title;
            PopupMessage = Message;
            ConfirmationAction = confirmAction;
            ConfirmationText = confirmationText;
            RefuseText = refuseText;
            ConfirmationCommand = new MvvmHelpers.Commands.AsyncCommand(OnConfirm);
            RefuseCommand = new MvvmHelpers.Commands.AsyncCommand(OnRefuse);
        }

        public Action RefuseAction { get; set; }

        public string ConfirmationText { get; set; }

        public string RefuseText { get; set; }

        public string  PopupTitle { get; set; }

        public string PopupMessage { get; set; }

        public Action ConfirmationAction { get; set; }

        public ICommand ConfirmationCommand { get; set; }

        public async Task OnConfirm()
        {
            ConfirmationAction.Invoke();

            await NavigationService.PopModalAsync();
        }

        public ICommand RefuseCommand { get; set; }

        public async Task OnRefuse()
        {
            await NavigationService.PopModalAsync();
        }

    }
}
