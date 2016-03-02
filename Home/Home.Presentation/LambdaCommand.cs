using System;
using System.Windows.Input;

namespace Home.Presentation
{
    class LambdaCommand:ICommand
    {
        public delegate void CommandHandle(object parameter);

        public delegate bool CommandCanExecuteHandle(object parameter);

        private CommandHandle _commandHandle;
        private CommandCanExecuteHandle _canExecuteHandle;

        private static CommandCanExecuteHandle _trueHandle = (x => true);

        public LambdaCommand(CommandHandle commandHandle, CommandCanExecuteHandle canExecuteHandle)
        {
            _commandHandle = commandHandle;
            _canExecuteHandle = canExecuteHandle;
        }

        public LambdaCommand(CommandHandle commandHandle)
            : this(commandHandle, _trueHandle)
        {
        }

        public bool CanExecute(object parameter)
        {
            return _canExecuteHandle(parameter);
        }

        public void Execute(object parameter)
        {
            _commandHandle(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }
    }
}
