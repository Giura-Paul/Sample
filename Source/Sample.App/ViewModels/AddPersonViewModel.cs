using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Sample.DTO;

namespace Sample.App.ViewModels;
public partial class AddPersonViewModel : ObservableObject
{
    private readonly ServiceClient _serviceClient;
    public DialogHandler? DialogHandler { get; set; }
    public PersonDTO AddedPerson { get; set; }

    public AddPersonViewModel()
    {
        _serviceClient = new ServiceClient();

        AddedPerson = new PersonDTO();
    }

    [RelayCommand]
    private async Task AddPersonAsync()
    {
        var result = await _serviceClient.AddPersonAsync(AddedPerson);

        await DialogMessageDisplay.ShowMessageDialogAsync(result.IsSuccess ? "Success" : "Error", result.Message);

        if (result.IsSuccess)
        {
            DialogHandler?.Close();
        }
    }
}
