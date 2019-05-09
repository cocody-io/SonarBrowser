using SonarBrowser.ActiveDirectory.Service.Interface;
using SonarBrowser.ActiveDirectory.Service.Model;
using SonarBrowser.Infrastructure.Cache;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;

namespace SonarBrowser.ActiveDirectory.Service
{
    /// <summary>
    /// Active directory service
    /// </summary>
    public class ActiveDirectoryService : IActiveDirectoryService
    {
        private AdSettings _adSettings;
        private ICacheManager _cacheManager;

        public ActiveDirectoryService(AdSettings adSettings, ICacheManager cacheManager)
        {
            _adSettings = adSettings;
            _cacheManager = cacheManager;
        }

        public GroupADSet GetUsersByGroupAd(List<string> GroupSet)
        {
            if (GroupSet == null || GroupSet.Count == 0) return null;

            string cacheKey = string.Format("GetUsersByGroupAd_{0}", string.Join(",", GroupSet));
            return _cacheManager.Resolve(cacheKey, () =>
            {
                GroupADSet _groupAD = new GroupADSet();
                _groupAD.groupADSet = new List<GroupAD>();
                foreach (var currentGroupAd in GroupSet)
                {
                    var users = GetUsersByGroupAd(currentGroupAd);
                    GroupAD groupAd = new GroupAD(currentGroupAd, users);
                    _groupAD.groupADSet.Add(groupAd);
                }
                return _groupAD;
            });
        }

        private List<string> GetUsersByGroupAd(string groupAd)
        {
            PrincipalContext oPrincipalContext = new PrincipalContext(ContextType.Domain);
            List<Principal> principals = new List<Principal>();
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, _adSettings.DomainName, null, _adSettings.Login, _adSettings.Pass);

            GroupPrincipal groupP = GroupPrincipal.FindByIdentity(ctx, groupAd);
            return groupP?.Members?.Select(_ => _.Name.ToLower().Trim().Replace(" ",".")).ToList();
        }

    }
}
