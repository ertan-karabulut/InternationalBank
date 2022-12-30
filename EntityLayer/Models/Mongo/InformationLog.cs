using CoreLayer.Utilities.Logger;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Models.Mongo
{
    public class InformationLog : ILogDto
    {
        public string Token { get; set; }
        public string Message { get; set; }
        public string Page { get; set; }
        public string Method { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreateDate { get; set; }
        public string Ip { get; set; }
    }
}
