using ProiectPOOSAM;

namespace ProiectPOoSAM;

public class Admin : USER
{
    public Admin(string username, string password,Role role) : base(username, password, Role.Admin)
    { }
}