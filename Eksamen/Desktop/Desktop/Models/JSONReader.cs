using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Desktop.Models
{
    class QuestionDto
    {
        public string Question { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }
        public string Correct { get; set; }
    }

    class JSONReader
    {
        public IList<IQuestion> Deserialize()
        {
            IList<IQuestion> questionList = new List<IQuestion>();

            try {
                string json = File.ReadAllText(Directory.GetCurrentDirectory() + "\\questions.json");
                List<QuestionDto> questions = JsonConvert.DeserializeObject<List<QuestionDto>>(json);

                foreach (var q in questions)
                {
                    var tmp = new Question(q);
                    questionList.Add(tmp);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("There was a problem reading the JSON file\nError message: " + e.Message);
            }
            return questionList;
        }
    }
}
