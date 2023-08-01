using System.IO;
using WebApplicationZno.Models;
using WebApplicationZno.Persistance;

namespace WebApplicationZno
{
    public class EfQuestionRepository : IQuestionRepository
    {
        private readonly EfDbContext _efDbContext;
        public EfQuestionRepository(EfDbContext efDbContext)
        {
            _efDbContext = efDbContext;
        }

        public void Create(QuestionModel question)
        {
           _efDbContext.Questions.Add(question);
           _efDbContext.SaveChanges();
        }

        public QuestionModel Delete(Guid id)
        {
            var questionForDelete = GetById(id);

            _efDbContext.Remove(questionForDelete);

            _efDbContext.SaveChanges();

            return questionForDelete;
        }

        public IEnumerable<QuestionModel> GetAllQuestions()
        {
            var questions = _efDbContext.Questions.ToList();

            return questions; 
        }

        public QuestionModel GetById(Guid id)
        {
            var question = _efDbContext.Questions.FirstOrDefault(x => x.Id == id);

            return question;
        }

        public void Update(QuestionModel question)
        {
            var questionForUpdate = GetById(question.Id);
            questionForUpdate.Title = question.Title;
            questionForUpdate.Description = question.Description;
            _efDbContext.Questions.Update(questionForUpdate);
            _efDbContext.SaveChanges();
        }
    }
}
