using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SonarBrowser.WebSite.Models.Datatable
{
    public class DataTableJson
    {
        //[JsonProperty("draw", NullValueHandling = NullValueHandling.Ignore)]
        //public int PageNumber { get; set; }

        //public int recordsTotal { get; set; }

        //public int recordsFiltered { get; set; }

        public List<DataTableRow> data { get; set; }

    }
}