using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetzweltExam.Models
{

    public class DataModel
    {
        public List<TerritoryModel> Data { get; set; }
    }

    public class TerritoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Parent { get; set; }
    }
}
