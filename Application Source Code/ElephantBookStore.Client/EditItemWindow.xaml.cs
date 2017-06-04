using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ElephantBookStore.Data;
using ElephantBookStore.Data.Contracts;
using ElephantBookStore.Data.Models;

namespace ElephantBookStore.Client
{
	/// <summary>
	/// Interaction logic for EditItemWindow.xaml
	/// </summary>
	public partial class EditItemWindow : Window, INotifyPropertyChanged
	{
		private Item item;
		private BookStoreContext context;

		public EditItemWindow()
		{
		}

		public EditItemWindow(Item item)
		{
			InitializeComponent();
			this.ItemToEdit = item;
			this.context = new BookStoreContext();
		}

		public ICollection<Category> Categories
		{
			get
			{
				var name = this.ItemToEdit.GetType().Name;
				return this.context.ProductTypes.First(p => p.ProductTypeName.Contains(name)).Categories;
			}
		}

		public Item ItemToEdit
		{
			get
			{
				return this.item;
			}
			set
			{
				NotifyPropertyChanged();
				this.item = value;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
