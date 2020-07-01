using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QRede.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubtitledImage : ContentView
    {
        public SubtitledImage()
        {
            InitializeComponent();
        }

        public string ImageText
        {
            get { return (string)GetValue(ImageTextProperty); }
            set { SetValue(ImageTextProperty,value); }
        }


        public static readonly BindableProperty ImageTextProperty =
            BindableProperty.Create(
                propertyName: nameof(ImageText),
                returnType: typeof(string),
                declaringType: typeof(SubtitledImage),
                defaultValue: default,
                defaultBindingMode: BindingMode.OneWay
                );

        public Xamarin.Forms.ImageSource ImageSource
        {
            get { return (Xamarin.Forms.ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly BindableProperty ImageSourceProperty =
            BindableProperty.Create(
                propertyName: nameof(ImageSource),
                returnType: typeof(Xamarin.Forms.ImageSource),
                declaringType: typeof(SubtitledImage),
                defaultValue: default,
                defaultBindingMode: BindingMode.OneWay
                );
        
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(
                propertyName: nameof(Command),
                returnType: typeof(ICommand),
                declaringType: typeof(SubtitledImage),
                defaultValue: default,
                defaultBindingMode: BindingMode.OneWay
                );

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(
                propertyName: nameof(CommandParameter),
                returnType: typeof(object),
                declaringType: typeof(SubtitledImage),
                defaultValue: default,
                defaultBindingMode: BindingMode.OneWay
                );

        private void Button_Clicked(object sender, EventArgs e)
        {
            if(Command!=null && CommandParameter != null)
            {
                Command.Execute(CommandParameter);
            }
        }
    }
}