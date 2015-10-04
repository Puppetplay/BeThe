using System;
using System.Data.Linq.Mapping;

namespace BeThe.Items
{
    [Table]
    public class Relay_W : DbItemBase
    {
        // PRIMARY KEY	IDENTITY
        [Column(AutoSync = AutoSync.OnInsert, IsDbGenerated = true, IsPrimaryKey = true, CanBeNull = false)]
        public override Int64 Id { get; set; }

        [Column(CanBeNull = false)]
        public String GameId  { get; set; }

        [Column(CanBeNull = false)]
        public String Content { get; set; }

    }
}
