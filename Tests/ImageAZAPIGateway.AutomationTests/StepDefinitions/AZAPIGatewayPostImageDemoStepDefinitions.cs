using ImageAZAPIGateway.AutomationTests.Drivers;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Text.Json;
using System.Threading;
using TechTalk.SpecFlow;

namespace ImageAZAPIGateway.AutomationTests.StepDefinitions
{
    [Binding]
    public sealed class AZAPIGatewayPostImageDemoStepDefinitions
    {
        private readonly BrowserDriver _browserDriver;
        private readonly string _randomKey;

        public AZAPIGatewayPostImageDemoStepDefinitions(BrowserDriver browserDriver)
        {
            _browserDriver = browserDriver;
            _randomKey = GenerateRandomString();
        }

        [Given("Go to API gateway portal")]
        public void GivenGoToAPIGatewayPortal()
        {
            //TODO: Move to configs
            var portalUrl = "https://latestimageviewer-apigateway.developer.azure-api.net/api-details#api=imageazapigateway-server&operation=post-public-images";
            _browserDriver.Current.Navigate().GoToUrl(portalUrl);
        }

        [Given("Open popup to try API post image")]
        public void GivenOpenPopupToTryAPIPostImage()
        {
            var btnTryItXpath = "//button[contains(text(),'Try it')]";
            // Wait for page load
            WebDriverWait waitForElement = new WebDriverWait(_browserDriver.Current, TimeSpan.FromSeconds(300));
            waitForElement.Until(ExpectedConditions.ElementIsVisible(By.XPath(btnTryItXpath)));
            var btnTryItElement = _browserDriver.Current.FindElement(By.XPath(btnTryItXpath));
            btnTryItElement.Click();
            Thread.Sleep(3000);
        }

        [Given("Input the API body (.*) and (.*)")]
        public void GivenInputTheApiBody(string imageUrl, string description)
        {
            var areaBodyXpath = "//textarea[@aria-label='Request body']";
            var areaBodyElement = _browserDriver.Current.FindElement(By.XPath(areaBodyXpath));
            areaBodyElement.Clear();
            // Because api ignore post the same url, so we need to add random key
            var jsonObject = new
            {
                imageUrl = imageUrl.Replace("$", _randomKey),
                description = description.Replace("$", _randomKey)
            };

            string jsonString = JsonSerializer.Serialize(jsonObject);
            areaBodyElement.SendKeys(jsonString);
            Thread.Sleep(1000);
        }

        [When("Click Send button")]
        public void WhenClickSendButton()
        {
            var btnSendXpath = "//button[normalize-space(text())='Send']";
            var btnSendElement = _browserDriver.Current.FindElement(By.XPath(btnSendXpath));
            btnSendElement.Click();
            Thread.Sleep(3000);
        }

        [Then("Verify the API response status is 200")]
        public void ThenVerifyTheAPIResponseStatusIs200()
        {
            try
            {
                var labelSuccessXpath = "//span[@data-code='200' and text()='200']";
                WebDriverWait waitForElement = new WebDriverWait(_browserDriver.Current, TimeSpan.FromSeconds(300));
                waitForElement.Until(ExpectedConditions.ElementIsVisible(By.XPath(labelSuccessXpath)));
            }
            catch (Exception ex)
            {
                throw new Exception($"Call api to post image was failed, Exception: {ex.Message}");
            }
        }

        [Then("Verify image information (.*) and (.*) display on latestimageviewer page")]
        public void ThenVerifyTheImageInfoDisplayOnAngularPage(string imageUrl, string description)
        {
            try
            {
                var latestImageViewerUrl = "https://latestimageviewer-hefeeef7bpa0fcar.southeastasia-01.azurewebsites.net/";
                _browserDriver.Current.Navigate().GoToUrl(latestImageViewerUrl);
                var pageLoadedXpath = "//h1[text()='Latest Image Viewer']";
                WebDriverWait waitForElement = new WebDriverWait(_browserDriver.Current, TimeSpan.FromSeconds(300));
                waitForElement.Until(ExpectedConditions.ElementIsVisible(By.XPath(pageLoadedXpath)));

                imageUrl = imageUrl.Replace("$", _randomKey);
                description = description.Replace("$", _randomKey);

                string imgXpath = $"//img[@src='{imageUrl}']";
                string descXpath = $"//p[contains(text(), 'Description: {description}')]";

                waitForElement.Until(ExpectedConditions.ElementIsVisible(By.XPath(imgXpath)));
                waitForElement.Until(ExpectedConditions.ElementIsVisible(By.XPath(descXpath)));
            }
            catch (Exception ex)
            {
                throw new Exception($"The image information display incorrect on latestimageviewer page, Exception: {ex.Message}");
            }
        }

        public static string GenerateRandomString(int length = 5)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            char[] result = new char[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = chars[random.Next(chars.Length)];
            }

            return new string(result);
        }
    }
}
