namespace FullStackCI.Data
{
    using FullStackCI.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    public class ApplicationDbContext : DbContext
    {
        
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {
            }

            public DbSet<Book> Books { get; set; }
            public DbSet<Author> Authors { get; set; }
            public DbSet<Category> Categories { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // Configuración de relación uno-a-muchos entre Category y Book
                modelBuilder.Entity<Book>()
                    .HasOne(b => b.Category)
                    .WithMany(c => c.Books)
                    .HasForeignKey(b => b.CategoryId);

                // Configuración de relación uno-a-muchos entre Author y Book
                modelBuilder.Entity<Book>()
                    .HasOne(b => b.Author)
                    .WithMany(a => a.Books)
                    .HasForeignKey(b => b.AuthorId);

                // Datos de prueba
                SeedData(modelBuilder);
            }

            private void SeedData(ModelBuilder modelBuilder)
            {
                // Categorías
                var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Ficción", Description = "Libros de ficción y novelas" },
                new Category { Id = 2, Name = "Ciencia", Description = "Libros científicos y técnicos" },
                new Category { Id = 3, Name = "Historia", Description = "Libros históricos" },
                new Category { Id = 4, Name = "Fantasy", Description = "Libros de fantasía" }
            };

                // Autores
                var authors = new List<Author>
            {
                new Author { Id = 1, Name = "Gabriel García Márquez", Nationality = "Colombiano", BirthYear = 2007},
                new Author { Id = 2, Name = "J.K. Rowling", Nationality = "Británica", BirthYear = 1965 },
                new Author { Id = 3, Name = "Stephen King", Nationality = "Estadounidense", BirthYear = 2000 },
                new Author { Id = 4, Name = "Isaac Asimov", Nationality = "Ruso", BirthYear =1987 },
                new Author { Id = 5, Name = "Yuval Noah Harari", Nationality = "Israelí", BirthYear = 2005 }
            };

                // Libros
                var books = new List<Book>
            {
                new Book {
                    Id = 1,
                    Title = "Cien años de soledad",
                    ISBN = "978-0307474728",
                    PublicationYear = 2010,
                    Pages = 417,
                    Description = "Una obra maestra de la literatura latinoamericana",
                    CategoryId = 1,
                    AuthorId = 1
                },
                new Book {
                    Id = 2,
                    Title = "Harry Potter y la piedra filosofal",
                    ISBN = "978-8478884452",
                    PublicationYear = 2020,
                    Pages = 320,
                    Description = "El primer libro de la serie Harry Potter",
                    CategoryId = 4,
                    AuthorId = 2
                },
                new Book {
                    Id = 3,
                    Title = "It",
                    ISBN = "978-1501142970",
                    PublicationYear = 1986,
                    Pages = 1138,
                    Description = "Una novela de terror",
                    CategoryId = 1,
                    AuthorId = 3
                },
                new Book {
                    Id = 4,
                    Title = "Fundación",
                    ISBN = "978-0553293357",
                    PublicationYear = 2025,
                    Pages = 255,
                    Description = "Primer libro de la serie Fundación",
                    CategoryId = 2,
                    AuthorId = 4
                },
                new Book {
                    Id = 5,
                    Title = "Sapiens: De animales a dioses",
                    ISBN = "978-8499926223",
                    PublicationYear = 2011,
                    Pages = 496,
                    Description = "Breve historia de la humanidad",
                    CategoryId = 3,
                    AuthorId = 5
                }
            };

                modelBuilder.Entity<Category>().HasData(categories);
                modelBuilder.Entity<Author>().HasData(authors);
                modelBuilder.Entity<Book>().HasData(books);
            }
        }
    }

