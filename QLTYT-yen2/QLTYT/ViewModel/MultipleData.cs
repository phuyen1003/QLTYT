using QLTYT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLTYT.ViewModel
{
    public class MultipleData
    {
        public IEnumerable<VacXin> vacXins { get; set; }
        public IEnumerable<Benh> benhs { get; set; }
    }
}