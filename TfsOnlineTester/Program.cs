using SonarBrowser.Infrastructure.Cache;
using SonarBrowser.Infrastructure.ContextProvider;
using SonarBrowser.Infrastructure.Logging;
using SonarBrowser.Infrastructure.WebClient;
using SonarBrowser.Tfs.Service;
using SonarBrowser.Tfs.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TfsOnlineTester
{
    class Program
    {
        static void Main(string[] args)
        {
            string tfsUrl = "http://tfs.cdbdx.biz:8080/tfs/DefaultCollection/";

            string personalAccessToken = "it5ug4bzryfm5b4ar2z4l5ufr7gd6ha73r7hnh4xydkrp5b7i4ja";

            string credentials = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", "", personalAccessToken)));

            TfsConnector _tfs = new TfsConnector(new TfsSettings() { Url = tfsUrl, Token = credentials}, new CacheManager(new CacheClient()) , new WebApiClient(new Log4NetLoggingService(new HttpContextService())) );

            //Changeset c = _tfs.Changesets().GetChangeset("625577").Result;

            //Changes cs = _tfs.Changesets().GetChangesetChanges("625577").Result;

            //foreach (ValueItem v in cs.Value)
            //{
            //    SearchHistory search = _tfs.VersionControl().FileHistory(v.Item.Path, v.Item.Version.ToString()).Result;
            //}

     //       Console.WriteLine(_tfs.GetAgressoCode(618472).Result);
     
            Console.WriteLine(_tfs.GetNumberOfImpactedLines(631552).Result);

            Console.Read();
        }

        
    }
}
