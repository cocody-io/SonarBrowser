using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarBrowser.ActiveDirectory.Service
{
    public class GroupADSet
    {

        public List<GroupAD> groupADSet { get; set; }

        public GroupADSet()
        {
            groupADSet = new List<GroupAD>();
        }

        public List<string> GetListOfUser()
        {
            List<string> users = new List<string>();
            if (groupADSet == null) return null;
            foreach (var currentGroup in groupADSet)
            {
                foreach (var currentUser in currentGroup.UserSet)
                {
                    users.Add(currentUser.Login);
                }
            }
            return users;
        }

        public void AddLogin(string groupName, string login)
        {
            if (string.IsNullOrEmpty(login) || groupADSet==null ) return;

            GroupAD groupAD = groupADSet.FirstOrDefault(_ => _.Name == groupName);
            if (groupAD==null)
            {
                groupAD = new GroupAD() { Name = groupName, UserSet =new List<UserAD>(){ } };
                groupAD.UserSet.Add(new UserAD() { Login = login });
                groupADSet.Add(groupAD);
            }
            else
            {
                if(!string.IsNullOrEmpty(GetGroupNameByLogin(login)))
                {
                    groupAD.UserSet.Add(new UserAD() { Login = login });
                }
            }
        }

        public string GetGroupNameByLogin(string login)
        {
            if (string.IsNullOrEmpty(login) || groupADSet?.Count== 0) return null;

            foreach (var group in groupADSet)
            {
                if (group.UserSet.Any(_ => string.Compare(_.Login, login, true) == 0))
                {
                    return group.Name;
                }
            }
            
            return null;
        }
    }
}
