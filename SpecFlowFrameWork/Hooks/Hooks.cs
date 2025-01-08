using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SpecFlowFrameWork.Utility;
using SpecFlowProject1.Base;
using SpecFlowProject1.Utility;
using TechTalk.SpecFlow;

namespace SpecFlowFrameWork.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        private IWebDriver _driver;
        private readonly IObjectContainer _container;
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;

        [ThreadStatic]
        public static ExtentTest _feature;
        [ThreadStatic]
        public static ExtentTest _scenario;
        private static ThreadLocal<IWebDriver> Driver = new ThreadLocal<IWebDriver>();
        //public static ThreadLocal<ExtentReports> Extent = new ThreadLocal<ExtentReports>();
        public static ThreadLocal<ExtentTest> ScenarioTest = new ThreadLocal<ExtentTest>();
        public static DateTime Time = DateTime.Now;
        public static string Filename = "Screenshot_" + Time.ToString("h_mm_ss") + ".png";
        public ExtentReport er = new ExtentReport();
        private static JSonReader _jsonReader = new JSonReader();
        public Hooks(IObjectContainer container, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _container = container;
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
           
        }


        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Console.WriteLine("Initializing global resources (Extent Reports).");
            ExtentReport.ExtentReportInit();

        }

        [BeforeFeature("@BeforeFeature")]
        public static void BeforeFeature(FeatureContext featureContext)
        {
           
          //  _jsonReader.ReadJsonFile();
            Console.WriteLine("Json file initialize");
            Console.WriteLine("Running Before Feature: " + featureContext.FeatureInfo.Title);
            _feature = ExtentReport._extentReports.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }

        [AfterFeature]
        public static void AfterFeatureWithTag()
        {
            Console.WriteLine("Json file close");
        }

        [BeforeScenario(Order = 1)]
        public void InitializeWebDriver()
        {
            Console.WriteLine("Initializing WebDriver.");
           
           var browserName = _jsonReader.TestDataURL("Browser");
            BaseClass baseClass = new BaseClass();

            _driver = baseClass.BrowserLaunch(_driver, browserName);
            Driver.Value = _driver;
            _container.RegisterInstanceAs<IWebDriver>(_driver);
        }


        [BeforeScenario]
        public void BeforeScenarioWithTag(ScenarioContext scenarioContext)
        {
            Console.WriteLine("Maximizing the browser window before scenario.");
            _driver.Manage().Window.Maximize();
            Console.WriteLine("Running Before Scenario: " + scenarioContext.ScenarioInfo.Title);
            _scenario = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
            scenarioContext["scenario"] = _scenario;
            var Url = _jsonReader.TestDataURL("Url");
            _driver.Navigate().GoToUrl(Url);

        }


        [AfterScenario]
        public void AfterScenario()
        {
            Console.WriteLine("Closing browser after scenario.");
            _driver.Quit();
        }


        [AfterTestRun]
        public static void AfterTestRun()
        {
            Console.WriteLine("Finalizing resources after test run (flushing reports).");
            ExtentReport.ExtentReportTearDown();
        }

        [BeforeStep("@BeforeStep")]
        public void beforestep(ScenarioContext scenarioContext)
        {
            Console.WriteLine("Running before step....");
            string stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            string stepName = scenarioContext.StepContext.StepInfo.Text;

            switch (stepType)
            {
                case "Given":
                    scenarioContext["scenario"] = _scenario.CreateNode<Given>(stepName);
                    break;
                case "When":
                    scenarioContext["scenario"] = _scenario.CreateNode<When>(stepName);
                    break;
                case "Then":
                    scenarioContext["scenario"] = _scenario.CreateNode<Then>(stepName);
                    break;
            }
        }


        [AfterStep]
        public void AfterStep(ScenarioContext scenarioContext)
        {
            string stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            string stepName = scenarioContext.StepContext.StepInfo.Text;
            IWebDriver _driver = _container.Resolve<IWebDriver>();
            //string screenshotPath = ExtentReport.AddScreenshot(_driver, scenarioContext.ScenarioInfo.Title);

            if (scenarioContext.TestError == null)
            {
                if (stepType == "Given")
                {
                    _scenario.CreateNode<Given>(stepName).Pass("Passed Successfully", er.addScreenshot(_driver, Filename));
                }
                else if (stepType == "When")
                {
                    _scenario.CreateNode<When>(stepName).Pass("Passed Successfully", er.addScreenshot(_driver, Filename));
                }
                else if (stepType == "Then")
                {
                    _scenario.CreateNode<Then>(stepName).Pass("Passed Successfully", er.addScreenshot(_driver, Filename));
                }
                else if (stepType == "And")
                {
                    _scenario.CreateNode<And>(stepName).Pass("Passed Successfully", er.addScreenshot(_driver, Filename));
                }
            }
            else
            {

                if (scenarioContext.TestError != null)
                {
                    _scenario.CreateNode(stepName).Fail(scenarioContext.TestError.Message, er.addScreenshot(_driver, Filename));
                    //ExtentReport.AddScreenshotToReport(screenshotPath);
                    if (stepType == "Given")
                    {
                        _scenario.CreateNode<Given>(stepName).Fail(scenarioContext.TestError.Message);
                    }
                    else if (stepType == "When")
                    {
                        _scenario.CreateNode<When>(stepName).Fail(scenarioContext.TestError.Message);
                    }
                    else if (stepType == "Then")
                    {
                       _scenario.CreateNode<Then>(stepName).Fail(scenarioContext.TestError.Message);
                    }
                    else if (stepType == "And")
                    {
                        _scenario.CreateNode<And>(stepName).Fail(scenarioContext.TestError.Message);
                    }
                }
            }
        }
    }
}
