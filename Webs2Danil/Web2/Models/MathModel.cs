namespace Web2.Models
{
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    public class MathModel
    {
        public int NumberOne { get; set; }
        public int NumberTwo { get; set; }
        public string OperType { get; set; } = "";

        public HtmlString CreateResult()
        {
            string resultString = "";
            switch (OperType)
            {
                case "plus":
                    resultString = NumberOne.ToString() + " + " + NumberTwo.ToString() + " = " + (NumberOne + NumberTwo).ToString();
                    break;
                case "minus":
                    resultString = NumberOne.ToString() + " - " + NumberTwo.ToString() + " = " + (NumberOne - NumberTwo).ToString();
                    break;
                case "multiply":
                    resultString = NumberOne.ToString() + " * " + NumberTwo.ToString() + " = " + (NumberOne * NumberTwo).ToString();
                    break;
                case "divide":
                    resultString = NumberOne.ToString() + " / " + NumberTwo.ToString() + " = " + ((NumberTwo != 0) ? (NumberOne / NumberTwo).ToString() : "Error (division on 0)");
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
