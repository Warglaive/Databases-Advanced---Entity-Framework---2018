using System;
using System.Collections.Generic;
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
            Console.WriteLine(GetBooksByCategory(context, input));
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var TotalTitles = new List<string>();
            var inputArgs = input.Split(new[] { ' ' }
                , StringSplitOptions.RemoveEmptyEntries);

            var currentCategoryTitles = context.Books
                .Include(x => x.BookCategories)
                .Where(b => b.BookCategories
                    .Any(c => inputArgs.Contains(c.Category.Name.ToLower())))
                .Select(t => t.Title)
                .OrderBy(t => t);


            TotalTitles.AddRange(currentCategoryTitles);

            return string.Join(Environment.NewLine, TotalTitles);

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
