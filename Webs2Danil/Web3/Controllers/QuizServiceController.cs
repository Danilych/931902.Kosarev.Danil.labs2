using Microsoft.AspNetCore.Mvc;

namespace Web3.Controllers
{
    using Web3.Models;
    public class QuizServiceController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.Keys.Contains("QuizModel"))
            {
                QuizModel? quizModel = HttpContext.Session.Get<QuizModel>("QuizModel");
            }
            else
            {
                QuizModel quizModel = new QuizModel();
                HttpContext.Session.Set<QuizModel>("QuizModel", quizModel);
            }

            return View();
        }

        [HttpGet]
        public IActionResult Quiz()
        {
            if (HttpContext.Session.Keys.Contains("QuizModel"))
            {
                QuizModel? quizModel = HttpContext.Session.Get<QuizModel>("QuizModel");

                if (ModelState.IsValid && quizModel != null)
                {
                    if (quizModel.currentQuestion == quizModel.answersAmount)
                    {
                        //Go to result page
                        return RedirectToAction("ShowResults");
                    }
                    else
                    {
                        ViewBag.answer = quizModel.GetNextAnswer();
                        return View();
                    }
                }
                else
                {
                    return Content("Model is not valid!");
                }
            }
            else
            {
                return Content("Return to main menu for new quiz generation!");
            }   
        }

        [HttpPost]
        public IActionResult Quiz(Web3.Models.answerModel answerM)
        {
            if (HttpContext.Session.Keys.Contains("QuizModel"))
            {
                QuizModel? quizModel = HttpContext.Session.Get<QuizModel>("QuizModel");

                if (ModelState.IsValid && quizModel != null)
                {
                    //Quiz page open
                    quizModel.CheckAnswer(answerM.answer);
                    HttpContext.Session.Set<QuizModel>("QuizModel", quizModel);

                    if(quizModel.currentQuestion == quizModel.answersAmount)
                    {
                        //Go to result page
                        return RedirectToAction("ShowResults");
                    }
                    else
                    {
                        ViewBag.answer = quizModel.GetNextAnswer();
                        return View();
                    }  
                }
                else
                {
                    return Content("Model is not valid!");
                }
            }
            else
            {
                return Content("Return to main menu for new quiz generation!");
            }
        }

        [HttpPost]
        public IActionResult ResultPost(Web3.Models.answerModel answerM)
        {
            if (HttpContext.Session.Keys.Contains("QuizModel"))
            {
                QuizModel? quizModel = HttpContext.Session.Get<QuizModel>("QuizModel");

                if (ModelState.IsValid && quizModel != null)
                {
                    //Quiz page open
                    quizModel.CheckAnswer(answerM.answer);
                    HttpContext.Session.Set<QuizModel>("QuizModel", quizModel);
                    return RedirectToAction("ShowResults");
                }
                else
                {
                    return Content("Model is not valid!");
                }
            }
            else
            {
                return Content("Return to main menu for new quiz generation!");
            }
        }

        public IActionResult ShowResults()
        {
            if (HttpContext.Session.Keys.Contains("QuizModel"))
            {
                QuizModel? quizModel = HttpContext.Session.Get<QuizModel>("QuizModel");

                if (ModelState.IsValid && quizModel != null)
                {
                    //Quiz page open
                    ViewBag.result = quizModel.ShowResult();
                    return View();
                }
                else
                {
                    return Content("Model is not valid!");
                }
            }
            else
            {
                return Content("Return to main menu for new quiz generation!");
            }
        }
    }
}
