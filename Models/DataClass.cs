namespace MyAPIApp.Models
{
    public class Contacts
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
    }



    public class SaveContacts
    {
        public string Name { get; set; }
        public string ContactNumber { get; set; }
    }
}
