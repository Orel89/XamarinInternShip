using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contacts.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainListPage : ContentPage
    {
        public string[] Items { get; set; }
        public MainListPage()
        {
            InitializeComponent();
        }
        private void Item_Selected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}