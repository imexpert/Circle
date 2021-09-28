﻿using System;

namespace Circle.Core.Entities.Concrete
{
    public class UserGroup : LightEntity
    {
        public Guid GroupId { get; set; }
        public Guid UserId { get; set; }
        public string RecordUsername { get; set; }
        public DateTime RecordDate { get; set; }
        public string UpdateUsername { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Ip { get; set; }
    }
}