using QRede.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using QRede.Interfaces;
using MvvmHelpers;
using magno = MvvmHelpers.Commands;
using System.Threading.Tasks;
using System.Windows.Input;
using QRede.Services;
using Xamarin.Forms;
using System.Linq;
using System.Resources;
using Xamarin.Essentials;

namespace QRede.Modules
{
    public class QRCollectionViewModel : BaseViewModel
    {
        public QRCollectionViewModel()
        {
            WifiSummaryCollection = new ObservableRangeCollection<WifiSummary>();

            DeleteCommand = new magno.AsyncCommand<WifiSummary>(OnDelete);
            ConnectCommand = new magno.AsyncCommand<WifiSummary>(OnConnect);
            SearchCommand = new magno.AsyncCommand(OnSearch);
            SortByWordsCommand = new magno.AsyncCommand(OnSortByWords);
            ZoomImageCommand = new magno.AsyncCommand<WifiSummary>(OnZoomImage);
            GetImageFromGalleryCommand = new MvvmHelpers.Commands.AsyncCommand(OnGetImageFromGallery);

            OrderText = Language.Language.SortByAscending;
        }

        #region Properties
        public string QRSearch { get; set; }

        private string _orderText;
        public string OrderText
        {
            get { return _orderText; }
            set { SetProperty(ref _orderText, value); }
        }

        private bool _isDarkTheme;
        public bool IsDarkTheme
        {
            get { return _isDarkTheme; }
            set { SetProperty(ref _isDarkTheme, value); }
        }

        public ObservableRangeCollection<WifiSummary> WifiSummaryCollection { get; set; }

        private List<WifiSummary> OriginalWifiSummaryCollection;
        #endregion

        #region Commands
        public ICommand DeleteCommand { get; set; }

        private async Task OnDelete(WifiSummary wifiSummary)
        {
            if (NavigationService.CanPopupNavigate<GenericPopupViewModel>())
            {
                await NavigationService.NavigateAsync<GenericPopupViewModel>(Language.Language.Warning, 
                    Language.Language.DeleteConfirmation, Language.Language.Confirmation, 
                    Language.Language.Refuse, 
                new Action(() =>
                {
                    WifiSummaryCollection.Remove(wifiSummary);
                    OriginalWifiSummaryCollection.Remove(wifiSummary);
                    App.liteDatabase.GetCollection<WifiSummary>().Delete(wifiSummary.Id);
                    DependencyService.Get<IToastService>().ToastLongMessage(Language.Language.DeleteComplete);
                })); 
            }
        }

        public ICommand ConnectCommand { get; set; }
        private async Task OnConnect(WifiSummary wifiSummary)
        {
            await DependencyService.Get<IConnectivityService>().Connect(EncryptionService.DecryptPassword(wifiSummary.EncryptedWifiString, wifiSummary.Key));
        }

        public ICommand SearchCommand { get; set; }

        private async Task OnSearch()
        {
            var task = Task.Run(() =>
            {
                return OriginalWifiSummaryCollection.Where(wifiSummary => wifiSummary.ParseWifiString(WifiParam.S).ToLower().Contains(QRSearch.ToLower()));
            });

            WifiSummaryCollection.ReplaceRange(await task);
        }


        public ICommand SortByWordsCommand { get; private set; }
        private async Task OnSortByWords()
        {
            var task = Task.Run(() =>
            {
                if (OrderText == Language.Language.SortByAscending)
                {
                    OrderText = Language.Language.SortByDescending;
                    return OriginalWifiSummaryCollection.OrderBy(wifiSummary => wifiSummary.ParseWifiString(WifiParam.S));
                }
                else
                {
                    OrderText = Language.Language.SortByAscending;
                    return OriginalWifiSummaryCollection.OrderByDescending(wifiSummary => wifiSummary.ParseWifiString(WifiParam.S));
                }
            });

            WifiSummaryCollection.ReplaceRange(await task);
        }

        public ICommand ZoomImageCommand { get; private set; }
        private async Task OnZoomImage(WifiSummary wifiSummary)
        {
            if (IsNotBusy)
            {
                IsBusy = true;
                await NavigationService.NavigateAsync<QRCodeViewModel>(wifiSummary, this);
                IsBusy = false;
            }
        }

        public ICommand GetImageFromGalleryCommand { get; private set; }
        private async Task OnGetImageFromGallery()
        {
            FileResult fileResult = await FilePicker.PickAsync(PickOptions.Images);

            if (fileResult != null)
            {
                WifiSummary wifiSummary = await DependencyService.Get<IBarcodeService>().GetImageAsWifiSummary(fileResult);

                if (wifiSummary == null)
                {
                    DependencyService.Get<IToastService>().ToastLongMessage(Language.Language.IncompatibleImage);
                    return;
                }

                WifiSummaryCollection.Add(wifiSummary);
                OriginalWifiSummaryCollection.Add(wifiSummary);
                App.liteDatabase.GetCollection<WifiSummary>().Upsert(wifiSummary);
            }
        }

        #endregion

        public async Task LoadAsync()
        {
            await Task.Run(() =>
            {
                IsDarkTheme = Convert.ToBoolean(Convert.ToUInt32(App.Current.Properties[App.themeKey]));
                OriginalWifiSummaryCollection = App.liteDatabase.GetCollection<WifiSummary>().FindAll().ToList();
                WifiSummaryCollection.ReplaceRange(OriginalWifiSummaryCollection);
            });
        }
    }
}
