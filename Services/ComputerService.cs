using MiQuintoPrograma.Models;
using MiQuintoPrograma.Repositories;

namespace MiQuintoPrograma.Services;

public class ComputerService(ComputerRepository computerRepository)
{
    private readonly ComputerRepository _computerRepository = computerRepository;

    public List<Computer> FindAll()
    {
        return _computerRepository.FindAll();
    }

    public int Delete(Guid uuid)
    {
        // Lo correcto sería validar primero si existe ese registro
        return _computerRepository.Delete(uuid);
    }

    public int Create(Guid uuid, string name, string brand, string model, int price)
    {
        // Aquí puedo validar que ninguno de esos campos vengan vacíos
        return _computerRepository.Create(uuid, name, brand, model, price);
    }

    public int Update(Guid uuid, string name, string brand, string model, int price)
    {
        return _computerRepository.Update(uuid, name, brand, model, price);
    }
}
