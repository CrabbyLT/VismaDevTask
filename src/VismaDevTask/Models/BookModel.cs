using System;

namespace VismaDevTask.Models
{
    public class BookModel
    {
        public BookModel(string name, string author, string category, string language, DateTime publicationDate, string isbn)
        {
            Name = name;
            Author = author;
            Category = category;
            Language = language;
            PublicationDate = publicationDate;
            Isbn = isbn;
        }

        public string Name { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Isbn { get; set; }
    }
}
