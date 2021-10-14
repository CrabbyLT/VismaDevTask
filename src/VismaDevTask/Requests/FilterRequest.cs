namespace VismaDevTask.Requests
{
    public class FilterRequest
    {
        public FilterRequest(string field, string value)
        {
            Field = field;
            Value = value;
        }

        public string Field { get; set; }
        public string Value { get; set; }
    }
}
