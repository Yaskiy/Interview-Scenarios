
using NUnit.Framework;
using System.Threading;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using TFLInterviewScenarios.Pages;
using TFLInterviewScenarios.SetUp;
using TFLInterviewScenarios.Model;
using System;
using AventStack.ExtentReports.Gherkin.Model;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace TFLInterviewScenarios.StepDefinitions
{
    [Binding]
    internal class TFLValidJouneySteps
    {
        Context _context;
        JourneyPage _journeyPage;
        TFLHomepage _tFLHomepage;
        public TFLValidJouneySteps(Context context, JourneyPage journeyPage, TFLHomepage tFLHomepage)
        {
            _context = context;
            _journeyPage = journeyPage; 
            _tFLHomepage = tFLHomepage;
        }

        //[Given(@"that the application under test is successfully loaded")]
        //public void GivenThatTheApplicationUnderTestIsSuccessfullyLoaded()
        //{
        //    _context.
        //}

        [Given(@"a user clicks on Accept only essential cookies")]
        public void GivenAUserClicksOnAcceptOnlyEssentialCookies()
        {
            Thread.Sleep(4000);
            _tFLHomepage.ClickEssentialCookies();
        }


        [When(@"a user input (.*) in the 'From' text-field on the Plan a journey widget")]
        //[When(@"a user re-input (.*) in the 'From' text-field on the Plan a journey widget")]
        public void WhenAUserInputGraysRailStationInTheFromText_FieldOnThePlanAJourneyWidget(string fromLCData)
        {
            Thread.Sleep(3000);
            _tFLHomepage.InputFromLocation(fromLCData);
        }

        

        [When(@"a user input (.*) in the 'To' text-field")]
       // [When(@"a user re-input (.*) in the 'To' text-field")]
        public void WhenAUserInputLondonBridgeInTheToText_Field(string toLCData)
        {
            _tFLHomepage.InputToLocation(toLCData);
        }

        [When(@"a user re-input (.*) in the 'From' text-field on the Plan a journey widget")]
        public void WhenAUserReInputGraysRailStationInTheFromText_FieldOnThePlanAJourneyWidget(string fromLCData)
        {
            Thread.Sleep(3000);
            _journeyPage.ClearFromTextFields();
            _tFLHomepage.InputFromLocation(fromLCData);
        }

        [When(@"a user re-input (.*) in the 'To' text-field")]
        public void WhenAUserReInputLondonBridgeInTheToText_Field(string toLCData)
        {
            Thread.Sleep(3000);
            _journeyPage.ClearToTextFields();
            _tFLHomepage.InputToLocation(toLCData);
        }

        //[When(@"a user clears both the From and To text fields")]
        //public void WhenAUserClearsBothTheFromAndToTextFields()
        //{
        //    Thread.Sleep(3000);
        //   _journeyPage.ClearTextFields();  
        //}



        [When(@"the Leaving icon is set to (.*)")]
        public void WhenTheLeavingIconIsSetToNow(string leavingData)
        {
           Assert.IsTrue( _tFLHomepage.ConfirmLeavingIcon().Equals(leavingData));
        }

        [When(@"a user clicks on plan my journey button")]
        [When(@"a user clicks on the Update Journey button")]
        public void WhenAUserClicksOnPlanMyJourney()
        {
            _tFLHomepage.ClickPlanJouneyBtn();
        }

        [Then(@"the (.*) Page is Loaded with the Journey Information")]
        [Then(@"a new (.*) Page is Loaded with a new Journey Information")]
        
        public void ThenTheJourneyPageIsLoadedWithTheJourneyInformation(string journeyPageData)
        {
           Assert.IsTrue(_journeyPage.ConfirmJourneyPage().Contains(journeyPageData));
        }

        [Then(@"the Journey Information contains the data below")]
        public void ThenTheJourneyInformationContainsTheDataBelow(Table table)
        {
            Thread.Sleep(7000);
            var tableData = table.CreateInstance<JourneyInfoModel>();
            Assert.IsTrue(_journeyPage.FromAndToLocationAppears().Contains(tableData.FromLocation));
            Assert.IsTrue(_journeyPage.FromAndToLocationAppears().Contains(tableData.ToLocation));
        }

        [Then(@"a Message (.*) should appear")]
        public void ThenAMessageSorryWeCantFindAJourneyMatchingYourCriteriaShouldAppear(string noLocationData)
        {
            Assert.AreEqual(_journeyPage.NoMatchingLocation(), noLocationData,$"Expected {noLocationData} is not equal to actual {_journeyPage.NoMatchingLocation()}");
        }

        [Then(@"the error messages below should appear")]
        public void ThenTheErrorMessagesBelowShouldAppear(Table table)
        {
            var tableData = table.CreateInstance<JourneyInfoModel>();
            Assert.AreEqual(_tFLHomepage.NullFromFieldError(), tableData.FromField);
            Assert.AreEqual(_tFLHomepage.NullToFieldError(), tableData.ToField);
        }

        //Arriving Option


        [When(@"a user clicks on the change time link")]
        public void WhenAUserClicksOnTheChangeTimeLink()
        {
            _tFLHomepage.ClickChangeTimeLnk();
        }

        [Then(@"the (.*) option must displayed")]
        public void ThenTheArrivingOptionMustDisplayed(string arrivingOptData)
        {
            Thread.Sleep(2000);
            Assert.AreEqual(_tFLHomepage.ConfirmArrivingOption(), arrivingOptData);
        }

        [When(@"a user clicks on the Arriving option")]
        public void WhenAUserClicksOnTheArrivingOption()
        {
            _tFLHomepage.ClickArrivingOption();
        }

        [When(@"user clicks on the Date dropdown and select (.*)")]
        public void WhenUserClicksOnTheDateDropdownAndSelectTomorrow(string dateData)
        {
            _tFLHomepage.ClickAndSelectDate(dateData);
        }

        [When(@"user clicks on the Time dropdown and select (.*)")]
        public void WhenUserClicksOnTheTimeDropdownAndSelect(string timeData)
        {
            _tFLHomepage.ClickAndSelectTime(timeData);
        }

        [When(@"a user clicks on the Edit Journey link")]
        public void WhenAUserClicksOnTheEditJourneyLink()
        {
            Thread.Sleep(7000);
            _journeyPage.ClickEditJourneyLnk();
        }


        // Recent Jouneys List

        [When(@"a user clicks on Recents tab")]
        public void WhenAUserClicksOnRecentsTab()
        {
            _tFLHomepage.ClickRecentTab(); 
        }

        [When(@"a user clicks on the Accept functionality cookies link")]
        public void WhenAUserClicksOnTheAcceptFunctionalityCookiesLink()
        {
            _tFLHomepage.ClickFunctionalityCookies();
        }

        [When(@"a user clicks on the accept all cookies button")]
        public void WhenAUserClicksOnTheAcceptAllCookiesButton()
        {
            Thread.Sleep(4000);
            _tFLHomepage.ClickAcceptAllCookies();
        }

        [When(@"a user clicks on the plan a journey button")]
        public void WhenAUserClicksOnThePlanAJourneyButton()
        {
            _journeyPage.ClickPlanAjourney();
        }

        [Then(@"the following journeys should appear under Recents tab")]
        public void ThenTheFollowingJourneysShouldAppearUnderRecentsTab(Table table)
        {
            var tableData = table.CreateInstance<JourneyInfoModel>();
            Assert.IsTrue(_tFLHomepage.ListOfRecentJourney().Contains(tableData.Recent1));
            Assert.IsTrue(_tFLHomepage.ListOfRecentJourney().Contains(tableData.Recent2));
        }

        [When(@"a user clicks on the New tab")]
        public void WhenAUserClicksOnTheNewTab()
        {
            _tFLHomepage.ClickNewTab();
        }



    }
}
