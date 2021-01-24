using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace client_server.Data.Models
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
    public string UserId { get; set; }

    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
    public DateTime Date { get; set; }
  }
}
