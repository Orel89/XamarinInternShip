using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contacts.Model
{
    public class ProfileModel : IEntityBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
