using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntilaQ.ComputerTests.Client.Models
{
    public class AnswerTest 
    {

        public int Id { get; set; }

        public int QuestionTestId { get; set; }

        public string Text { get; set; }

        public List<string> SuggestedAnswers { get; set; }

        public string ChosenAnswer { get; set; }
    }
}
