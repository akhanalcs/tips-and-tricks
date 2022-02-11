using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Log
    {
        /*
        [Id] int IDENTITY(1,1) NOT NULL,
        [Message] nvarchar(max) NULL,
        [MessageTemplate] nvarchar(max) NULL,
        [Level] nvarchar(128) NULL,
        [TimeStamp] datetime NOT NULL,
        [Exception] nvarchar(max) NULL,
        [Properties] nvarchar(max) NULL
        */

        public int Id { get; set; }
        [Column(TypeName = "VARCHAR(500)")]
        public string Message { get; set; }
        [Column(TypeName = "VARCHAR(500)")]
        public string MessageTemplate { get; set; }
        [Column(TypeName = "VARCHAR(500)")]
        public string Level { get; set; }
        public DateTime TimeStamp { get; set; }
        [Column(TypeName = "VARCHAR(800)")]
        public string Exception { get; set; }
        [Column(TypeName = "VARCHAR(500)")]
        public string Properties { get; set; }
    }
}
