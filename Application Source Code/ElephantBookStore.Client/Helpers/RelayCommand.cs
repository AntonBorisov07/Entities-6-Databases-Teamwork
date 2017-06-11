namespace ElephantBookStore.Client.Helpers
{
	using System;
	using System.Windows.Input;

	public class RelayCommand : ICommand
	{
		private Action<object> execute;
		private Predicate<object> canExecute;

		public event EventHandler CanExecuteChanged;

		public RelayCommand(Action<object> execute) : this(execute, DefaultCanExecute)
		{
		}

		public RelayCommand(Action<object> execute, Predicate<object> canExecute)
		{
			this.execute = execute ?? throw new ArgumentNullException("execute");
			this.canExecute = canExecute ?? throw new ArgumentNullException("canExecute");
		}

		public void RaiseCanExecuteChanged()
		{
			this.CanExecuteChanged?.Invoke(this, new EventArgs());
		}

		public bool CanExecute(object parameter)
		{
			return this.canExecute(parameter);
		}

		public void Execute(object parameter)
		{
			this.execute(parameter);
		}

		private static bool DefaultCanExecute(object parameter)
		{
			return true;
		}

	}
}
