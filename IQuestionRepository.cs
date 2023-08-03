using WebApplicationZno.Models;

namespace WebApplicationZno
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<QuestionModel>> GetAllQuestions();
        Task<QuestionModel> GetById(Guid id);
        Task Create(QuestionModel question);
        Task Update(QuestionModel question);
        Task<QuestionModel> Delete(Guid id);
        
    }
}
