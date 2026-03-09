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
        public int? CoreId { get; set; }
        public string? CoreName { get; set; }
        public int? TopicId { get; set; }
        public string? TopicName { get; set; }
        public string? SemesterCode { get; set; }
        public int? AssignedInstructorId { get; set; }
        public string? AssignedInstructorName { get; set; }
        public string? Status { get; set; }
        public string? Category { get; set; }
        public string? Difficulty { get; set; }
        public int? ViewCount { get; set; }
        public int? AnswerCount { get; set; }
        public int? CommentCount { get; set; }
        public int? AcceptedAnswerId { get; set; }
        public bool? IsPinned { get; set; }
        public bool? IsPrivate { get; set; }
        public bool? IsClosed { get; set; }
        public string? ClosedReason { get; set; }
        public int? ClosedById { get; set; }
        public DateTime? ClosedAt { get; set; }
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
        public int? CoreId { get; set; }
        public int? TopicId { get; set; }
        public string? Category { get; set; }
        public string? Difficulty { get; set; }
        public bool? IsPrivate { get; set; }
    }

    public class UpdateQuestionDTO
    {
        public int QuestionId { get; set; }
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public string? Excerpt { get; set; }
        public int? TeamId { get; set; }
        public int? CoreId { get; set; }
        public int? TopicId { get; set; }
        public int? AssignedInstructorId { get; set; }
        public string? Status { get; set; }
        public string? Category { get; set; }
        public string? Difficulty { get; set; }
        public bool? IsPinned { get; set; }
        public bool? IsPrivate { get; set; }
        public bool? IsClosed { get; set; }
        public string? ClosedReason { get; set; }
        public int? ClosedById { get; set; }
    }

    public class GetQuestionDTO
    {
        public int QuestionId { get; set; }
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public string? Excerpt { get; set; }
        public string? AuthorName { get; set; }
        public string? TeamName { get; set; }
        public string? CoreName { get; set; }
        public string? TopicName { get; set; }
        public string? SemesterCode { get; set; }
        public string? AssignedInstructorName { get; set; }
        public string? Status { get; set; }
        public string? Category { get; set; }
        public string? Difficulty { get; set; }
        public int? ViewCount { get; set; }
        public int? AnswerCount { get; set; }
        public int? CommentCount { get; set; }
        public bool? IsPinned { get; set; }
        public bool? IsPrivate { get; set; }
        public bool? IsClosed { get; set; }
        public DateTime? LastActivityAt { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class QuestionDetailDTO : GetQuestionDTO
    {
        public int AuthorId { get; set; }
        public int? AcceptedAnswerId { get; set; }
        public string? ClosedReason { get; set; }
        public int? ClosedById { get; set; }
        public DateTime? ClosedAt { get; set; }
        public List<QuestionAnswerDTO> Answers { get; set; } = new List<QuestionAnswerDTO>();
        public List<QuestionCommentDTO> Comments { get; set; } = new List<QuestionCommentDTO>();
        public bool CanEdit { get; set; } // Can current user edit?
    }

    public class QuestionAnswerDTO
    {
        public int AnswerId { get; set; }
        public int AuthorId { get; set; }
        public string? AuthorName { get; set; }
        public string Body { get; set; } = null!;
        public bool? IsAccepted { get; set; }
        public bool? IsMentorAnswer { get; set; }
        public int? CommentCount { get; set; }
        public DateTime? CreatedAt { get; set; }
        public List<QuestionCommentDTO> Comments { get; set; } = new List<QuestionCommentDTO>();
    }

    public class QuestionCommentDTO
    {
        public int CommentId { get; set; }
        public int AuthorId { get; set; }
        public string? AuthorName { get; set; }
        public string Body { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
    }

    public class MarkAsUnderstoodDTO
    {
        public int QuestionId { get; set; }
    }
}
