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
            var command = commands.First();
            Assert.Equal(1, commands.Count);
            Assert.Equal("Do Shipping For The Annotated Turing", command.WorkerInstruction());
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
            var command = commands.First();
            Assert.Equal(1, commands.Count);
            Assert.Equal("Active membership for John Smith", command.WorkerInstruction());
        }
    }
}