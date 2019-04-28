using System;
using UltimateQuarryFeedback;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UltimateQuarryFeedbackTest
{
    [TestClass]
    public class FeedbackFormTest
    {
        FeedbackFormStorage formStorage = new FeedbackFormStorage();

        [TestMethod]
        public void FeedbackForm_GivenCorrectInputs_ReturnsSuccess()
        {
            // Given
            FeedbackFormStorage formStorage = new FeedbackFormStorage();

            string testFeedback = "test feedback";
            int testStarRating = 1;
            string testEmailAddress = "test@example.com";
            string testUsername = "test";
            DateTimeOffset testDate = DateTimeOffset.Now;

            FeedbackForm form = new FeedbackForm
            {
                feedback = testFeedback,
                starRating = testStarRating,
                emailAddress = testEmailAddress,
                username = testUsername,
                submittedDate = testDate
            };

            // Try 
            formStorage.addFeedbackForm(form);

            // Assert
            Assert.AreEqual(formStorage.feedbackForms[0].feedback, testFeedback);
            Assert.AreEqual(formStorage.feedbackForms[0].starRating, testStarRating);
            Assert.AreEqual(formStorage.feedbackForms[0].emailAddress, testEmailAddress);
            Assert.AreEqual(formStorage.feedbackForms[0].submittedDate, testDate);
        }

        [TestMethod]
        public void FeedbackForm_GivenBlankFeedback_ReturnSuccess()
        {
            // Given
            string testFeedback = "";
            int testStarRating = 1;
            string testEmailAddress = "test@example.com";
            string testUsername = "test";
            DateTimeOffset testDate = DateTimeOffset.Now;

            FeedbackForm form = new FeedbackForm
            {
                feedback = testFeedback,
                starRating = testStarRating,
                emailAddress = testEmailAddress,
                username = testUsername,
                submittedDate = testDate
            };

            // Try 
            formStorage.addFeedbackForm(form);

            // Assert
            Assert.AreEqual(formStorage.feedbackForms[0].feedback, testFeedback);
            Assert.AreEqual(formStorage.feedbackForms[0].starRating, testStarRating);
            Assert.AreEqual(formStorage.feedbackForms[0].emailAddress, testEmailAddress);
            Assert.AreEqual(formStorage.feedbackForms[0].submittedDate, testDate);
        }

        [TestMethod]
        public void FeedbackForm_GivenInvalidEmailAddress_ReturnException()
        {
            // Given
            string testFeedback = "test feedback";
            int testStarRating = 1;
            string testEmailAddress = "testexample.com";
            string testUsername = "test";
            DateTimeOffset testDate = DateTimeOffset.Now;

            FeedbackForm form = new FeedbackForm
            {
                feedback = testFeedback,
                starRating = testStarRating,
                emailAddress = testEmailAddress,
                username = testUsername,
                submittedDate = testDate
            };

            // Assert
            var ex = Assert.ThrowsException<ArgumentException>(() => formStorage.addFeedbackForm(form));
            Assert.AreEqual("The emailAddress field is not a valid e-mail address.", ex.Message);
        }

        [TestMethod]
        public void FeedbackForm_GivenInvalidUsername_ReturnException()
        {
            // Given
            string testFeedback = "test feedback";
            int testStarRating = 1;
            string testEmailAddress = "test@example.com";
            string testUsername = "test123";
            DateTimeOffset testDate = DateTimeOffset.Now;

            FeedbackForm form = new FeedbackForm
            {
                feedback = testFeedback,
                starRating = testStarRating,
                emailAddress = testEmailAddress,
                username = testUsername,
                submittedDate = testDate
            };

            // Assert
            Assert.ThrowsException<ArgumentException>(() => formStorage.addFeedbackForm(form));
        }

        [TestMethod]
        public void FeedbackForm_GivenTooLongUsername_ReturnException()
        {
            // Given
            string testFeedback = "test feedback";
            int testStarRating = 1;
            string testEmailAddress = "test@example.com";
            // 24 characters
            string testUsername = "testtesttesttesttesttest";
            DateTimeOffset testDate = DateTimeOffset.Now;

            FeedbackForm form = new FeedbackForm
            {
                feedback = testFeedback,
                starRating = testStarRating,
                emailAddress = testEmailAddress,
                username = testUsername,
                submittedDate = testDate
            };

            // Assert
            var ex = Assert.ThrowsException<ArgumentException>(() => formStorage.addFeedbackForm(form));
            Assert.AreEqual("The field username must be a string with a minimum length of 1 and a maximum length of 20.", ex.Message);
        }

        [TestMethod]
        public void FeedbackForm_GivenDuplicateEmail_ReturnException()
        {
            // Given
            string testAFeedback = "test A feedback";
            int testAStarRating = 1;
            string testAEmailAddress = "test@example.com";
            string testAUsername = "testA";
            DateTimeOffset testADate = DateTimeOffset.Now;

            string testBFeedback = "test B feedback";
            int testBStarRaing = 2;
            string testBUsername = "testB";
            DateTimeOffset testBDate = DateTimeOffset.Now;
            FeedbackForm formA = new FeedbackForm
            {
                feedback = testAFeedback,
                starRating = testAStarRating,
                emailAddress = testAEmailAddress,
                username = testAUsername,
                submittedDate = testADate
            };
            FeedbackForm formB = new FeedbackForm
            {
                feedback = testBFeedback,
                starRating = testBStarRaing,
                emailAddress = testAEmailAddress,
                username = testBUsername,
                submittedDate = testBDate
            };

            // create first form
            formStorage.addFeedbackForm(formA);

            // Assert
            var ex = Assert.ThrowsException<ArgumentException>(() => formStorage.addFeedbackForm(formB));
            Assert.AreEqual("email address has already been used", ex.Message);
        }

        [TestMethod]
        public void FeedbackForm_GivenInvalidStarRating_ReturnException()
        {
            // Given
            string testFeedback = "test feedback";
            int testStarRating = 6;
            string testEmailAddress = "test@example.com";
            string testUsername = "test";
            DateTimeOffset testDate = DateTimeOffset.Now;

            FeedbackForm form = new FeedbackForm
            {
                feedback = testFeedback,
                starRating = testStarRating,
                emailAddress = testEmailAddress,
                username = testUsername,
                submittedDate = testDate
            };

            // Assert
            var ex = Assert.ThrowsException<ArgumentException>(() => formStorage.addFeedbackForm(form));
            Assert.AreEqual("The field starRating must be between 1 and 5.", ex.Message);
        }
    }
}
