using OpenQA.Selenium;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using SpecFlowFrameWork.Utility;

namespace SpecFlowFrameWork.Utility
{
    public class ExtentReport
    {
        public static ExtentReports _extentReports;

        public static ThreadLocal<ExtentReports> Extent = new ThreadLocal<ExtentReports>();
        
        public static String dir = AppDomain.CurrentDomain.BaseDirectory;
        public static String testResultPath = dir.Replace("bin\\Debug\\net6.0", "TestResults");
        public static String screenshotsPath = dir.Replace("bin\\Debug\\net6.0", "Screenshots");

        public static void ExtentReportInit()
        {
            if (Directory.Exists(screenshotsPath))
            {
                Directory.Delete(screenshotsPath, true);
            }

            String WorkingDirectory = Environment.CurrentDirectory;
            String ProjectDirectory = Directory.GetParent(WorkingDirectory).Parent.Parent.FullName;

            Directory.CreateDirectory(screenshotsPath);
            var htmlReporter = new ExtentHtmlReporter(testResultPath);

            htmlReporter.Config.ReportName = "ExtentReport";
            htmlReporter.Config.DocumentTitle = "ExtentReport";
            htmlReporter.Config.CSS = "css-string";
            htmlReporter.Config.Theme = Theme.Standard;

            htmlReporter.Start();

            _extentReports = new ExtentReports();
            Extent.Value = _extentReports;
            _extentReports.AttachReporter(htmlReporter);
            _extentReports.AddSystemInfo("Application", "LoginPageApplication");
            _extentReports.AddSystemInfo("Browser", $"Chrome");
            _extentReports.AddSystemInfo("Environment", $"Test_Server");
            _extentReports.AddSystemInfo("OS", "Windows");
            _extentReports.AddSystemInfo("SDET", $"Ram Kadam");
            _extentReports.AttachReporter(htmlReporter);







        }

        public static void ExtentReportTearDown()
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            string reportName = "ExtentReport_" + timestamp;
            // Instance.Flush();
            _extentReports.Flush();
            // Rename the report file
            string defaultReportFile = Path.Combine(testResultPath, "index.html");
            string renamedReportFile = Path.Combine(testResultPath, reportName + ".html");

            File.Move(defaultReportFile, renamedReportFile);
        }

        public MediaEntityModelProvider addScreenshot(IWebDriver driver, string screenshotName)
        {
            ITakesScreenshot takesScreenshot = (ITakesScreenshot)driver;
            var screenshot = takesScreenshot.GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenshotName).Build();
        }

    }
}
