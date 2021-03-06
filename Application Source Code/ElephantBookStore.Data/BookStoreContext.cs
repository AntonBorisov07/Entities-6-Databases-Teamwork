namespace ElephantBookStore.Data
{
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity;
	using System.Data.Entity.Infrastructure.Annotations;
	using ElephantBookStore.Data.Contracts;
	using Models;

	public class BookStoreContext : DbContext, IBookStoreContext
	{
		// Your context has been configured to use a 'BookStoreModel' connection string from your application's 
		// configuration file (App.config or Web.config). By default, this connection string targets the 
		// 'ElephantBookStore.Data.BookStoreModel' database on your LocalDb instance. 
		// 
		// If you wish to target a different database and/or database provider, modify the 'BookStoreModel' 
		// connection string in the application configuration file.
		public BookStoreContext()
			: base("name=BookStoreContext")
		{
			Database.SetInitializer(new CreateDatabaseIfNotExists<BookStoreContext>());
			//this.Database.Initialize(false);
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ProductType>().ToTable("ProductTypes");
			modelBuilder.Entity<ProductType>().HasKey(pt => pt.ID);
			modelBuilder.Entity<ProductType>().HasMany(pt => pt.Categories);
			modelBuilder.Entity<ProductType>().Property(p => p.ProductTypeName)
											  .IsRequired()
											  .HasMaxLength(100)
											  .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_UniqueProductTypeName",2)
											  {
												  IsUnique = true
											  }));

			modelBuilder.Entity<Gift>().ToTable("Gifts");
			modelBuilder.Entity<Magazine>().ToTable("Magazines");
			modelBuilder.Entity<Manga>().ToTable("Mangas");
			modelBuilder.Entity<Comic>().ToTable("Comics");

			base.OnModelCreating(modelBuilder);
		}

		// Add a DbSet for each entity type that you want to include in your model. For more information 
		// on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

		// public virtual DbSet<MyEntity> MyEntities { get; set; }
		public virtual DbSet<ProductType> ProductTypes { get; set; }

		public virtual DbSet<Category> Categories { get; set; }

		public virtual DbSet<Item> Items { get; set; }
	}

	//public class MyEntity
	//{
	//    public int Id { get; set; }
	//    public string Name { get; set; }
	//}
}