using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Core;
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

        public async Task Create(QuestionModel question)
        {
           await _efDbContext.Questions.AddAsync(question);
           await _efDbContext.SaveChangesAsync();
        }

        public async  Task<QuestionModel> Delete(Guid id)
        {
            var questionForDelete = await GetById(id);

            _efDbContext.Remove(questionForDelete);

            await _efDbContext.SaveChangesAsync();

            return questionForDelete;
        }

        public async Task<IEnumerable<QuestionModel>> GetAllQuestions()
        {
            var questions = await _efDbContext.Questions.ToListAsync();

            return questions; 
        }

        public async Task<QuestionModel> GetById(Guid id)
        {
            var question = await _efDbContext.Questions.FirstOrDefaultAsync(x => x.Id == id);
            if (question == null)
            {
                throw new ObjectNotFoundException("Entity was not found");
            }
            return question;
        }

        public async Task Update(QuestionModel question)
        {
            var questionForUpdate = await GetById(question.Id);
            questionForUpdate.Title = question.Title;
            questionForUpdate.Description = question.Description;
            _efDbContext.Questions.Update(questionForUpdate);
            await _efDbContext.SaveChangesAsync();
        }
    }
}
