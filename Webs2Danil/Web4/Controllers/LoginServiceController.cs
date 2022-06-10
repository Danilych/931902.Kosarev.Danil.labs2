using Microsoft.AspNetCore.Mvc;

namespace Web4.Controllers
{
    using Web4.Models;
    public class LoginServiceController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.Keys.Contains("userModel"))
            {
                UserModel? userModel = HttpContext.Session.Get<UserModel>("userModel");
            }
            else
            {
                UserModel userModel = new UserModel();
                HttpContext.Session.Set<UserModel>("userModel", userModel);
            }

            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            if (HttpContext.Session.Keys.Contains("userModel"))
            {
                UserModel? userModel = HttpContext.Session.Get<UserModel>("userModel");

                if (ModelState.IsValid && userModel != null)
                {
                    if(userModel.currentRegisterStage == 1)
                    {
                        return View("SignUpOne");
                        //Start registration
                    }
                    else if(userModel.currentRegisterStage == 2)
                    {
                        return View("SignUpTwo");
                        //Continue registration
                    }
                    else
                    {
                        ViewBag.firstName = userModel.firstName;
                        ViewBag.lastName = userModel.lastName;
                        ViewBag.birthday = userModel.birthday;
                        ViewBag.gender = userModel.gender;
                        ViewBag.email = userModel.email;
                        ViewBag.password = userModel.password;
                        return View("SignUpCredentials");
                        //Show credentials
                    }
                }
                else
                {
                    return Content("Model is not valid!");
                }
            }
            else
            {
                return Content("Return to index menu for new data storage generation!");
            }
        }

        [HttpPost]
        public IActionResult SignUp(FirstRegisterStageModel firstStageModel, SecondRegisterStageModel secondStageModel)
        {
            if (HttpContext.Session.Keys.Contains("userModel"))
            {
                UserModel? userModel = HttpContext.Session.Get<UserModel>("userModel");

                if ((firstStageModel.firstName != "" || secondStageModel.email != "") && userModel != null)
                {
                    if(userModel.currentRegisterStage != 3)
                    {
                        if (userModel.currentRegisterStage == 1)
                        {
                            userModel.firstName = firstStageModel.firstName;
                            userModel.lastName = firstStageModel.lastName;

                            string birthString = "";
                            birthString += firstStageModel.birthdayDay;
                            birthString += " ";
                            birthString += firstStageModel.birthdayMonth;
                            birthString += " ";
                            birthString += firstStageModel.birthdayYear;

                            userModel.birthday = birthString;
                            userModel.gender = firstStageModel.gender;
                            userModel.currentRegisterStage = 2;
                            HttpContext.Session.Set<UserModel>("userModel", userModel);
                            return View("SignUpTwo");
                        }
                        else
                        {
                            userModel.email = secondStageModel.email;
                            userModel.password = secondStageModel.password;
                            userModel.remember = secondStageModel.remember;
                            userModel.currentRegisterStage = 3;
                            HttpContext.Session.Set<UserModel>("userModel", userModel);

                            ViewBag.firstName = userModel.firstName;
                            ViewBag.lastName = userModel.lastName;
                            ViewBag.birthday = userModel.birthday;
                            ViewBag.gender = userModel.gender;
                            ViewBag.email = userModel.email;
                            ViewBag.password = userModel.password;
                            return View("SignUpCredentials");

                            //Show credentials
                        }
                        
                    }
                    else
                    {
                        ViewBag.firstName = userModel.firstName;
                        ViewBag.lastName = userModel.lastName;
                        ViewBag.birthday = userModel.birthday;
                        ViewBag.gender = userModel.gender;
                        ViewBag.email = userModel.email;
                        ViewBag.password = userModel.password;
                        return View("SignUpCredentials");
                        //Show credentials
                    }
                }
                else
                {
                    return Content("Model is not valid!");
                }
            }
            else
            {
                return Content("Return to index menu for new data storage generation!");
            }
        }

        public IActionResult Controls()
        {
            if (!HttpContext.Session.Keys.Contains("controlsModel"))
            {
                ControlsModel controlsModel = new ControlsModel();
                HttpContext.Session.Set<ControlsModel>("controlsModel", controlsModel);
            }

            return View();
        }

        [HttpGet]
        public IActionResult Reset()
        {
            if (HttpContext.Session.Keys.Contains("userModel"))
            {
                return View("ResetEmail");
            }
            else
            {
                return Content("Return to index menu! (No user presented in data)");
            }
        }

        [HttpPost]
        public IActionResult Reset(string email, string code, string password)
        {
            if(email != null)
            {
                Random rand = new Random();
                string genCode = "";

                for(int i = 0; i<10;i++)
                    genCode += rand.Next(0,10).ToString();
                
                HttpContext.Session.SetString("genCode", genCode);
                ViewBag.testCode = genCode;
                ViewBag.error = "";

                return View("ResetCode");
            }
            else if(code != null)
            {
                string genCode = "";
                genCode = HttpContext.Session.GetString("genCode");

                if(code == genCode)
                {
                    return View("ResetPassword");
                }
                else
                {
                    ViewBag.testCode = genCode;
                    ViewBag.error = "Incorrect verifying code!";
                    return View("ResetCode");
                }
            }
            else if(password != null)
            {
                if (HttpContext.Session.Keys.Contains("userModel"))
                {
                    UserModel? userModel = HttpContext.Session.Get<UserModel>("userModel");
                    if (userModel != null)
                    {
                        userModel.password = password;
                        HttpContext.Session.Set<UserModel>("userModel", userModel);

                        ViewBag.message = "Password changed successfuly!";
                        return View("Index");
                    }
                    else
                    {
                        return Content("Unknown error!");
                    }
                }
                else
                {
                    return Content("Error: user data is empty, register again!");
                }
            }
            else
            {
                return Content("Unknown error!");
            }
        }

        [HttpGet]
        public IActionResult TextBox()
        {
            if (HttpContext.Session.Keys.Contains("controlsModel"))
            {
                ControlsModel? controlsModel = HttpContext.Session.Get<ControlsModel>("controlsModel");
                //Show result page

                if (ModelState.IsValid && controlsModel != null)
                {
                    //Show result or input page
                    if(controlsModel.textBox != "")
                    {
                        ViewBag.result = controlsModel.textBox;
                        return View("TextBoxResult");
                        //Show result
                    }
                    else
                    {
                        return View("TextBox");
                        //Show input
                    }
                }
                else
                {
                    return Content("Model is not valid!");
                }
            }
            else
            {
                return Content("Return to controls menu for new data storage generation!");
            }
        }

        [HttpPost]
        public IActionResult TextBox(string textBox)
        {
            if (HttpContext.Session.Keys.Contains("controlsModel"))
            {
                ControlsModel? controlsModel = HttpContext.Session.Get<ControlsModel>("controlsModel");
                //Show result page

                if (ModelState.IsValid && controlsModel != null)
                {
                    controlsModel.textBox = textBox;
                    HttpContext.Session.Set<ControlsModel>("controlsModel", controlsModel);
                    ViewBag.result = controlsModel.textBox;

                    //Show result
                    return View("TextBoxResult");
                }
                else
                {
                    return Content("Model is not valid!");
                }
            }
            else
            {
                return Content("Return to controls menu for new data storage generation!");
            }
        }

        [HttpGet]
        public IActionResult TextArea()
        {
            if (HttpContext.Session.Keys.Contains("controlsModel"))
            {
                ControlsModel? controlsModel = HttpContext.Session.Get<ControlsModel>("controlsModel");
                //Show result page

                if (ModelState.IsValid && controlsModel != null)
                {
                    //Show result or input page
                    if (controlsModel.textArea != "")
                    {
                        ViewBag.result = controlsModel.textArea;
                        return View("TextAreaResult");
                        //Show result
                    }
                    else
                    {
                        return View("TextArea");
                        //Show input
                    }
                }
                else
                {
                    return Content("Model is not valid!");
                }
            }
            else
            {
                return Content("Return to controls menu for new data storage generation!");
            }
        }

        [HttpPost]
        public IActionResult TextArea(string textArea)
        {
            if (HttpContext.Session.Keys.Contains("controlsModel"))
            {
                ControlsModel? controlsModel = HttpContext.Session.Get<ControlsModel>("controlsModel");
                //Show result page

                if (ModelState.IsValid && controlsModel != null)
                {
                    controlsModel.textArea = textArea;
                    HttpContext.Session.Set<ControlsModel>("controlsModel", controlsModel);
                    ViewBag.result = controlsModel.textArea;

                    //Show result
                    return View("TextAreaResult");
                }
                else
                {
                    return Content("Model is not valid!");
                }
            }
            else
            {
                return Content("Return to controls menu for new data storage generation!");
            }
        }

        [HttpGet]
        public IActionResult CheckBox()
        {
            if (HttpContext.Session.Keys.Contains("controlsModel"))
            {
                ControlsModel? controlsModel = HttpContext.Session.Get<ControlsModel>("controlsModel");
                //Show result page

                if (ModelState.IsValid && controlsModel != null)
                {
                    //Show result or input page
                    if (controlsModel.checkBox != "")
                    {
                        ViewBag.result = controlsModel.checkBox;
                        return View("CheckBoxResult");
                        //Show result
                    }
                    else
                    {
                        return View("CheckBox");
                        //Show input
                    }
                }
                else
                {
                    return Content("Model is not valid!");
                }
            }
            else
            {
                return Content("Return to controls menu for new data storage generation!");
            }
        }

        [HttpPost]
        public IActionResult CheckBox(string checkBox)
        {
            if (HttpContext.Session.Keys.Contains("controlsModel"))
            {
                ControlsModel? controlsModel = HttpContext.Session.Get<ControlsModel>("controlsModel");
                //Show result page

                if (ModelState.IsValid && controlsModel != null)
                {
                    controlsModel.checkBox = checkBox;
                    HttpContext.Session.Set<ControlsModel>("controlsModel", controlsModel);
                    ViewBag.result = controlsModel.checkBox;

                    //Show result
                    return View("CheckBoxResult");
                }
                else
                {
                    return Content("Model is not valid!");
                }
            }
            else
            {
                return Content("Return to controls menu for new data storage generation!");
            }
        }

        [HttpGet]
        public IActionResult Radio()
        {
            if (HttpContext.Session.Keys.Contains("controlsModel"))
            {
                ControlsModel? controlsModel = HttpContext.Session.Get<ControlsModel>("controlsModel");
                //Show result page

                if (ModelState.IsValid && controlsModel != null)
                {
                    //Show result or input page
                    if (controlsModel.radio != "")
                    {
                        ViewBag.result = controlsModel.radio;
                        return View("RadioResult");
                        //Show result
                    }
                    else
                    {
                        return View("Radio");
                        //Show input
                    }
                }
                else
                {
                    return Content("Model is not valid!");
                }
            }
            else
            {
                return Content("Return to controls menu for new data storage generation!");
            }
        }

        [HttpPost]
        public IActionResult Radio(string radio)
        {
            if (HttpContext.Session.Keys.Contains("controlsModel"))
            {
                ControlsModel? controlsModel = HttpContext.Session.Get<ControlsModel>("controlsModel");
                //Show result page

                if (ModelState.IsValid && controlsModel != null)
                {
                    controlsModel.radio = radio;
                    HttpContext.Session.Set<ControlsModel>("controlsModel", controlsModel);
                    ViewBag.result = controlsModel.radio;

                    //Show result
                    return View("RadioResult");
                }
                else
                {
                    return Content("Model is not valid!");
                }
            }
            else
            {
                return Content("Return to controls menu for new data storage generation!");
            }
        }

        [HttpGet]
        public IActionResult DropDownList()
        {
            if (HttpContext.Session.Keys.Contains("controlsModel"))
            {
                ControlsModel? controlsModel = HttpContext.Session.Get<ControlsModel>("controlsModel");
                //Show result page

                if (ModelState.IsValid && controlsModel != null)
                {
                    //Show result or input page
                    if (controlsModel.dropDownList != "")
                    {
                        ViewBag.result = controlsModel.dropDownList;
                        return View("DropDownListResult");
                        //Show result
                    }
                    else
                    {
                        return View("DropDownList");
                        //Show input
                    }
                }
                else
                {
                    return Content("Model is not valid!");
                }
            }
            else
            {
                return Content("Return to controls menu for new data storage generation!");
            }
        }

        [HttpPost]
        public IActionResult DropDownList(string dropDownList)
        {
            if (HttpContext.Session.Keys.Contains("controlsModel"))
            {
                ControlsModel? controlsModel = HttpContext.Session.Get<ControlsModel>("controlsModel");
                //Show result page

                if (ModelState.IsValid && controlsModel != null)
                {
                    controlsModel.dropDownList = dropDownList;
                    HttpContext.Session.Set<ControlsModel>("controlsModel", controlsModel);
                    ViewBag.result = controlsModel.dropDownList;

                    //Show result
                    return View("DropDownListResult");
                }
                else
                {
                    return Content("Model is not valid!");
                }
            }
            else
            {
                return Content("Return to controls menu for new data storage generation!");
            }
        }

        [HttpGet]
        public IActionResult ListBox()
        {
            if (HttpContext.Session.Keys.Contains("controlsModel"))
            {
                ControlsModel? controlsModel = HttpContext.Session.Get<ControlsModel>("controlsModel");
                //Show result page

                if (ModelState.IsValid && controlsModel != null)
                {
                    //Show result or input page
                    if (controlsModel.listBox != "")
                    {
                        ViewBag.result = controlsModel.listBox;
                        return View("ListBoxResult");
                        //Show result
                    }
                    else
                    {
                        return View("ListBox");
                        //Show input
                    }
                }
                else
                {
                    return Content("Model is not valid!");
                }
            }
            else
            {
                return Content("Return to controls menu for new data storage generation!");
            }
        }

        [HttpPost]
        public IActionResult ListBox(string listBox)
        {
            if (HttpContext.Session.Keys.Contains("controlsModel"))
            {
                ControlsModel? controlsModel = HttpContext.Session.Get<ControlsModel>("controlsModel");
                //Show result page

                if (ModelState.IsValid && controlsModel != null)
                {
                    controlsModel.listBox = listBox;
                    HttpContext.Session.Set<ControlsModel>("controlsModel", controlsModel);
                    ViewBag.result = controlsModel.listBox;

                    //Show result
                    return View("ListBoxResult");
                }
                else
                {
                    return Content("Model is not valid!");
                }
            }
            else
            {
                return Content("Return to controls menu for new data storage generation!");
            }
        }

        //[HttpGet]
        //public IActionResult Quiz()
        //{
        //    if (HttpContext.Session.Keys.Contains("QuizModel"))
        //    {
        //        QuizModel? quizModel = HttpContext.Session.Get<QuizModel>("QuizModel");

        //        if (ModelState.IsValid && quizModel != null)
        //        {
        //            if (quizModel.currentQuestion == quizModel.answersAmount)
        //            {
        //                //Go to result page
        //                return RedirectToAction("ShowResults");
        //            }
        //            else
        //            {
        //                ViewBag.answer = quizModel.GetNextAnswer();
        //                return View();
        //            }
        //        }
        //        else
        //        {
        //            return Content("Model is not valid!");
        //        }
        //    }
        //    else
        //    {
        //        return Content("Return to main menu for new quiz generation!");
        //    }   
        //}

        //[HttpPost]
        //public IActionResult Quiz(Web4.Models.answerModel answerM)
        //{
        //    if (HttpContext.Session.Keys.Contains("QuizModel"))
        //    {
        //        QuizModel? quizModel = HttpContext.Session.Get<QuizModel>("QuizModel");

        //        if (ModelState.IsValid && quizModel != null)
        //        {
        //            //Quiz page open
        //            quizModel.CheckAnswer(answerM.answer);
        //            HttpContext.Session.Set<QuizModel>("QuizModel", quizModel);

        //            if(quizModel.currentQuestion == quizModel.answersAmount)
        //            {
        //                //Go to result page
        //                return RedirectToAction("ShowResults");
        //            }
        //            else
        //            {
        //                ViewBag.answer = quizModel.GetNextAnswer();
        //                return View();
        //            }  
        //        }
        //        else
        //        {
        //            return Content("Model is not valid!");
        //        }
        //    }
        //    else
        //    {
        //        return Content("Return to main menu for new quiz generation!");
        //    }
        //}

        //[HttpPost]
        //public IActionResult ResultPost(Web4.Models.answerModel answerM)
        //{
        //    if (HttpContext.Session.Keys.Contains("QuizModel"))
        //    {
        //        QuizModel? quizModel = HttpContext.Session.Get<QuizModel>("QuizModel");

        //        if (ModelState.IsValid && quizModel != null)
        //        {
        //            //Quiz page open
        //            quizModel.CheckAnswer(answerM.answer);
        //            HttpContext.Session.Set<QuizModel>("QuizModel", quizModel);
        //            return RedirectToAction("ShowResults");
        //        }
        //        else
        //        {
        //            return Content("Model is not valid!");
        //        }
        //    }
        //    else
        //    {
        //        return Content("Return to main menu for new quiz generation!");
        //    }
        //}

        //public IActionResult ShowResults()
        //{
        //    if (HttpContext.Session.Keys.Contains("QuizModel"))
        //    {
        //        QuizModel? quizModel = HttpContext.Session.Get<QuizModel>("QuizModel");

        //        if (ModelState.IsValid && quizModel != null)
        //        {
        //            //Quiz page open
        //            ViewBag.result = quizModel.ShowResult();
        //            return View();
        //        }
        //        else
        //        {
        //            return Content("Model is not valid!");
        //        }
        //    }
        //    else
        //    {
        //        return Content("Return to main menu for new quiz generation!");
        //    }
        //}
    }
}
