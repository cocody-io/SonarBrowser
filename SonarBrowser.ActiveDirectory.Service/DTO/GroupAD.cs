using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarBrowser.ActiveDirectory.Service
{
    public class GroupAD
    {
        public string Name { get; set; }
        public List<UserAD> UserSet { get; set; }


        public GroupAD() { UserSet = new List<UserAD>(); }

        public GroupAD(string name)
        {
            Name = name;
            UserSet = new List<UserAD>();
        }

        public GroupAD(string name, List<string> Users)
        {
            Name = name;
            UserSet = new List<UserAD>();
            Users.ForEach(u=> UserSet.Add(new UserAD() { Login = u }));
        }

        public string GetGroupNameByLogin(string login)
        {
            if (string.IsNullOrEmpty(login) || UserSet?.Count== 0) return null;
            
            if(UserSet.Any(_=> string.Compare(_.Login,login,true)==0))
            {
                return Name;
            }
            return null;
        }
    }
}
