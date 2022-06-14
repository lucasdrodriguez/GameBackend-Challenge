using System.Threading.Tasks;

namespace Darewise_Challenge.Services
{
    public interface IFilesManager
    {
        Task<string> SaveFile(byte[] content, string extension, string container, string contentType);
    }
}

