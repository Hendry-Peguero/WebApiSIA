
namespace WebApiSIA.Core.Application.Interfaces.Helpers
{
    public interface IMd5Helper
    {
        string GenerateMd5(string input);
        bool VerifyMd5(string input, string md5Hash);
    }
}
