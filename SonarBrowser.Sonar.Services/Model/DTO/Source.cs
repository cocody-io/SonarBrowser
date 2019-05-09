using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarBrowser.Sonar.Services.Model.DTO
{
    public class Source
    {
        public int Line { get; set; }
        public int ChangeSet { get; set; }
        public string AuthorEmail { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
