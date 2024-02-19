using System.Net.Http.Json;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Sample.DTO;
using Windows.UI.Popups;

namespace Sample.App.ViewModels;

public partial class EditPersonViewModel : ObservableObject
{
    private readonly int _personId;
    private readonly ServiceClient _serviceClient;
    public UpdatedPersonDTO UpdatedPersonDTO { get; }
    public DialogHandler? DialogHandler { get; set; }
    public EditPersonViewModel(PersonDTO personDTO)
    {
        _personId = personDTO.Id;
        _serviceClient = new ServiceClient();

        UpdatedPersonDTO = new UpdatedPersonDTO
        {
            FirstName = personDTO.FirstName,
            LastName = personDTO.LastName,
        };
    }

    [RelayCommand]
    private async Task EditPersonAsync()
    {
        var result = await _serviceClient.UpdatePersonAsync(_personId, UpdatedPersonDTO);

        await DialogMessageDisplay.ShowMessageDialogAsync(result.IsSuccess ? "Success" : "Error");

        if (result.IsSuccess)
        {
            DialogHandler?.Close();
        }
    }
}
