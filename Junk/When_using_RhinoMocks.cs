using System.Collections;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Rhino.Mocks;

namespace Junk
{
    [TestFixture]
    class When_using_RhinoMocks
    {
        [Test]
        public void Should_convert_a_string_to_an_int()
        {
            var someService = MockRepository.GenerateStub<ISomeService>();

            someService.Stub(x => x.Convert("Foo")).Return(7);

            someService.Convert("Foo").Should().Be(7);
        }

        [Test]
        public void Should_mock_a_returned_value_if_it_is_an_interface()
        {
            var someService = MockRepository.GenerateStub<ISomeService>();

            someService.Stub(x => x.GetAValue()).Return(MockRepository.GenerateMock<IEnumerable>());

            someService.GetAValue().Should().NotBeNull();
        }

        [Test]
        public void Should_return_multiple_values()
        {
            var someService = MockRepository.GenerateStub<ISomeService>();

            someService.Stub(x => x.Convert("multi")).Return(1).Repeat.Once();
            someService.Stub(x => x.Convert("multi")).Return(2).Repeat.Once();
            someService.Stub(x => x.Convert("multi")).Return(3).Repeat.Once();
            someService.Stub(x => x.Convert("multi")).Return(4).Repeat.Once();
            someService.Stub(x => x.Convert("multi")).Return(5).Repeat.Once();
            someService.Stub(x => x.Convert("multi")).Return(6).Repeat.Once();

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
            var someService = MockRepository.GenerateMock<ISomeService>();
            
            string suppliedValue = null;
            
            someService.Expect(x => x.Convert(Arg<string>.Is.Anything))
                .WhenCalled(invocation =>
                    {
                        suppliedValue = (string) invocation.Arguments.First();
                    })
                .Return(1);

            someService.Convert("wibble");

            suppliedValue.Should().Be("wibble");
        }

        [Test]
        public void Should_verify_a_method_was_called_two_times()
        {
            var someService = MockRepository.GenerateMock<ISomeService>();

            someService
                .Expect(x => x.Convert("FooBar"))
                .Repeat.Twice()
                .Return(1);

            //someService.Convert("FooBar");
            
            //someService.Convert("FooBar");

            someService.VerifyAllExpectations();
        }

        [Test]
        public void Should_verify_a_method_was_not_called()
        {
            var someService = MockRepository.GenerateMock<ISomeService>();

            someService.Expect(x => x.Convert("FooBar"))
                       .Repeat.Never();

            someService.Convert("FooBar");

            someService.VerifyAllExpectations();
        }
    }
}
