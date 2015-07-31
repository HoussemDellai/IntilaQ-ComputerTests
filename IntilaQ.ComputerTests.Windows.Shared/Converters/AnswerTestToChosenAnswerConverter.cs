using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using IntilaQ.ComputerTests.Client.Models;

namespace IntilaQ.ComputerTests.Client.Tools
{
    public class AnswerTestToChosenAnswerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return null;
            }

            var answerTest = (AnswerTest)value;

            return answerTest.ChosenAnswer;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
