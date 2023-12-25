using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace beheerapplicatie
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //Deze functie wordt gebruikt om een lijst van objecten te deserializen
        public void Serialize<T>(string fileName, ObservableCollection<T> collection)
        {
            try
            {
                string json = JsonConvert.SerializeObject(collection, Formatting.Indented);
                File.WriteAllText(fileName, json);
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}