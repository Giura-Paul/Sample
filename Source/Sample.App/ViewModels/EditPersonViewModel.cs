using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using Newtonsoft.Json;
using Sample.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Sample.App.ViewModels
{
    public partial class EditPersonViewModel : ObservableObject
    {
        private readonly int _personId;
        private readonly HttpClient _httpClient;
        public UpdatedPersonDTO UpdatedPersonDTO { get; }
        public DialogHandler? DialogHandler { get; set; }
        public EditPersonViewModel(PersonDTO personDTO)
        {
            _personId = personDTO.Id;

            UpdatedPersonDTO = new UpdatedPersonDTO
            {
                FirstName = personDTO.FirstName,
                LastName = personDTO.LastName
            };
        }


        [RelayCommand]
        private async Task EditPerson()
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7117/api/people/{_personId}",
                                             UpdatedPersonDTO);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var updateResult = JsonConvert.DeserializeObject<Result>(content);

                if (updateResult.IsSuccess is true)
                {

                }
            }
        }
    }
}
