using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScioTalks.ViewModel;
using Xamarin.Forms;

namespace ScioTalks.View
{
    public partial class SpeakersPage : ContentPage
    {
        private SpeakersViewModel vm;
        public SpeakersPage()
        {
            InitializeComponent();
            vm = new SpeakersViewModel();
            BindingContext = vm;
        }
    }
}
