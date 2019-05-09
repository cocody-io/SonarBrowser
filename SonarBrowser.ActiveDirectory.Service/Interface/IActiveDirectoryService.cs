using SonarBrowser.ActiveDirectory.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarBrowser.ActiveDirectory.Service.Interface
{
    public interface IActiveDirectoryService
    {
        GroupADSet GetUsersByGroupAd(List<string> GroupSet);
    }
}
