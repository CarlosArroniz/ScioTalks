using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ScioTalks.Model;
using Xamarin.Forms;

namespace ScioTalks.ViewModel
{
    public class SpeakersViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool isBusy;
        public ObservableCollection<Speaker> Speakers { get; set; }
        public Command GetSpeakersCommand { get; set; }

        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                isBusy = value;
                OnPropertyChanged();
                GetSpeakersCommand.ChangeCanExecute();
            }
        }

        public SpeakersViewModel()
        {
            Speakers = new ObservableCollection<Speaker>();
            GetSpeakersCommand = new Command(
                async () => await GetSpeakers(),
                () => !IsBusy);
        }

        private async Task GetSpeakers()
        {
            if (IsBusy)
                return;

            Exception error = null;
            try
            {
                IsBusy = true;

                using (var client = new HttpClient())
                {
                    var json = await client.GetStringAsync("http://demo4404797.mockable.io/speakers");

                    var items = JsonConvert.DeserializeObject<List<Speaker>>(json);

                    Speakers.Clear();
                    foreach (var item in items)
                        Speakers.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex);
                error = ex;
            }
            finally
            {
                IsBusy = false;
            }

            if (error != null)
                await Application.Current.MainPage.DisplayAlert("Error!", error.Message, "OK");

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
