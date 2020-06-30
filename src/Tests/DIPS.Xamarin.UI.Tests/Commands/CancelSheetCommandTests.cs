using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using DIPS.Xamarin.UI.Controls.Sheet;
using FluentAssertions;
using Xamarin.Forms;
using Xunit;

namespace DIPS.Xamarin.UI.Tests.Commands
{
    public class CancelSheetCommandTests
    {
        [Theory, CombinatorialData]
        public async Task CanCloseSheet_WithGeneric_CanCloseSheetSet_ShouldReturnCorrect(bool canClose)
        {
            var cancelSheetCommand = new CancelSheetCommand<string>(s => { }, s => true, s => canClose);

            var canCloseSheet = await cancelSheetCommand.CanCloseSheet(null);
            canCloseSheet.Should().Be(canClose);
        }
        [Theory, CombinatorialData]
        public async Task CanCloseSheet_WithGeneric_AsyncCanCloseSheetSet_ShouldReturnCorrect(bool canClose)
        {
            var cancelSheetCommand = new CancelSheetCommand<string>(s => { }, s => true, async s =>
            {
                await Task.Delay(100);
                return canClose;
            });

            var canCloseSheet = await cancelSheetCommand.CanCloseSheet(null);
            canCloseSheet.Should().Be(canClose);
        }

        [Theory, CombinatorialData]
        public async Task CanCloseSheet_CanCloseSheetSet_ShouldReturnCorrect(bool canClose)
        {
            var cancelSheetCommand = new CancelSheetCommand(o => { }, o => true, o => canClose);

            var canCloseSheet = await cancelSheetCommand.CanCloseSheet(null);
            canCloseSheet.Should().Be(canClose);
        }

        [Theory, CombinatorialData]
        public async Task CanCloseSheet_AsyncCanCloseSheetSet_ShouldReturnCorrect(bool canClose)
        {
            var cancelSheetCommand = new CancelSheetCommand(o => { }, o => true, async o =>
            {
                await Task.Delay(100);
                return canClose;
            });

            var canCloseSheet = await cancelSheetCommand.CanCloseSheet(null);
            canCloseSheet.Should().Be(canClose);
        }

        [Fact]
        public async Task CanCloseSheet_WithGeneric_CanCloseSheetIsNull_ShouldReturnTrue()
        {
            var cancelSheetCommand = new CancelSheetCommand<string>(s => { }, s => true);

            var cancloseSheet = await cancelSheetCommand.CanCloseSheet(null);
            cancloseSheet.Should().BeTrue();
        }

        [Fact]
        public async Task CanCloseSheet_CanCloseSheetIsNull_ShouldReturnTrue()
        {
            var cancelSheetCommand = new CancelSheetCommand(s => { }, s => true);

            var canCloseSheet = await cancelSheetCommand.CanCloseSheet(null);
            canCloseSheet.Should().BeTrue();
        }
    }
}
