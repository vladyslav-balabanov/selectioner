using System;
using MongoDB.Bson.Serialization.Attributes;

namespace BreadersHomebook.Models
{
    public class ArticleModel
    {
        [BsonId]
        public int Id { get; set; }
        public string Description { get; set; }
        public string Appearance { get; set; }

        public void Print()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(Description);
            Console.ResetColor();
            Console.WriteLine("\n");
            Console.WriteLine(Appearance);
        }
    }
}