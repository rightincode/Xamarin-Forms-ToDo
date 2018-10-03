using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ToDo.ViewModels;

namespace ToDo.UnitTests
{
    [TestClass]
    public class ListTasksPageViewModelTests
    {
        [TestMethod]
        public async Task RetrieveListOfTasksFromDB_OneTaskInDB_ListWithOneTask()
        {
            var vm = new ListTasksPageViewModel(new MockToDoItemDatabase());

            var recordCount = await vm.LoadItemsAsync(true);

            Assert.AreEqual(recordCount, 1);
        }
    }
}
