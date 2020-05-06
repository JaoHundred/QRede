using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QRede.Modules
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QRCollectionView : ContentPage
    {
        public QRCollectionView()
        {
            InitializeComponent();
            BindingContext = new QRCollectionViewModel();
        }

        private async void OnApeared(object sender, EventArgs e)
        {
            await (BindingContext as QRCollectionViewModel).LoadAsync();
        }
    }
}