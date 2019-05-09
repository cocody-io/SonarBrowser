using SonarBrowser.Services.DTO;
using SonarBrowser.Sonar.Service.Model;
using SonarBrowser.Sonar.Services.Model.DTO;
using System.Threading.Tasks;

namespace SonarBrowser.Sonar.Service
{
    public interface ISonarConnector
    {
        Task<SearchIssuesResponse> GetIssuesAsync(SonarRequestGetIssues sonarSettingRequest);

        Source GetChangeSet(SonarRequestGetChangeSet sonarRequestGetChangeSet);
    }
}
