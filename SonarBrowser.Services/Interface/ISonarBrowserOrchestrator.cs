using SonarBrowser.Services.DTO;
using SonarBrowser.SonarBrowserOrchestrator.Services.DTO;

namespace SonarBrowser.SonarBrowserOrchestrator.Services
{
    public interface ISonarBrowserOrchestrator
    {
        IssuesSonarSet GetIssuesSonar(SonarRequestGetIssues sonarSettingRequest);
    }
}
