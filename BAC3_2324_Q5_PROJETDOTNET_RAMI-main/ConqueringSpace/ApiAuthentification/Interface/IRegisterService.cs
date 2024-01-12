namespace ApiAuthentification.Interface
{
    public interface IRegisterService
    {
        string Register(string name, string password, string role);

    }
}