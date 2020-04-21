using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using DIPS.Xamarin.UI.Controls.Sheet;
using FluentAssertions;
using Xunit;

namespace DIPS.Xamarin.UI.Tests.Commands
{
    public class CancelSheetCommandTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void CanCloseSheet_FunctionSet_ShouldReturnCorrect(bool canClose)
        {
            var cancelSheetCommand = new CancelSheetCommand<string>(s => { }, s => true, s => canClose);

            cancelSheetCommand.CanCloseSheet(null).Should().Be(canClose);
        }
    }
}
