using System;
using System.Windows.Input;

namespace Visual_Novel_Universe
{
    public class RelayCommand<T> : ICommand
    {
        #region Fields

        private readonly Action<T> _Execute;
        private readonly Predicate<T> _CanExecute;

        #endregion

        #region Constructors

        public RelayCommand()
        {
        }

        public RelayCommand(Action<T> Execute)
            : this(Execute, null)
        {
        }
        
        public RelayCommand(Action<T> Execute, Predicate<T> CanExecute)
        {
            if (Execute == null)
                throw new ArgumentNullException(nameof(Execute));

            _Execute = Execute;
            _CanExecute = CanExecute;
        }

        #endregion

        #region ICommand Members
        
        public bool CanExecute(object Parameter)
        {
            return _CanExecute?.Invoke((T)Parameter) ?? true;
        }
        
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        
        public void Execute(object Parameter)
        {
            _Execute((T)Parameter);
        }

        #endregion
    }
}
