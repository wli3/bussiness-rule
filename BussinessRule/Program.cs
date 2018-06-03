using System.Collections.Generic;

namespace BussinessRule
{
    public interface IGood
    {
    }

    public class Book : IGood
    {
        public Book(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }

    public class MembershipGood : IGood
    {
        public MembershipGood(string membership)
        {
            Membership = membership;
        }

        public string Membership { get; }
    }

    public interface IRule
    {
        IReadOnlyCollection<ICommand> Handle(IGood good);
    }

    public interface ICommand
    {
    }


    public class SlipForShippingRule : IRule
    {
        public IReadOnlyCollection<ICommand> Handle(IGood good)
        {
            if (good is Book)
                return new[] {new SlipCommand("Shipping", good)};

            return new ICommand[0];
        }
    }

    public class SlipCommand : ICommand
    {
        public SlipCommand(string action, IGood good)
        {
            Action = action;
            Good = good;
        }

        public string Action { get; }
        public IGood Good { get; }
    }


    public class ActivateRule : IRule
    {
        public IReadOnlyCollection<ICommand> Handle(IGood good)
        {
            var m = good as MembershipGood;
            if (m != null)
                return new[] {new ActivateCommand(m.Membership)};

            return new ICommand[0];
        }
    }

    public class ActivateCommand : ICommand
    {
        public ActivateCommand(string membership)
        {
            Membership = membership;
        }

        public string Membership { get; }
    }


    public class CompositeRule : IRule
    {
        private readonly IRule[] rules;

        public CompositeRule(params IRule[] rules)
        {
            this.rules = rules;
        }

        public IReadOnlyCollection<ICommand> Handle(IGood good)
        {
            var commands = new List<ICommand>();
            foreach (var rule in rules)
                commands.AddRange(rule.Handle(good));
            return commands;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
        }
    }
}