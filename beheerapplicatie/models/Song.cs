using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beheerapplicatie.models
{
    public class Song : NotifyPropertyChanged
    {
        private string _title;
        private string _artist;
        public int Id { get; set; }
        public string Title { get { return _title; } set { _title = value; RaisePropertyChange("Title"); } }


        public string Artist { get { return _artist; } set { _artist = value; RaisePropertyChange("Artist"); } }
    }
}
