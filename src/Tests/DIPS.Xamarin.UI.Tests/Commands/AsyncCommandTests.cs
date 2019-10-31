using System;
using System.Threading.Tasks;
using DIPS.Xamarin.UI.Commands;
using FluentAssertions;
using Moq;
using Xunit;

namespace DIPS.Xamarin.UI.Tests.Commands
{
    public class AsyncCommandTests
    {
        private readonly Mock<IAsyncCommandTest<int>> m_asyncExecutableWithType = new Mock<IAsyncCommandTest<int>>();
        private readonly Mock<IAsyncCommandTest> m_asyncExecutable = new Mock<IAsyncCommandTest>();
        private readonly IAsyncCommand m_command;
        private readonly IAsyncCommand m_commandWithCanExecute;
        private readonly IAsyncCommand<int> m_commandWithT;
        private readonly IAsyncCommand<int> m_commandWithCanExecuteWithT;
        private readonly int m_input = 42;
        private Action<Exception> m_onException;

        public AsyncCommandTests()
        {
            m_command = new AsyncCommand(m_asyncExecutable.Object.Execute, e => m_onException?.Invoke(e));
            m_commandWithCanExecute = new AsyncCommand(m_asyncExecutable.Object.Execute, m_asyncExecutable.Object.CanExecute, e => m_onException?.Invoke(e));

            m_commandWithT = new AsyncCommand<int>(m_asyncExecutableWithType.Object.Execute, e => m_onException?.Invoke(e));
            m_commandWithCanExecuteWithT = new AsyncCommand<int>(m_asyncExecutableWithType.Object.Execute, m_asyncExecutableWithType.Object.CanExecute, e => m_onException?.Invoke(e));
        }

        [Fact]
        public void CanExecute_Is_Called()
        {
            m_command.CanExecute(null);

            m_asyncExecutable.Verify(e => e.CanExecute(), Times.Never);
        }

        [Fact]
        public void Execute_Dont_Start_Task_When_CanExecute_False()
        {
            m_commandWithCanExecute.Execute(null);

            m_asyncExecutable.Verify(e => e.Execute(), Times.Never);
        }

        [Fact]
        public void Execute_Starts_Task_When_CanExecute_True()
        {
            m_asyncExecutable.Setup(e => e.CanExecute()).Returns(true);

            m_commandWithCanExecute.Execute(null);

            m_asyncExecutable.Verify(e => e.Execute(), Times.Once);
        }

        [Fact]
        public void Execute_Should_Invoke_OnException_When_Task_Crashes()
        {
            var hasException = false;
            m_onException = e => hasException = true;
            m_asyncExecutable.Setup(e => e.Execute()).Throws(new Exception());

            m_command.Execute(null);

            hasException.Should().BeTrue();
        }

        [Fact]
        public void Execute_Should_Raise_CanExecuteChanged()
        {
            var invoked = false;
            m_command.CanExecuteChanged += (s, e) => invoked = true;

            m_command.RaiseCanExecuteChanged();

            invoked.Should().BeTrue();
        }



        [Fact]
        public void CanExecute_Is_Called_With_T()
        {
            m_commandWithT.CanExecute(m_input);

            m_asyncExecutableWithType.Verify(e => e.CanExecute(m_input), Times.Never);
        }

        [Fact]
        public void Execute_Dont_Start_Task_When_CanExecute_False_With_T()
        {
            m_commandWithCanExecuteWithT.Execute(m_input);

            m_asyncExecutableWithType.Verify(e => e.Execute(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void Execute_Dont_Start_Task_When_Invalid_Type_With_T()
        {
            m_commandWithCanExecuteWithT.Execute("Invalid");

            m_asyncExecutableWithType.Verify(e => e.Execute(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void Execute_Starts_Task_When_CanExecute_True_With_T()
        {
            m_asyncExecutableWithType.Setup(e => e.CanExecute(m_input)).Returns(true);

            m_commandWithCanExecuteWithT.Execute(m_input);

            m_asyncExecutableWithType.Verify(e => e.Execute(m_input), Times.Once);
        }

        [Fact]
        public void Execute_Should_Invoke_OnException_When_Task_Crashes_With_T()
        {
            var hasException = false;
            m_onException = e => hasException = true;
            m_asyncExecutableWithType.Setup(e => e.Execute(m_input)).Throws(new Exception());

            m_commandWithT.Execute(m_input);

            hasException.Should().BeTrue();
        }

        [Fact]
        public void Execute_Should_Raise_CanExecuteChanged_With_T()
        {
            var invoked = false;
            m_commandWithT.CanExecuteChanged += (s, e) => invoked = true;

            m_commandWithT.RaiseCanExecuteChanged();

            invoked.Should().BeTrue();
        }



        public interface IAsyncCommandTest
        {
            Task Execute();
            bool CanExecute();
        }

        public interface IAsyncCommandTest<T>
        {
            Task Execute(T value);
            bool CanExecute(T value);
        }
    }
}
