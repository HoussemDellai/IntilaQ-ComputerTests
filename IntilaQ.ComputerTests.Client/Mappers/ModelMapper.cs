using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntilaQ.ComputerTests.Client.Models;

namespace IntilaQ.ComputerTests.Client.Mappers
{
    public class ModelMapper
    {
        private AnswerTest MapTo(QuestionTest questionTest)
        {
            return new AnswerTest
            {
                Text = questionTest.Text,
                SuggestedAnswers = questionTest.SuggestedAnswers,
            };
        }

        public List<AnswerTest> MapTo(List<QuestionTest> questionTests)
        {
            return questionTests.Select(MapTo).ToList();

            //List<AnswerTest> answersTests = new List<AnswerTest>();

            //foreach (var questionTest in questionTests)
            //{
            //    answersTests.Add(MapTo(questionTest));    
            //}

            //return answersTests;
        }
    }
}
