using BoDi;
using ImageAZAPIGateway.AutomationTests.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace ImageAZAPIGateway.AutomationTests.Hooks
{
    [Binding]
    public class SharedBrowserHooks
    {
        [BeforeScenario]
        public static void BeforeScenario(ObjectContainer testThreadContainer)
        {
            testThreadContainer.BaseContainer.Resolve<BrowserDriver>();
        }
    }
}
