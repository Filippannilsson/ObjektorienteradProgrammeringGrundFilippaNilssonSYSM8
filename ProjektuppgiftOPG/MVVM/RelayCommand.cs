using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjektuppgiftOPG.MVVM
{
    public class RelayCommand : ICommand
    {
        //Fält för att lagra metoder som definierar kommandots logik
        private Action<object> execute;

        //Fält för att avgöra om kommandot kan köras
        private Func<object, bool> canExecute;
        
        //Event som signalerar när kommandots körbarhet har ändrats
        public event EventHandler? CanExecuteChanged;

        //Konstruktor
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        //Kontrollerar om kommandot kan köras
        public bool CanExecute(object? parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        //Kör den logik som tilldelats för kommandot
        public void Execute(object? parameter)
        {
            execute(parameter);
        }
    }
}
