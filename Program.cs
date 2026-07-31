global using Spectre.Console;

using MiQuintoPrograma.Data;
using MiQuintoPrograma.Repositories;
using MiQuintoPrograma.Screens;
using MiQuintoPrograma.Services;

class Program
{
    public static void Main(string[] args)
    {
        Database database = new("Database/computerstore.db");

        ComputerRepository computerRepository = new(database);

        ComputerService computerService = new(computerRepository);

        MainScreen mainScreen = new(computerService);

        mainScreen.Show();
    }
}
