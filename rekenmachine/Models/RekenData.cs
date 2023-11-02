using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RekenmachineCollege4.Models
{
    internal class RekenData : NotifyPropertyChanged
    {
        private string _lastResult;
        public string LastResult
        {
            get
            {
                return _lastResult;
            }
            set
            {
                _lastResult = value;
                RaisePropertyChange("LastResult");
            }
        }

        public string MemoryNumber { get; set; }

        public string LastAction { get; set; }
    }
}
