using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.ViewModels.AnswerDTOs
{
    public class AnswerDTO
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public string? QuestionTitle { get; set; }
        public int AuthorId { get; set; }
        public string? AuthorName { get; set; }
        public string Body { get; set; } = null!;
        public bool? IsAccepted { get; set; }
        public bool? IsMentorAnswer { get; set; }
        public int? CommentCount { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class CreateAnswerDTO
    {
        public int QuestionId { get; set; }
        public int AuthorId { get; set; }
        public string Body { get; set; } = null!;
        public bool? IsMentorAnswer { get; set; }
    }

    public class UpdateAnswerDTO
    {
        public int AnswerId { get; set; }
        public string Body { get; set; } = null!;
        public bool? IsAccepted { get; set; }
        public bool? IsMentorAnswer { get; set; }
    }

    public class GetAnswerDTO
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public string? QuestionTitle { get; set; }
        public string? AuthorName { get; set; }
        public string Body { get; set; } = null!;
        public bool? IsAccepted { get; set; }
        public bool? IsMentorAnswer { get; set; }
        public int? CommentCount { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
