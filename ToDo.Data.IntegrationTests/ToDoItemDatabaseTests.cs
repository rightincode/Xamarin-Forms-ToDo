using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Threading.Tasks;
using ToDo.Core.Models;

namespace ToDo.Data.IntegrationTests
{
    [TestClass]
    public class ToDoItemDatabaseTests
    {
        private static string databaseFolderPath;
        private static string fullPath;
        private readonly static string dbName = @"integrationTestDatabase.db3";
        private static ToDoItemDatabase testDatabase;
        private static ToDoItem testToDoItem;
        
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            databaseFolderPath = Environment.CurrentDirectory;
            fullPath = Path.Combine(databaseFolderPath, dbName);

            testDatabase = new ToDoItemDatabase(fullPath);
            testDatabase.InitializeAsync().Wait();

            testToDoItem = new ToDoItem
            {
                DueDate = new DateTime(2019, 1, 1, 4, 30, 0),
                Priority = "Low",
                TaskName = "Test ToDo(Low)"
            };

            testDatabase.SaveItemAsync(testToDoItem).Wait();
        }

        [TestMethod]
        public async Task GetToDoItems_InitializedDatabase_ListOfToDoItemsWithOneOrMoreToDoItems()
        {
            var toDoItemList = await testDatabase.GetItemsAsync(true);

            Assert.IsTrue(toDoItemList.Count > 0);
        }

        [TestMethod]
        public async Task GetToDoItem_InitializedDatabaseWithExpectedToDoItem_ListOfToDoItemsWithOneMatchingToDoItem()
        {
            var retrievedToDoItem = await testDatabase.GetItemAsync(testToDoItem.Id);

            Assert.AreEqual(testToDoItem.Id, retrievedToDoItem.Id);
            Assert.AreEqual(testToDoItem.Priority, retrievedToDoItem.Priority);
            Assert.AreEqual(testToDoItem.TaskName, retrievedToDoItem.TaskName);
            Assert.AreEqual(testToDoItem.DueDate, retrievedToDoItem.DueDate);
            Assert.AreEqual(testToDoItem.CreatedAt, retrievedToDoItem.CreatedAt);
            Assert.AreEqual(testToDoItem.UpdatedAt, retrievedToDoItem.UpdatedAt);
            Assert.AreEqual(testToDoItem.AzureVersion, retrievedToDoItem.AzureVersion);
        }

        [TestMethod]
        public async Task GetToDoItem_InitializedDatabaseWithoutExpectedToDoItem_NullList()
        {
            var retrievedToDoItem = await testDatabase.GetItemAsync(new Guid().ToString());

            Assert.IsNull(retrievedToDoItem);
        }

        [TestMethod]
        public async Task DeleteToDoItem_InitializedDatabaseWithExpectedToDoItem_TrueReturned()
        {
            var testToDoItem2 = new ToDoItem
            {
                DueDate = new DateTime(2019, 1, 1, 4, 30, 0),
                Priority = "High",
                TaskName = "Test ToDo(High)"
            };

            await testDatabase.SaveItemAsync(testToDoItem2);
            var result = await testDatabase.DeleteItemAsync(testToDoItem2);

            Assert.IsTrue(result);
        }

        [ClassCleanup]
        public static void CleanUp()
        {
            testDatabase.DeleteItemAsync(testToDoItem).Wait();
        }
        
    }
}
