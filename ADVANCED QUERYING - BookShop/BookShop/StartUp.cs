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
            var input = Console.ReadLine();
            Console.WriteLine(GetBooksByAuthor(context, input));
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