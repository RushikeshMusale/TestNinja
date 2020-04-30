using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class StackTests
    {
        [Test]
        public void Count_EmptyStack_ReturnZero()
        {
            var stack = new Stack<int>();
            Assert.That(stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void Push_WhenPassedArgument_InsertsIntoStack()
        {
            var stack = new Stack<int>();
            stack.Push(3);

            Assert.That(stack.Count, Is.EqualTo(1));
            //Assert.That(stack, Does.Contain(3));
        }


        [Test]
        public void Push_NullObjectPassed_ThrowArgumentNullException()
        {
            var stack = new Stack<string>();

            Assert.That(() => stack.Push(null), Throws.ArgumentNullException);          

        }


        [Test]
        public void Pop_WhenCalled_RemovesObjectFromStack()
        {
            var stack = new Stack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            var result = stack.Pop();

            Assert.That(result, Is.EqualTo(3));
            Assert.That(stack.Count, Is.EqualTo(2));

        }

        [Test]
        public void Pop_WhenStackIsEmpty_ThrowsInvalidOperationException()
        {
            var stack = new Stack<int>();
            Assert.That(() => stack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Peek_WhenCalled_ReturnsLastObject()
        {
            var stack = new Stack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            var result = stack.Peek();

            Assert.That(result, Is.EqualTo(3));
            Assert.That(stack.Count, Is.EqualTo(3));
        }

        

        [Test]
        public void Peek_WhenStackIsEmpty_ThrowsInvalidOperationException()
        {
            var stack = new Stack<int>();
            Assert.That(() => stack.Peek(), Throws.InvalidOperationException);
        }
    }
}
