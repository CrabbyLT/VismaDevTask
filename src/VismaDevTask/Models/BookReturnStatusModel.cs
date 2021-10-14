using System;

namespace VismaDevTask.Models
{
    public class BookReturnStatusModel
    {
        public string Isbn { get; set; }
        public string TakenBy { get; set; }
        public bool Returned { get; set; }
        public DateTime DateTaken { get; set; }
        public TimeSpan DurationTaken { get; set; }

        public BookReturnStatusModel(string isbn, string takenBy, bool returned, DateTime dateTaken, TimeSpan durationTaken)
        {
            Isbn = isbn;
            TakenBy = takenBy;
            Returned = returned;
            DateTaken = dateTaken;
            DurationTaken = durationTaken;
        }
    }
}
