using System;
using System.Globalization;
using System.Linq;
using System.Text;
using BookShop.Data;
using BookShop.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShop
{
    public class StartUp
    {
        public static void Main()
        {
            var context = new BookShopContext();
            Console.WriteLine(CountCopiesByAuthor(context));
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var sb = new StringBuilder();

            var authorBooks = context
                .Authors
                .Include(a => a.Books)
                .Select(a => new
                {
                    AuthorName = $"{a.FirstName} {a.LastName}",
                    BookCopies = a.Books.Sum(b => b.Copies)

                })
                .OrderByDescending(a => a.BookCopies)
                .ToList();

            authorBooks.ForEach(a => sb.AppendLine($"{a.AuthorName} - {a.BookCopies}"));

            return sb.ToString().Trim();
            //    var result = context
            //        .Authors
            //        .Include(a => a.Books)
            //        .Select(x => new
            //        {
            //            name = x.FirstName + " " + x.LastName,
            //            Copies = x.Books.Sum(c => c.Copies)

            //        }).OrderByDescending(x => x.Copies).ToList();

            //    var sb = new StringBuilder();
            //    result.ForEach(x => sb.AppendLine($"{x.name} - {x.Copies}"));
            //    return sb.ToString().Trim();
        }
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var result = context.Books.Where(t => t.Title.Length > lengthCheck)
                .Select(x => x.Title).Count();
            return result;
        }
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var result = context.Books
                .Include(x => x.Author)
                .Where(x => x.Author.LastName.ToLower()
                .StartsWith(input.ToLower()))
                .OrderBy(a => a.BookId)
                .Select(x => new
                {
                    x.Title,
                    FullName = x.Author.FirstName + " " + x.Author.LastName
                }).ToList();

            var sb = new StringBuilder();
            result.ForEach(b => sb.AppendLine($"{b.Title} ({b.FullName})"));
            return sb.ToString().Trim();
        }
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            return string.Join(Environment.NewLine, context.Books.Where(x => x.Title.ToLower().Contains(input.ToLower()))
                .Select(t => t.Title)
                .OrderBy(x => x)).Trim();
        }
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var sb = new StringBuilder();

            var authors = context.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => new
                {
                    FullName = $"{a.FirstName} {a.LastName}"
                })
                .OrderBy(a => a.FullName)
                .ToList();

            authors.ForEach(a => sb.AppendLine(a.FullName));

            return sb.ToString().Trim();
        }
        public static string GetBooksReleasedBefore(BookShopContext context, string inputDate)
        {
            var date = DateTime.ParseExact(inputDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            var result = context.Books
                .Where(b => b.ReleaseDate < date)
                .OrderByDescending(x => x.ReleaseDate)
                .ToList();
            var stringBuilder = new StringBuilder();
            foreach (var book in result)
            {
                stringBuilder.AppendLine($"{book.Title} - {book.EditionType.ToString()} - ${book.Price:f2}");
            }
            return stringBuilder.ToString().Trim();
        }
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var sb = new StringBuilder();

            var categoryNames = input.ToLower().Split();

            var bookTitles = context.Books
                .Include(b => b.BookCategories)
                .Where(b => b.BookCategories.Any(c => categoryNames.Contains(c.Category.Name.ToLower())))
                .Select(b => b.Title)
                .OrderBy(b => b);

            foreach (var bookTitle in bookTitles)
            {
                sb.AppendLine(bookTitle);
            }

            return sb.ToString().Trim();

        }
        public static string GetBooksNotRealeasedIn(BookShopContext context, int year)
        {
            var result = context.Books.Where(x => x.ReleaseDate.Value.Year != year).OrderBy(x => x.BookId);

            return string.Join(Environment.NewLine, result.Select(x => x.Title));
        }
        public static string GetBooksByPrice(BookShopContext context)
        {
            var builder = new StringBuilder();
            var result = context.Books.Where(x => x.Price > 40)
                .OrderByDescending(x => x.Price);
            foreach (var book in result)
            {
                builder.AppendLine($"{book.Title} - ${book.Price:f2}");
            }
            return builder.ToString().Trim();
        }
        public static string GetGoldenBooks(BookShopContext context)
        {
            var wantedType = Enum.Parse(typeof(EditionType), "Gold");
            return string.Join(Environment.NewLine, context.Books.Where(x => x.EditionType.Equals(wantedType))
                  .Where(x => x.Copies < 5000)
                  .OrderBy(x => x.BookId)
                  .Select(x => x.Title));
        }
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var restriction = Enum.Parse(typeof(AgeRestriction), command, true);
            var bookTitles = context.Books.Where(x => x.AgeRestriction.Equals(restriction))
                .Select(x => x.Title)
                .OrderBy(x => x)
                .ToArray();
            return string.Join(Environment.NewLine, bookTitles);
        }
    }
}