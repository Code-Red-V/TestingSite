using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Testing.Models;

namespace Testing.ViewModels
{
    public class QuestionViewModel
    {
        public int QuestionId { get; set; }
        public string? Text { get; set; }
        public int? TestId { get; set; }
        public virtual ICollection<Answer>? Answers { get; } = new List<Answer>();
        public virtual Test? Test { get; set; }
        public List<int> SelectedAnswers { get; set; }
    }
}
