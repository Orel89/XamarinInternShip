using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contacts.Model
{
    [Table("Users")]
    public class UserModel : IEntityBase
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set;}
        [Unique]
        public string Login { get; set; }
        public string Password { get; set; }

    }
}
