using SonarBrowser.WebSite.Factories.Interfaces;
using SonarBrowser.WebSite.ViewModel;
using System.Text;
using System.Web.Mvc;

namespace SonarBrowser.WebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IIssuesSonarViewModelFactory _issuesSonarViewModelFactory;

        public HomeController(IIssuesSonarViewModelFactory issuesSonarViewModelFactory)
        {
            _issuesSonarViewModelFactory = issuesSonarViewModelFactory;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult report(IssuesSonarViewModel model)
        {
            var viewModel = _issuesSonarViewModelFactory.BuildIssuesSonarViewModel(model.DateFrom, model.DateTo.AddDays(1));
            return View("Index", viewModel);
        }

        public FileContentResult DownloadCSV(IssuesSonarViewModel model)
        {
            var viewModel = _issuesSonarViewModelFactory.BuildIssuesSonarViewModel(model.DateFrom, model.DateTo.AddDays(1));
            if (viewModel?.Issues?.IssueSet == null) return default(FileContentResult);

            var data = BuildCsv(viewModel);
            string filename = "reportSonar_" + model?.DateFrom.ToString("dd-MM-yyyy") + "_" + model?.DateTo.ToString("dd-MM-yyyy") + ".csv";
            return File(data, "text/csv", filename);
        }

        private byte[] BuildCsv(IssuesSonarViewModel viewModel)
        {
            StringBuilder stringBuilderCsv = new StringBuilder();
            stringBuilderCsv.Append("Date de creation");
            stringBuilderCsv.Append(";" + "Groupe Ad");
            stringBuilderCsv.Append(";" + "Code Project");
            stringBuilderCsv.Append(";" + "Assignee");
            stringBuilderCsv.Append(";" + "Projet");
            stringBuilderCsv.Append(";" + "Severite");
            stringBuilderCsv.Append(";" + "Regle");
            stringBuilderCsv.Append(";" + "ChangeSet");
            stringBuilderCsv.Append(";" + "Line");
            stringBuilderCsv.Append(";" + "Count Code Line for changeSet");

            stringBuilderCsv.AppendLine();

            foreach (var item in viewModel.Issues.IssueSet)
            {
                stringBuilderCsv.Append(item.IssueDetail.creationDate);
                stringBuilderCsv.Append(";" + item.ADGroup);
                stringBuilderCsv.Append(";" + item.CodeProject);
                stringBuilderCsv.Append(";" + item.IssueDetail.assignee);
                stringBuilderCsv.Append(";" + item.IssueDetail.project);
                stringBuilderCsv.Append(";" + item.IssueDetail.severity);
                stringBuilderCsv.Append(";" + item.IssueDetail.rule);
                stringBuilderCsv.Append(";" + item.ChangetSet);
                stringBuilderCsv.Append(";" + item.IssueDetail.line);
                stringBuilderCsv.Append(";" + item.CodeLineCountForChangeSet);
                stringBuilderCsv.AppendLine();
            }
            return Encoding.UTF8.GetBytes(stringBuilderCsv.ToString());
        }
    }
}