using Autofac;
using System;
using TechTalk.SpecFlow;
using EvernoteDesktop.PageObjectModel;

namespace EvernoteDesktop
{
    [Binding]
    public class CommonSteps
    {
        private ILifetimeScope testScope;
        private readonly TestContext context;

        private LoginPage login;

        public CommonSteps() {

        }

        public CommonSteps(TestContext context)
        {
            this.context = context;
        }

        [BeforeScenario]
        public void SetupScenarion()
        {
            if (!ScenarioContext.Current.TryGetValue("TestScope", out testScope))
            {
                var builder = new ContainerBuilder();
                builder.RegisterModule<DesktopModule>();
                var container = builder.Build();
                testScope = container.BeginLifetimeScope();
                ScenarioContext.Current["TestScope"] = testScope;
            }
        }

        [AfterScenario]
        public void Teardown() {
            Common.FinalizeTest();
        }

        [BeforeStep]
        public void BeforeStep() {
            login = testScope.Resolve<Func<TestContext, LoginPage>>()(context);
            //do for rest of page objects
        }

        [Given(@"I am on login page")]
        public void GivenIAmOnLoginPage()
        {
            ScenarioContext.Current["LoginPage"] = login;
            login.Navigate();
        }


    }
}
