using daza_store_be.Entities;

namespace daza_store_be.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
    }
}