using SonarBrowser.Tfs.Service.Connectors;
using System.Threading.Tasks;

namespace SonarBrowser.Tfs.Service.Interface
{
    public interface ITfsConnector
    {
        Task<string> GetAgressoCode(int? changesetId);
        Task<int> GetNumberOfImpactedLines(int? changesetId);
    }
}
