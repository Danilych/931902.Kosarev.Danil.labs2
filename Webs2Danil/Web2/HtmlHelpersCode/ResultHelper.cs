using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web2.HtmlHelpersCode
{
    public static class ResultHelper
    {
        public static HtmlString CreateResult(int firstNumber, int secondNumber, string operationType)
        {
            string resultString = "";
            switch (operationType)
            {
                case "plus": 
                    resultString = firstNumber.ToString() + " + " + secondNumber.ToString() + " = " + (firstNumber + secondNumber).ToString();
                    break;
                case "minus":
                    resultString = firstNumber.ToString() + " - " + secondNumber.ToString() + " = " + (firstNumber - secondNumber).ToString();
                    break;
                case "multiply":
                    resultString = firstNumber.ToString() + " * " + secondNumber.ToString() + " = " + (firstNumber * secondNumber).ToString();
                    break;
                case "divide":
                    resultString = firstNumber.ToString() + " / " + secondNumber.ToString() + " = " + ((secondNumber != 0) ? (firstNumber / secondNumber).ToString() : "Error (division on 0)");
                    break;
            }
            TagBuilder h = new TagBuilder("h2");
            h.InnerHtml.Append(resultString);

            var writer = new System.IO.StringWriter();
            h.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);

            return new HtmlString(writer.ToString());
        }
    }
}
