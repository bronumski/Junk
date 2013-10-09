using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using NUnit.Framework;

namespace Junk
{
    [TestFixture]
    class When_asserting_with_fluent_assertions
    {
        [Test]
        public void Should_check_is_happy_using_regular_assertion()
        {
            var currentMood = new CurrentMood(false);

            Assert.IsTrue(currentMood.IAmHappy, "I want to be happy");

            Assert.That(currentMood.IAmHappy, Is.True, "I want to be happy");
        }

        [Test]
        public void Should_check_is_not_happy_using_a_custom_assertion()
        {
            var currentMood = new CurrentMood(true);

            currentMood.Should().NotBeHappy();
        }

        [Test]
        public void Should_check_is_happy_using_a_custom_assertion()
        {
            var currentMood = new CurrentMood(false);

            currentMood.Should().BeHappy("all work and no play makes Jack a dull boy");
        }

        [Test]
        public void Should_check_that_one_mode_is_the_same_as_the_other()
        {
            var happyMode = new CurrentMood(true);

            var unhappyMode = new CurrentMood(false);

            happyMode.ShouldBeEquivalentTo(unhappyMode);
        }
    }

    class CurrentMood
    {
        public CurrentMood(bool isHappy)
        {
            IAmHappy = isHappy;
        }

        public bool IAmHappy { get; private set; }
    }


    static class FluentAssertionExtensions
    {
        public static CurrentModeAssertions Should(this CurrentMood currentMood)
        {
            return new CurrentModeAssertions(currentMood);
        }
    }

    internal class CurrentModeAssertions : ObjectAssertions
    {
        public CurrentModeAssertions(CurrentMood currentMood) : base(currentMood)
        {
        }

        public AndConstraint<CurrentModeAssertions> BeHappy(string reason = "", params object[] reasonArgs)
        {
            Execute.Assertion.ForCondition(((CurrentMood) Subject).IAmHappy)
                    .BecauseOf(reason, reasonArgs)
                   .FailWith("Expected the current mood to be happy{reason}, but found that it was not.");

            return new AndConstraint<CurrentModeAssertions>(this);
        }

        public AndConstraint<CurrentModeAssertions> NotBeHappy()
        {
            Execute.Assertion.ForCondition(((CurrentMood)Subject).IAmHappy == false)
                   .FailWith("Expected current mood to not be happy{reason}, but found that it was.");

            return new AndConstraint<CurrentModeAssertions>(this);
        }
    }
}