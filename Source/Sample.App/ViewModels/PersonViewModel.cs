using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Sample.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Sample.App.ViewModels
{
    public class PersonViewModel : ObservableObject
    {
        private ObservableCollection<PersonDTO> _persons = new();

        public ICollection<PersonDTO> Persons => _persons;

        private readonly HttpClient _httpClient;

        public PersonViewModel()
        {
            _httpClient = new HttpClient();
            LoadPersons();
        }

        public async void LoadPersons()
        {
            var response = await _httpClient.GetAsync("https://localhost:7117/api/people");

            if (response.IsSuccessStatusCode)
            {
                var persons = await response.Content.ReadFromJsonAsync<List<PersonDTO>>();

                foreach (var person in persons)
                    _persons.Add(person);
            }
            else
            {
                ContentDialog errorMessageDialog = new ContentDialog
                {
                    Title = "Operation failed",
                    Content = "The list of persons could not be retrieved at this time.",
                    CloseButtonText = "Ok"
                };

                ContentDialogResult result = await errorMessageDialog.ShowAsync();
            }
        }

        public RelayCommand AddPersonCommand => new RelayCommand(AddPerson);

        private async void AddPerson()
        {

        }

        public RelayCommand DeletePersonCommand => new RelayCommand(DeletePerson);

        private async void DeletePerson()
        {

        }

        public RelayCommand EditPersonCommand => new RelayCommand(EditPerson);

        private PersonDTO _selectedPerson;
        public PersonDTO SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                _selectedPerson = value;
                OnPropertyChanged(nameof(SelectedPerson));
            }
        }

        private async void EditPerson()
        {

        }
    }
}
