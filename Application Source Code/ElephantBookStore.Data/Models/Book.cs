namespace ElephantBookStore.Data.Models
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	[Table("Books")]
	public class Book : Item
	{
		[Required]
		[MinLength(3)]
		[MaxLength(250)]
		public string Author { get; set; }
	}
}
