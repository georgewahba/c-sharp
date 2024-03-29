﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beheerapplicatie.models
{
    //Album model met een naam en een lijst van songs
    public class Album  : NotifyPropertyChanged
    {
        private string name { get; set; }
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        public List<Song> Songs { get; set; }
    }
}
