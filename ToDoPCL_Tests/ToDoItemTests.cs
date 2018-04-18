using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoPCL.Models;

namespace ToDoPCL_Tests
{
    [TestClass]
    public class ToDoItemTests
    {
        [TestMethod]
        public void TestSetToDoItemId()
        {
            var toDoItem = new ToDoItem();

            toDoItem.SetToDoItemId();

            Assert.IsInstanceOfType(toDoItem.Id, typeof(System.String));

            //Assert.IsNotNull(toDoItem.Id);
            //Assert.IsTrue(toDoItem.Id.Length > 0);            
        }
    }
}
