using System;

namespace VismaDevTask.Requests
{
    public class TakeBookRequest
    {
        public string TakenBy { get; set; }
        public string Isbn { get; set; }
        public TimeSpan TakenDuration { get; set; }

        public TakeBookRequest(string takenBy, string isbn, TimeSpan takenDuration)
        {
            TakenBy = takenBy;
            Isbn = isbn;
            TakenDuration = takenDuration;
        }
    }
}
