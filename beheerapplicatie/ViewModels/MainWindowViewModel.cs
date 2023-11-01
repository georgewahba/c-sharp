using RekenmachineCollege4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RekenmachineCollege4.ViewModels
{
    class MainWindowViewModel : RekenData
    {
        public ICommand CalculatorActionButton { get; private set; }
        public ICommand CalculatorNumberButton { get; private set; }

        private const string ErrorMessage = "Error!";


        public MainWindowViewModel()
        {
            LastResult = "0";
            MemoryNumber = "0";
            LastAction = "";

            CalculatorActionButton = new RelayCommand(ExecuteCalculatorActionButton, CanExecuteCalculatorActionButton);
            CalculatorNumberButton = new RelayCommand(ExecuteCalculatorNumberButton, CanExecuteCalculatorNumberButton);
        }

        private bool CanExecuteCalculatorActionButton(object parameter)
        {
            if (parameter is string tag)
            {
                if(tag == "=" && LastAction == "/" && LastResult == "0")
                {
                    LastResult = "Je bent een sul, als je deelt door nul!";
                    return false;
                }
            }
            return true;
        }

        private void ExecuteCalculatorActionButton(object parameter)
        {
            // Voer hier de actie uit die moet worden uitgevoerd wanneer het commando wordt geactiveerd
            if (parameter is string tag)
            {
                try
                {
                    switch (tag)
                    {
                        case ",":
                            if (!LastResult.Contains(","))
                            {
                                LastResult += ",";
                            }
                            break;
                        case "backspace":
                            if (LastResult.Length > 1)
                            {
                                LastResult = LastResult.Remove(LastResult.Length - 1, 1);
                            }
                            if (LastResult.Length == 1)
                            {
                                LastResult = "0";
                            }
                            break;
                        case "clear":
                            LastAction = "0";
                            LastResult = "0";
                            break;
                        case "=":
                            float.TryParse(LastResult, out float number);
                            float.TryParse(MemoryNumber, out float ans);
                            switch (LastAction)
                            {
                                case "+":
                                    ans += number;
                                    break;
                                case "-":
                                    ans -= number;
                                    break;
                                case "*":
                                    ans *= number;
                                    break;
                                case "/":
                                    ans /= number;
                                    break;
                            }
                            LastResult = ans.ToString();
                            LastAction = tag;
                            break;
                        default:
                            LastAction = tag;
                            MemoryNumber = LastResult;
                            LastResult = LastAction;
                            break;
                    }
                }
                catch
                {
                    LastResult = ErrorMessage;
                }
            }
        }

        private bool CanExecuteCalculatorNumberButton(object parameter)
        {
            // Voeg hier je logica toe om te controleren of het commando kan worden uitgevoerd
            return true;
        }

        private void ExecuteCalculatorNumberButton(object parameter)
        {
            if (parameter is string tag)
            {
                if(LastAction == "=")
                {
                    LastResult = "";
                    LastAction = "";
                }
                else if (LastResult == "0" || LastResult == LastAction || LastResult == ErrorMessage)
                {
                    LastResult = "";
                }
                LastResult += tag;
                return;
            }
        }
    }
}
