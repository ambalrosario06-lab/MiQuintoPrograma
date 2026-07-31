using Spectre.Console;
using MiQuintoPrograma.Models;
using MiQuintoPrograma.Services;

namespace MiQuintoPrograma.Screens;

public class MainScreen(ComputerService computerService)
{
    private readonly ComputerService _service = computerService;
    private bool running = true;

    private readonly (string Text, int Value)[] choices =
    [
        ("1. Mostrar computadoras", 1),
        ("2. Eliminar computadora", 2),
        ("3. Agregar computadora", 3),
        ("4. Actualizar computadora", 4),
        ("5. Salir", 0)
    ];

    public void Show()
    {
        AnsiConsole.Clear();

        var figlet = new FigletText("Computer Store")
        {
            Color = Color.MediumPurple2,
            Justification = Justify.Center
        };

        AnsiConsole.Write(figlet);

        var table = new Table();

        while (running)
        {
            var prompt = new SelectionPrompt<(string Text, int Value)>()
                .Title("Indica una acción a realizar:")
                .AddChoices(choices)
                .HighlightStyle("MediumPurple2")
                .WrapAround()
                .UseConverter(c => $"{c.Text}");

            var option = AnsiConsole.Prompt(prompt);

            switch (option.Value)
            {
                case 1:
                    var computers = _service.FindAll();

                    AnsiConsole.Clear();
                    
                    table.AddColumn("Uuid");
                    table.AddColumn("Nombre");
                    table.AddColumn("Marca");
                    table.AddColumn("Modelo");
                    table.AddColumn("Precio");

                    foreach (Computer computer in computers)
                    {
                        table.AddRow(
                            computer.Uuid.ToString(),
                            computer.Name,
                            computer.Brand,
                            computer.Model,
                            computer.Price.ToString()
                        );
                    }

                    AnsiConsole.Write(table);
                    break;

                case 2:
                    AnsiConsole.Clear();

                    Guid uuid = AnsiConsole.Ask<Guid>("Ingrese el uuid de la computadora:");
                    bool confirmDelete = AnsiConsole.Confirm("¿Está seguro?");

                    AnsiConsole.Clear();

                    if (confirmDelete)
                    {
                        _service.Delete(uuid);
                        AnsiConsole.MarkupLine("[GreenYellow]¡Computadora eliminada![/]");
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[DarkOrange]¡Operación cancelada![/]");
                    }

                    break;

                case 3:
                    AnsiConsole.Clear();

                    string name = AnsiConsole.Ask<string>("Ingrese el nombre:");
                    string brand = AnsiConsole.Ask<string>("Ingrese la marca:");
                    string model = AnsiConsole.Ask<string>("Ingrese el modelo:");
                    int price = AnsiConsole.Ask<int>("Ingrese el precio:");

                    bool confirmCreate = AnsiConsole.Confirm("¿Está seguro?");

                    AnsiConsole.Clear();

                    if (confirmCreate)
                    {
                        _service.Create(Guid.NewGuid(), name, brand, model, price);
                        AnsiConsole.MarkupLine("[GreenYellow]¡Computadora agregada exitosamente![/]");
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[DarkOrange]¡Operación cancelada![/]");
                    }

                    break;

                case 4:
                    AnsiConsole.Clear();

                    Guid uuidToUpdate = AnsiConsole.Ask<Guid>("Ingrese el uuid de la computadora:");
                    string newName = AnsiConsole.Ask<string>("Ingrese el nombre:");
                    string newBrand = AnsiConsole.Ask<string>("Ingrese la marca:");
                    string newModel = AnsiConsole.Ask<string>("Ingrese el modelo:");
                    int newPrice = AnsiConsole.Ask<int>("Ingrese el precio:");

                    bool confirmUpdate = AnsiConsole.Confirm("¿Está seguro?");

                    AnsiConsole.Clear();

                    if (confirmUpdate)
                    {
                        _service.Update(uuidToUpdate, newName, newBrand, newModel, newPrice);
                        AnsiConsole.MarkupLine("[GreenYellow]¡Computadora actualizada exitosamente![/]");
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[DarkOrange]¡Operación cancelada![/]");
                    }

                    break;

                default:
                    running = false;
                    Console.WriteLine("¡Fin de la aplicación!");
                    break;
            }
        }
    }
}
