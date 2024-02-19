using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sample.Service.Migrations;

/// <inheritdoc />
public partial class AddedFirstNameLastNameProperties : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "FirstName",
            table: "Persons",
            type: "nvarchar(255)",
            maxLength: 255,
            nullable: false,
            defaultValue: string.Empty);

        migrationBuilder.AddColumn<string>(
            name: "LastName",
            table: "Persons",
            type: "nvarchar(255)",
            maxLength: 255,
            nullable: false,
            defaultValue: string.Empty);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "FirstName",
            table: "Persons");

        migrationBuilder.DropColumn(
            name: "LastName",
            table: "Persons");
    }
}
