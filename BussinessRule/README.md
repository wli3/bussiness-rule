I always believe you don't need pattern matching, since method dispatch (OO polymorphism) is doing something like it already. 

I think Mark Seemann's C# Business Rules Kata is not ideal due to use C# class and "is", "as" to simulate pattern matching. In OOP sense, it violates tell don't ask. And it has Switch Statements Smell. http://wiki.c2.com/?SwitchStatementsSmell So I try to see how far I can go without using down casing.
http://blog.ploeh.dk/2018/05/17/composite-as-a-monoid-a-business-rules-example/

Weeeeeellll, the end result is funny to say at least. The good part there is no business logic duplication. And code is very idiomatic OOP. It is just visitor pattern. However, due to the method dispatching is defined at compile time. I have to copy paste 125-130 for every overload of method Handle. I try to minimize the duplication. So I get line 113 and 118 as result.

Question: is there a better way to make it more "tell don't ask" without these fancy stuff?

Lessons learned: You can use OO polymorphism. But at one point it is not powerful enough. When you have function like CompositeRule
