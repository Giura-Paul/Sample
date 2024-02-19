using System.Collections.ObjectModel;
using System.Net.Http.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Sample.DTO;

namespace Sample.App.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private ObservableCollection<PersonDTO> _persons = new();

    public ICollection<PersonDTO> Persons => _persons;

    private readonly HttpClient _httpClient;
    private readonly IXamlRootProvider _xamlRootProvider;

    public MainViewModel(IXamlRootProvider xamlRootProvider)
    {
        _httpClient = new HttpClient();
        _xamlRootProvider = xamlRootProvider;
    }

    [RelayCommand]
    public async Task LoadPersonsAsync()
    {
        _persons.Clear();

        var response = await _httpClient.GetAsync("https://localhost:7117/api/people");

        if (response.IsSuccessStatusCode)
        {
            List<PersonDTO> persons = await response.Content.ReadFromJsonAsync<List<PersonDTO>>();

            if (persons is not null)
            {
                foreach (PersonDTO person in persons)
                    _persons.Add(person);
            }
        }
        else
        {
            ContentDialog errorMessageDialog = new ContentDialog
            {
                Title = "Operation failed",
                Content = "The list of persons could not be retrieved at this time.",
                CloseButtonText = "Ok",
            };

            ContentDialogResult result = await errorMessageDialog.ShowAsync();
        }
    }

    [RelayCommand]
    private async Task ShowEditPersonDialogAsync(PersonDTO personDTO)
    {
        var vm = new EditPersonViewModel(personDTO);

        // Create a ContentDialog for editing the person
        var editPersonDialog = await new EditPersonDialog(vm, _xamlRootProvider.XamlRoot).ShowAsync();

        await LoadPersonsAsync();
    }
}
