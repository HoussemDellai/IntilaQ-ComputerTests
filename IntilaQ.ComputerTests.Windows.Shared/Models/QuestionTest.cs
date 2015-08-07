using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntilaQ.ComputerTests.Client.Models
{
    public class QuestionTest
    {

        public int Id { get; set; }

        /// <summary>
        /// Text value must be unique.
        /// </summary>
        public string Text { get; set; }

        public List<SuggestedAnswer> SuggestedAnswers { get; set; }

        public string RightAnswer { get; set; }
    }
}
