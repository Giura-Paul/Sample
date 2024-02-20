using System.ComponentModel.DataAnnotations.Schema;

namespace Sample.Data;

public class Person
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
