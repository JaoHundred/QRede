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
    }
}
