using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contacts.Model
{
    [Table("Contacts")]
    public class ContactModel : IEntityBase
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get ; set ; }
        public string UserLogin { get; set; }
        
        public string NickName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string PictureUrl { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
