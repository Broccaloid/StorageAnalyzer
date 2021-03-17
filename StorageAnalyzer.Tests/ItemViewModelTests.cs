
using Xunit;
using StorageAnalyzer;
using StorageAnalyzer.Models;
using Moq;
using System.Collections.Generic;
using StorageAnalyzer.ViewModels;
using System.Threading.Tasks;

namespace StorageAnalyzer.Tests
{

    public class ItemViewModelTests
    {
        [Fact]
        public async Task SetChildrenOnExpandSuccessfully()
        {
            // Arrange
            var mock = new Mock<IExpandable>();
            mock.Setup(mock => mock.GetChildrenItems()).Returns(new List<IItem>() { new Mock<IItem>().Object});
            var itemViewModel = new ItemViewModel(mock.Object);
            // Act

            await itemViewModel.SetChildrenOnExpand();

            // Assert
            Assert.True(itemViewModel.ChildrenItems.Count > 0);
        }

        [Fact]
        public async Task SetChildrenOnExpandNotHappened()
        {
            // Arrange
            var mock = new Mock<IItem>();
            var itemViewModel = new ItemViewModel(mock.Object);
            // Act

            await itemViewModel.SetChildrenOnExpand();

            // Assert
            Assert.True(itemViewModel.ChildrenItems.Count == 0);
        }
    }
}
