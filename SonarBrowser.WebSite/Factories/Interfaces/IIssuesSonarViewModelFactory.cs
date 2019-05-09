using SonarBrowser.WebSite.ViewModel;
using System;

namespace SonarBrowser.WebSite.Factories.Interfaces
{
    public interface IIssuesSonarViewModelFactory
    {
        IssuesSonarViewModel BuildIssuesSonarViewModel(DateTime DateFrom, DateTime DateTo);
    }
}
