using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimilarQuestionsRetrieval
{
    public interface IQuestionMatchingService
    {
        Task RunDemo();
        Task AddQuestion(Question question);
        Task<IEnumerable<Question>> GetSimilarQuestions(Question question);
    }
}
