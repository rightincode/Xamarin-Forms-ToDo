﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDo.Core.Models;

namespace ToDoPCL_Tests
{
    [TestClass]
    public class ToDoItemTests
    {
        [TestMethod]
        public void CreateNewToDoItem_NewToDoItem_IDIsAString()
        {
            var toDoItem = new ToDoItem();
            Assert.IsInstanceOfType(toDoItem.Id, typeof(System.String));

            #region better test
            //This is a better test
            Assert.IsNotNull(toDoItem.Id);
            Assert.IsTrue(toDoItem.Id.Length > 0);
            #endregion
        }
    }
}
