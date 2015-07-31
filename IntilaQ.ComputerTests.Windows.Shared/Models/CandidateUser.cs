using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntilaQ.ComputerTests.Client.Models
{
    public class CandidateUser
    {

        public int Id { get; set; }

        public string Fullname { get; set; }

        public string Email { get; set; }

        public int Phone { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<AnswerTest> AnswerTests { get; set; }
    }
}
