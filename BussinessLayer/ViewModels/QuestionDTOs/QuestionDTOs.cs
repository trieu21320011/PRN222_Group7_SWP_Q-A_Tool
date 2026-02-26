using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.ViewModels.QuestionDTOs
{
    public class QuestionDTO
    {
        public int QuestionId { get; set; }
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public string? Excerpt { get; set; }
        public int AuthorId { get; set; }
        public string? AuthorName { get; set; }
        public int? TeamId { get; set; }
        public string? TeamName { get; set; }
        public string? Status { get; set; }
        public string? Category { get; set; }
        public string? Difficulty { get; set; }
        public int? ViewCount { get; set; }
        public int? AnswerCount { get; set; }
        public int? CommentCount { get; set; }
        public int? UpvoteCount { get; set; }
        public int? DownvoteCount { get; set; }
        public int? AcceptedAnswerId { get; set; }
        public bool? IsPinned { get; set; }
        public bool? IsClosed { get; set; }
        public string? ClosedReason { get; set; }
        public DateTime? LastActivityAt { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class CreateQuestionDTO
    {
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public string? Excerpt { get; set; }
        public int AuthorId { get; set; }
        public int? TeamId { get; set; }
        public string? Category { get; set; }
        public string? Difficulty { get; set; }
    }

    public class UpdateQuestionDTO
    {
        public int QuestionId { get; set; }
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public string? Excerpt { get; set; }
        public int? TeamId { get; set; }
        public string? Status { get; set; }
        public string? Category { get; set; }
        public string? Difficulty { get; set; }
        public bool? IsPinned { get; set; }
        public bool? IsClosed { get; set; }
        public string? ClosedReason { get; set; }
    }

    public class GetQuestionDTO
    {
        public int QuestionId { get; set; }
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public string? Excerpt { get; set; }
        public string? AuthorName { get; set; }
        public string? TeamName { get; set; }
        public string? Status { get; set; }
        public string? Category { get; set; }
        public string? Difficulty { get; set; }
        public int? ViewCount { get; set; }
        public int? AnswerCount { get; set; }
        public int? UpvoteCount { get; set; }
        public int? DownvoteCount { get; set; }
        public bool? IsPinned { get; set; }
        public bool? IsClosed { get; set; }
        public DateTime? LastActivityAt { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
