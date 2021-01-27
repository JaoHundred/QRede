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
        public GenericPopupViewModel(string Title, string Message,  Action action)
        {
            PopupTitle = Title;
            PopupMessage = Message;
            ConfirmationAction = action;
            ConfirmationCommand = new MvvmHelpers.Commands.AsyncCommand(OnConfirm);
            RefuseCommand = new MvvmHelpers.Commands.AsyncCommand(OnRefuse);
        }

        private string popupTitle;

        public string PopupTitle
        {
            get { return popupTitle; }
            set { SetProperty(ref popupTitle, value); }
        }

        private string popupMessage;

        public string PopupMessage
        {
            get { return popupMessage; }
            set { SetProperty(ref popupMessage, value); }
        }

        private Action confirmationAction;
        public Action ConfirmationAction
        {
            get { return confirmationAction; }
            set { SetProperty(ref confirmationAction, value); }
        }

        public ICommand ConfirmationCommand { get; set; }

        public async Task OnConfirm()
        {
            ConfirmationAction.Invoke();

            DependencyService.Get<IToastService>().ToastLongMessage(Language.Language.DeleteComplete);

            await NavigationService.PopModalAsync();
        }

        public ICommand RefuseCommand { get; set; }

        public async Task OnRefuse()
        {
            await NavigationService.PopModalAsync();
        }

    }
}
