using System;
using System.Collections.Generic;

namespace BussinessRule
{
    public interface IGood
    {
        string Name { get; }
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
        public string Name => $"{Name}'s Membership";
    }

    public interface IRule
    {
        IReadOnlyCollection<ICommand> Handle(MembershipGood good);
        IReadOnlyCollection<ICommand> Handle(Book good);
    }

    public interface ICommand
    {
        string WorkerInstruction();
    }

    public class SlipForShippingRule : IRule
    {
        public IReadOnlyCollection<ICommand> Handle(MembershipGood good)
        {
            return new ICommand[0];
        }

        public IReadOnlyCollection<ICommand> Handle(Book good)
        {
            return new[] {new SlipCommand("Shipping", good)};
        }
    }

    public class SlipCommand : ICommand
    {
        public SlipCommand(string action, IGood good)
        {
            Action = action;
            Good = good;
        }

        private string Action { get; }
        private IGood Good { get; }

        public string WorkerInstruction()
        {
            return $"Do {Action} For {Good.Name}";
        }
    }

    public class ActivateRule : IRule
    {
        public IReadOnlyCollection<ICommand> Handle(MembershipGood good)
        {
            return good != null ? new[] {new ActivateCommand(good.Membership)} : new ICommand[0];
        }

        public IReadOnlyCollection<ICommand> Handle(Book good)
        {
            return new ICommand[0];
        }
    }

    public class ActivateCommand : ICommand
    {
        public ActivateCommand(string membership)
        {
            Membership = membership;
        }

        private string Membership { get; }

        public string WorkerInstruction()
        {
            return $"Active membership for {Membership}";
        }
    }

    public class CompositeRule : IRule
    {
        private readonly IRule[] rules;

        public CompositeRule(params IRule[] rules)
        {
            this.rules = rules;
        }

        public IReadOnlyCollection<ICommand> Handle(MembershipGood good)
        {
            // Cannot find a better way. Or I need to copy it all the time
            return Handle(good, (g, rule) => rule.Handle(g));
        }

        public IReadOnlyCollection<ICommand> Handle(Book good)
        {
            return Handle(good, (g, rule) => rule.Handle(g));   
        }
        
        private IReadOnlyCollection<ICommand> Handle<T>(
            T good,
            Func<T, IRule, IReadOnlyCollection<ICommand>> rightOverloadOfHandle)
        {
            var commands = new List<ICommand>();
            foreach (var rule in rules)
            {
                commands.AddRange(rightOverloadOfHandle(good, rule));
            }
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