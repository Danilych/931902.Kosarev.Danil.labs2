namespace Web3.Models
{
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    public class QuizModel
    {
        public int[] numberOne { get; set; }
        public int[] numberTwo { get; set; }
        public int answersAmount { get; set; }
        public int[] operType { get; set; }
        public int[] correctAnswers { get; set; }
        public int correctAnswersAmount { get; set; }

        public int[] userAnswers { get; set; }
        public int currentQuestion { get; set; }

        public QuizModel()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());

            answersAmount = rand.Next(2, 10);

            numberOne = new int[answersAmount];
            numberTwo = new int[answersAmount];
            operType = new int[answersAmount];
            correctAnswers = new int[answersAmount];
            userAnswers = new int[answersAmount];

            correctAnswersAmount = 0;
            currentQuestion = 0;

            for (int i = 0; i < answersAmount; i++)
            {
                numberOne[i] = rand.Next(1, 100);
                numberTwo[i] = rand.Next(1, 100);
                operType[i] = rand.Next(1, 5);

                int result = 0;

                switch (operType[i])
                {
                    case 1: result = numberOne[i] + numberTwo[i]; break;
                    case 2: result = numberOne[i] - numberTwo[i]; break;
                    case 3: result = numberOne[i] * numberTwo[i]; break;
                    case 4: result = numberOne[i] / numberTwo[i]; break;
                }

                correctAnswers[i] = result;
                userAnswers[i] = 0;
            }
        }

        public void CheckAnswer(int answer)
        {
            if (answer == correctAnswers[currentQuestion]) correctAnswersAmount++;
            userAnswers[currentQuestion] = answer;
            currentQuestion++;
        }

        public HtmlString ShowResult()
        {
            TagBuilder div = new TagBuilder("div");

            TagBuilder h = new TagBuilder("h2");
            h.InnerHtml.Append("Right answers ");
            h.InnerHtml.Append(correctAnswersAmount.ToString());
            h.InnerHtml.Append(" out ");
            h.InnerHtml.Append(answersAmount.ToString());

            div.InnerHtml.AppendHtml(h);

            TagBuilder list = new TagBuilder("ul");

            int questionNumber = 1;
            foreach (int i in userAnswers)
            {
                //li
                TagBuilder row = new TagBuilder("li");
                row.InnerHtml.Append(questionNumber.ToString());
                row.InnerHtml.Append(". ");
                row.InnerHtml.Append(numberOne[questionNumber-1].ToString());

                string operString = "";

                switch (operType[questionNumber-1])
                {
                    case 1: operString = " + "; break;
                    case 2: operString = " - "; break;
                    case 3: operString = " * "; break;
                    case 4: operString = " / "; break;
                }

                row.InnerHtml.Append(operString);
                row.InnerHtml.Append(numberTwo[questionNumber - 1].ToString());
                row.InnerHtml.Append(" = ");
                row.InnerHtml.Append(userAnswers[questionNumber-1].ToString());

                list.InnerHtml.AppendHtml(row);
                questionNumber++;
            }

            div.InnerHtml.AppendHtml(list);

            var writer = new System.IO.StringWriter();
            div.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);

            return new HtmlString(writer.ToString());
        }

        public HtmlString GetNextAnswer()
        {
            if (currentQuestion <= answersAmount - 1)
            {
                string resultString = "";
                switch (operType[currentQuestion])
                {
                    case 1:
                        resultString = numberOne[currentQuestion].ToString() + " + " + numberTwo[currentQuestion].ToString() + " = ";// + (numberOne[currentQuestion] + numberTwo[currentQuestion]).ToString();
                        break;
                    case 2:
                        resultString = numberOne[currentQuestion].ToString() + " - " + numberTwo[currentQuestion].ToString() + " = ";
                        break;
                    case 3:
                        resultString = numberOne[currentQuestion].ToString() + " * " + numberTwo[currentQuestion].ToString() + " = ";
                        break;
                    case 4:
                        resultString = numberOne[currentQuestion].ToString() + " / " + numberTwo[currentQuestion].ToString() + " = ";
                        break;
                }
                TagBuilder div = new TagBuilder("div");

                TagBuilder h = new TagBuilder("h1");
                h.InnerHtml.Append("Quiz");

                div.InnerHtml.AppendHtml(h);

                TagBuilder question = new TagBuilder("h3");
                question.InnerHtml.Append(resultString);
                div.InnerHtml.AppendHtml(question);

                var writer = new System.IO.StringWriter();
                div.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);

                return new HtmlString(writer.ToString());
            }
            else
            {
                return ShowResult();
                
            }
        }
    }
}
