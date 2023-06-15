using EntryLog.Enums;

namespace EntryLog.Entities
{
    public class PeopleEntryLogs
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
    }
}
