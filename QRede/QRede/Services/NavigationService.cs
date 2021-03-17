using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;
using Rg.Plugins.Popup.Pages;
using MvvmHelpers;

namespace QRede.Services
{
    public static class NavigationService
    {
        public static string CurrentPageRoute
        {
            get
            {
                return Shell.Current.CurrentItem.Route;
            }
        }

        public static IEnumerable<Page> Stack
        {
            get
            {
                return Shell.Current.Navigation.NavigationStack;
            }
        }


        public static IEnumerable<Page> StackModal
        {
            get
            {
                return Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopupStack;
            }
        }

        public static async Task NavigateAsync<T>(params object[] parameters)
        {
            string name = typeof(T).AssemblyQualifiedName + typeof(T).FullName;
            Type ViewType = Type.GetType(name.Replace("ViewModel", "View"));

            IEnumerable<Type> paramTypes = parameters.Select(parameter => parameter.GetType());
            ConstructorInfo ViewModelConstructor = typeof(T).GetConstructor(paramTypes.ToArray());
            object ViewModel = ViewModelConstructor.Invoke(parameters);

            ConstructorInfo ViewConstructor = ViewType.GetConstructor(Type.EmptyTypes);
            Page View = (Page)ViewConstructor.Invoke(null);

            View.BindingContext = ViewModel;
            if (View is PopupPage)
            {
                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync((PopupPage)View);
            }
            else
            {
                await Shell.Current.Navigation.PushAsync(View);
            }
        }

        public static Task PopModalAsync()
        {
            return Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
        }

        public static bool CanPopupNavigate<T>()
        {
            string name = typeof(T).AssemblyQualifiedName + typeof(T).FullName;
            Type ViewType = Type.GetType(name.Replace("ViewModel", "View"));

            PopupPage LastPopup = Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopupStack.LastOrDefault();
            if (LastPopup == null)
            {
                return true;
            }
            else if (LastPopup != null && ViewType != LastPopup.GetType())
            {
                return true;
            }
            return false;
        }

        public static async Task RouteNavigationAsync(string Target)
        {
            ShellNavigationState Navigate = new ShellNavigationState(Target);
            await Shell.Current.GoToAsync(Navigate);
        }
    }
}
