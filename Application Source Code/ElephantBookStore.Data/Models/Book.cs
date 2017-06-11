namespace ElephantBookStore.Data.Models
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	[Table("Books")]
	public class Book : Item
	{
		[Required]
		public string Author { get; set; }
	}
}
