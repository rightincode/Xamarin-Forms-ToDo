using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using ToDoPCL.Models;
using ToDoPCL.ViewModels;

namespace ToDoPCL_Tests
{
    [TestClass]
    public class CreatePageViewModelTests
    {
        [TestMethod]
        public async Task CreatePageViewModelAddAndLoadToDoItem()
        {
            var testToDoItem = new ToDoItem {
                TaskName = "Testing_AddToDoItem",
                DueDate = new DateTime(1970, 04, 27, 4, 30, 0),
                Priority = "Low"
            };
            testToDoItem.SetToDoItemId();

            var testDataStore = new MockToDoItemDatabase();

            var vm = new CreatePageViewModel(testToDoItem, testDataStore);
            await vm.AddToDoItem();
            await vm.LoadToDoListItem(testToDoItem.Id);

            Assert.AreEqual(testToDoItem.Id, vm.Id);
            Assert.AreEqual(testToDoItem.DueDate, vm.DueDate);
            Assert.AreEqual(testToDoItem.Priority, vm.Priority);
        }

        [TestMethod]
        public async Task CreatePageViewModelDeleteToDoItem()
        {
            var testToDoItem = new ToDoItem
            {
                TaskName = "Testing_DeleteToDoItem",
                DueDate = new DateTime(1970, 04, 27, 4, 30, 0),
                Priority = "Low"
            };
            testToDoItem.SetToDoItemId();

            var testDataStore = new MockToDoItemDatabase();

            var vm = new CreatePageViewModel(testToDoItem, testDataStore);
            await vm.AddToDoItem();
            var result = await vm.DeleteToDoItem();

            Assert.AreEqual(result, true);
        }
    }
}
