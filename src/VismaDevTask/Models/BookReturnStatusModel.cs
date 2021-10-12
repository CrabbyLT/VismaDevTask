using System;

namespace VismaDevTask.Models
{
    public class BookReturnStatusModel
    {
        public string Isbn { get; set; }
        public string TakenBy { get; set; }
        public bool Returned { get; set; }
        public DateTime DateTaken { get; set; }
    }
}
