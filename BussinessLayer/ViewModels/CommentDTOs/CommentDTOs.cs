using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.ViewModels.CommentDTOs
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public string Body { get; set; } = null!;
        public int AuthorId { get; set; }
        public string? AuthorName { get; set; }
        public int? QuestionId { get; set; }
        public int? AnswerId { get; set; }
        public int? ParentCommentId { get; set; }
        public bool? IsEdited { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class CreateCommentDTO
    {
        public string Body { get; set; } = null!;
        public int AuthorId { get; set; }
        public int? QuestionId { get; set; }
        public int? AnswerId { get; set; }
        public int? ParentCommentId { get; set; }
    }

    public class UpdateCommentDTO
    {
        public int CommentId { get; set; }
        public string Body { get; set; } = null!;
    }

    public class GetCommentDTO
    {
        public int CommentId { get; set; }
        public string Body { get; set; } = null!;
        public string? AuthorName { get; set; }
        public int? QuestionId { get; set; }
        public int? AnswerId { get; set; }
        public int? ParentCommentId { get; set; }
        public bool? IsEdited { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
