using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Junk
{
    [TestFixture]
    class When_using_NSubstitute
    {
        [Test]
        public void Should_convert_a_string_to_an_int()
        {
            var someService = Substitute.For<ISomeService>();

            someService.Convert("Foo").Returns(7);



            someService.Convert("Foo").Should().Be(7);
        }

        [Test]
        public void Should_mock_a_returned_value_if_it_is_an_interface()
        {
            var someService = Substitute.For<ISomeService>();

            someService.GetAValue().Should().NotBeNull();
        }

        [Test]
        public void Should_return_multiple_values()
        {
            var someService = Substitute.For<ISomeService>();

            someService.Convert("multi").Returns(1, 2, 3, 4, 5, 6);
            someService.Convert("multi").Should().Be(1);
            someService.Convert("multi").Should().Be(2);
            someService.Convert("multi").Should().Be(3);
            someService.Convert("multi").Should().Be(4);
            someService.Convert("multi").Should().Be(5);
            someService.Convert("multi").Should().Be(6);
        }

        [Test]
        public void Should_do_an_action_when_a_mocked_method_is_called()
        {
            var someService = Substitute.For<ISomeService>();

            string suppliedValue = null;

            someService.Convert(Arg.Do<string>(x => suppliedValue = x)).Returns(1);

            someService.Convert("wibble");

            suppliedValue.Should().Be("wibble");
        }

        [Test]
        public void Should_verify_a_method_was_called_two_times()
        {
            var someService = Substitute.For<ISomeService>();

            //someService.Convert("FooBar");
            
            //someService.Convert("FooBar");

            someService.Received(2).Convert("FooBar");
        }

        [Test]
        public void Should_verify_a_method_was_not_called()
        {
            var someService = Substitute.For<ISomeService>();

            someService.Convert("FooBar");

            someService.DidNotReceive().Convert("FooBar");
        }
    }
}