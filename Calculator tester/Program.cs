using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Calculator_tester
{
    class Program
    {

        public static IWebDriver driver = new ChromeDriver();

        [Obsolete]
        static void Main(string[] args)
        {
            string filePath;
            int testNumber = 1;
            //string baseURL = "http://api.mathjs.org/v4/?expr=";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Please drag and drop the CSV file into the console!", Console.ForegroundColor);
            filePath = Console.ReadLine();
            Console.WriteLine($"The path for the file is {filePath}", Console.ForegroundColor);
            //Console.ReadLine();

            CSVReader reader = new CSVReader(filePath);

            List<Expression> expressions = reader.ReadAllExpressions();

            foreach (Expression expression in expressions)
            {
                
                Console.WriteLine($"EXPECTED Test #{testNumber} : {expression.Divident} / {expression.Divisor} = {expression.ExpectedResult} ");
                testNumber++;
            }
            //Console.ForegroundColor = ConsoleColor.Cyan;

            
            //driver.Navigate().GoToUrl("https://api.mathjs.org/");
            //var field = driver.FindElement(By.Id("expr1"));
            testNumber = 1;

            foreach (Expression expression in expressions)
            {
                //Thread.Sleep(7000);
                driver.Navigate().GoToUrl("http://api.mathjs.org/v4/?expr=" + $"{expression.Divident}" + "%2F" + $"{expression.Divisor}");
                //ClickAndWaitForPageToLoad(By.Id("expr1"), 10);
                //field.Click();
                //field.Clear();
                //field.SendKeys($"{expression.Divident} / {expression.Divisor}");
                //field.SendKeys(Keys.Return);
                var answer = driver.FindElement(By.TagName("body")).Text;
                float answerParse = float.Parse(answer);
                if (answerParse == expression.ExpectedResult)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"ACTUAL RESULT Test #{testNumber} : {answerParse}", Console.ForegroundColor);
                    
                    Console.WriteLine($"Test #{testNumber} PASSED", Console.ForegroundColor);
                    testNumber++;
                    //driver.Navigate().GoToUrl("https://api.mathjs.org/");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"ACTUAL RESULT Test #{testNumber} : {answerParse}", Console.ForegroundColor);
                    
                    Console.WriteLine($"Test #{testNumber} FAILED", Console.ForegroundColor);
                    testNumber++;
                    //driver.Navigate().GoToUrl("https://api.mathjs.org/");
                }
            }

            Console.ReadLine();
            
        }

        [Obsolete]
        public static void ClickAndWaitForPageToLoad(By elementLocator, int timeout = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                var element = driver.FindElement(elementLocator);
                element.Click();
                wait.Until(ExpectedConditions.ElementIsVisible(elementLocator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + elementLocator + "' was not found in current context page.");
                throw;
            }
        }



    }
}
