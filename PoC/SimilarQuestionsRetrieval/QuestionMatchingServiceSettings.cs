using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimilarQuestionsRetrieval
{
    public class QuestionMatchingServiceSettings
    {
        public string OpenAIKey { get; set; }
        public string OpenAIDeploymentName { get; set; }
        public string OpenAIEndpoint { get; set; }
    }
}
