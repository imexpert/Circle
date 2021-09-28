using System;

namespace Circle.Core.Entities.Concrete
{
    public class Log : LightEntity
    {
        public Guid Id { get; set; }
        public string MessageTemplate { get; set; }
        public string Level { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Exception { get; set; }
    }
}