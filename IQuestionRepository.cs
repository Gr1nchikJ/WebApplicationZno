using WebApplicationZno.Models;

namespace WebApplicationZno
{
    public interface IQuestionRepository
    {
        IEnumerable<QuestionModel> GetAllQuestions();
        QuestionModel GetById(Guid id);
        void Create(QuestionModel question);
        void Update(QuestionModel question);
        QuestionModel Delete(Guid id);
        
    }
}
