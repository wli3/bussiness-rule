using System.Linq;
using BussinessRule;
using Xunit;

namespace FeedCheckerTest
{
    public class GivenFeedChecker
    {
        [Fact]
        public void BooksTest()
        {
            var rule =
                new CompositeRule(
                    new SlipForShippingRule(),
                    new ActivateRule());

            var book = new Book("The Annotated Turing");
            var commands = rule.Handle(book);
            var command = commands.First() as SlipCommand;
            Assert.Equal(book, command.Good);
            Assert.Equal("Shipping", command.Action);
        }

        [Fact]
        public void MembershipTests()
        {
            var rule =
                new CompositeRule(
                    new SlipForShippingRule(),
                    new ActivateRule());

            var membership = new MembershipGood("John Smith");
            var commands = rule.Handle(membership);
            var command = commands.First() as ActivateCommand;
            Assert.Equal("John Smith", command.Membership);
        }
    }
}