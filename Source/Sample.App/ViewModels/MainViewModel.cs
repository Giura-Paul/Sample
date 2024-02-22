using System.Collections.ObjectModel;
using System.Net.Http.Json;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Sample.DTO;

namespace Sample.App.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private ObservableCollection<PersonDTO> _persons = new();
    private readonly ServiceClient _serviceClient;
    public ICollection<PersonDTO> Persons => _persons;

    private readonly IXamlRootProvider _xamlRootProvider;

    public MainViewModel(IXamlRootProvider xamlRootProvider)
    {
        _serviceClient = new ServiceClient();
        _xamlRootProvider = xamlRootProvider;
    }

    [RelayCommand]
    public async Task LoadPersonsAsync()
    {
        _persons.Clear();

        var result = await _serviceClient.GetAllPersonsAsync();

        var persons = result.Data;

        if (persons is not null)
        {
            foreach (var person in persons)
                _persons.Add(person);
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

    [RelayCommand]
    private async Task ShowAddPersonDialogAsync()
    {
        var vm = new AddPersonViewModel();

        var addPersonDialog = await new AddPersonDialog(vm, _xamlRootProvider.XamlRoot).ShowAsync();

        await LoadPersonsAsync();
    }

    [RelayCommand]
    private async Task ShowDeletePersonDialogAsync(PersonDTO personDTO)
    {
        var deletePersonDialog = await new ContentDialog {
            Title = "Delete Person",
            Content = "Are you sure you want to delete this person?",
            PrimaryButtonText = "Delete",
            CloseButtonText = "Cancel",
            XamlRoot = _xamlRootProvider.XamlRoot,
        }.ShowAsync();

        if (deletePersonDialog == ContentDialogResult.Primary)
        {
            var result = await _serviceClient.DeletePersonAsync(personDTO.Id);

            await DialogMessageDisplay.ShowMessageDialogAsync(result.IsSuccess ? "Success" : "Error", result.Message);
        }

        await LoadPersonsAsync();
    }
}
