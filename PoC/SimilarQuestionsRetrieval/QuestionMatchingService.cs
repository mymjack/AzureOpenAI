using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel.Memory;
using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimilarQuestionsRetrieval
{
    public class QuestionMatchingService : IQuestionMatchingService
    {
        readonly QuestionMatchingServiceSettings _settings;
        readonly IKernel _semanticKernel;
        readonly string _memoryCollectionName;

        public QuestionMatchingService(
            IOptions<QuestionMatchingServiceSettings> options)
        {
            _settings = options.Value;

            var builder = new KernelBuilder();

            builder.WithAzureTextEmbeddingGenerationService(
                _settings.OpenAIDeploymentName,
                _settings.OpenAIEndpoint,
                _settings.OpenAIKey);

            //builder.WithAzureTextCompletionService(model, azureEndpoint, apiKey);


            builder.WithMemoryStorage(new VolatileMemoryStore());

            _semanticKernel = builder.Build();

            _memoryCollectionName = "questions";
        }

        public async Task AddQuestion(Question question)
        {
            await _semanticKernel.Memory.SaveInformationAsync(_memoryCollectionName, id: question.Id, text: question.Text);
        }

        public Task<IEnumerable<Question>> GetSimilarQuestions(Question question)
        {
            throw new NotImplementedException();
        }

        public async Task RunDemo()
        {
            var newQuestion = "I am looking to buy a hat, can you help me?";

            var matchingQuestions = await _semanticKernel.Memory.SearchAsync(_memoryCollectionName, newQuestion).Take(3).ToListAsync();

            Console.WriteLine($"Your question: {newQuestion}");
            Console.WriteLine();
            Console.WriteLine($"Questions already asked that are similar to yours:");

            foreach (var matchingQuestion in matchingQuestions)
                Console.WriteLine($"Id: {matchingQuestion.Metadata.Id}, Question: {matchingQuestion.Metadata.Text}");

            Console.ReadLine();
        }
    }
}
