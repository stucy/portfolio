using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace client_server.Models
{
    public class CommentsModel
    {
        [Key]
        public int CommentId { get; set; }

        [Column(TypeName = "nvarchar(12)")]
        public string Username { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Comment { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string userSession { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime Date { get; set; }
    }
}
