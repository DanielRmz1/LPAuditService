using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPAuditService.Models.Checking
{
    [Table("Tbl_QuestionsLog")]
    public class QuestionLog
    {
        [Key]
        public int int_IdQuestionLog { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public AnswerLog Answers { get; set; }
    }
}