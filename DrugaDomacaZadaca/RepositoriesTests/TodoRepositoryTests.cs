using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Interfaces;

namespace Repositories.Tests
{
    [TestClass()]
    public class TodoRepositoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingNullToDatabaseThrowsException()
        {
            ITodoRepository repository = new TodoRepository();
            repository.Add(null);
        }
        [TestMethod]
        public void AddingItemWillAddToDatabase()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            repository.Add(todoItem);
            Assert.AreEqual(1, repository.GetAll().Count);
            Assert.IsTrue(repository.Get(todoItem.Id) != null);
        }
        [TestMethod]
        [ExpectedException(typeof(DuplicateTodoItemException))]
        public void AddingExistingItemWillThrowException()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            repository.Add(todoItem);
            repository.Add(todoItem);
        }
        //method Get(Guid todoId)
        [TestMethod]
        public void SearchForExistingWillReturnItem()
        {
            ITodoRepository repository = new TodoRepository();
            var FirstTodoItem = new TodoItem("Chore 1");
            var SecondTodoItem = new TodoItem("Chore 2");
            repository.Add(FirstTodoItem);
            repository.Add(SecondTodoItem);
            Assert.AreEqual(FirstTodoItem, repository.Get(FirstTodoItem.Id));
        }
        [TestMethod]
        public void SearchForNonexistingWillReturnNull()
        {
            ITodoRepository repository = new TodoRepository();
            var FirstTodoItem = new TodoItem("Chore 1");
            var SecondTodoItem = new TodoItem("Chore 2");
            var ThirdTodoItem = new TodoItem("Chore 3");
            repository.Add(FirstTodoItem);
            repository.Add(SecondTodoItem);
            Assert.AreEqual(null, repository.Get(ThirdTodoItem.Id));
        }

        //method GetActive()
        [TestMethod]
        public void SearchForActiveWillReturnActiveItems()
        {
            ITodoRepository repository = new TodoRepository();
            var FirstTodoItem = new TodoItem("Chore 1");
            var SecondTodoItem = new TodoItem("Chore 2");
            FirstTodoItem.MarkAsCompleted();
            repository.Add(FirstTodoItem);
            repository.Add(SecondTodoItem);
            Assert.AreEqual(1, repository.GetActive().Count);
            ////////////////
        }
        [TestMethod]
        public void SearchForActiveWhenThereAreNonWillReturnNull()
        {
            ITodoRepository repository = new TodoRepository();
            var FirstTodoItem = new TodoItem("Chore 1");
            var SecondTodoItem = new TodoItem("Chore 2");
            FirstTodoItem.MarkAsCompleted();
            SecondTodoItem.MarkAsCompleted();
            repository.Add(FirstTodoItem);
            repository.Add(SecondTodoItem);
            Assert.AreEqual(null, repository.GetActive());
            ///////////////
        }

        //method GetAll()
        [TestMethod]
        public void EmptyRepositoryReturnsNull()
        {
            ITodoRepository repository = new TodoRepository();
            Assert.AreEqual(null, repository.GetAll());
        }

        [TestMethod]
        public void GetAllReturnsAllItems()
        {
            ITodoRepository repository = new TodoRepository();
            var FirstTodoItem = new TodoItem("Chore 1");
            var SecondTodoItem = new TodoItem("Chore 2");
            repository.Add(FirstTodoItem);
            repository.Add(SecondTodoItem);
            ITodoRepository secondRepository = new TodoRepository();
            secondRepository.Add(FirstTodoItem);
            secondRepository.Add(SecondTodoItem);
            Assert.AreEqual(2, secondRepository.GetAll().Count);
            ////////////////////////////
        }

        //method GetCompleted()
        [TestMethod]
        public void SearchForCompletedWillReturnCompletedItems()
        {
            ITodoRepository repository = new TodoRepository();
            var FirstTodoItem = new TodoItem("Chore 1");
            var SecondTodoItem = new TodoItem("Chore 2");
            FirstTodoItem.MarkAsCompleted();
            repository.Add(FirstTodoItem);
            repository.Add(SecondTodoItem);
            Assert.AreEqual(1, repository.GetCompleted().Count);
            ////////////////
        }
        [TestMethod]
        public void SearchForCompletedWhenThereAreNonWillReturnNull()
        {
            ITodoRepository repository = new TodoRepository();
            var FirstTodoItem = new TodoItem("Chore 1");
            var SecondTodoItem = new TodoItem("Chore 2");
            repository.Add(FirstTodoItem);
            repository.Add(SecondTodoItem);
            Assert.AreEqual(null, repository.GetCompleted());
            ////////////////
        }

        //method GetFiltered
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FilteringWithNullThrowsException()
        {
            ITodoRepository repository = new TodoRepository();
            var FirstTodoItem = new TodoItem("Chore 1");
            repository.Add(FirstTodoItem);
            repository.GetFiltered(null);
        }
        [TestMethod]
        public void FilteringEveryItemReturnsNull()
        {
            ITodoRepository repository = new TodoRepository();
            var FirstTodoItem = new TodoItem("Chore 1");
            var SecondTodoItem = new TodoItem("Chore 2");
            repository.Add(FirstTodoItem);
            repository.Add(SecondTodoItem);
            Assert.AreEqual(null, repository.GetFiltered(i => i != i));
        }
        [TestMethod]
        public void FilteringNoItemsReturnsAll()
        {
            ITodoRepository repository = new TodoRepository();
            var FirstTodoItem = new TodoItem("Chore 1");
            var SecondTodoItem = new TodoItem("Chore 2");
            repository.Add(FirstTodoItem);
            repository.Add(SecondTodoItem);
            Assert.AreEqual(2, repository.GetFiltered(i => i == i).Count);
        }

        //method MarkAsCompleted(Guid todoId)
        [TestMethod]
        public void PassingANonexistingIdReturnsFalse()
        {
            ITodoRepository repository = new TodoRepository();
            var FirstTodoItem = new TodoItem("Chore 1");
            var SecondTodoItem = new TodoItem("Chore 2");
            repository.Add(FirstTodoItem);
            Assert.AreEqual(false, repository.MarkAsCompleted(SecondTodoItem.Id));
        }
        [TestMethod]
        public void PassingAnExistingIdReturnsTrue()
        {
            ITodoRepository repository = new TodoRepository();
            var FirstTodoItem = new TodoItem("Chore 1");
            var SecondTodoItem = new TodoItem("Chore 2");
            repository.Add(FirstTodoItem);
            repository.Add(SecondTodoItem);
            Assert.AreEqual(true, repository.MarkAsCompleted(SecondTodoItem.Id));
        }

        //method Remove(Guid todoId)
        [TestMethod]
        public void RemovingANonexistingIdReturnsFalse()
        {
            ITodoRepository repository = new TodoRepository();
            var FirstTodoItem = new TodoItem("Chore 1");
            var SecondTodoItem = new TodoItem("Chore 2");
            repository.Add(FirstTodoItem);
            Assert.AreEqual(false, repository.Remove(SecondTodoItem.Id));
        }
        [TestMethod]
        public void RemovingAnExistingIdReturnsTrue()
        {
            ITodoRepository repository = new TodoRepository();
            var FirstTodoItem = new TodoItem("Chore 1");
            var SecondTodoItem = new TodoItem("Chore 2");
            repository.Add(FirstTodoItem);
            repository.Add(SecondTodoItem);
            Assert.AreEqual(true, repository.Remove(SecondTodoItem.Id));
        }

        //method Update(TodoItem todoItem)
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdatingWithNullThrowsException()
        {
            ITodoRepository repository = new TodoRepository();
            var FirstTodoItem = new TodoItem("Chore 1");
            var SecondTodoItem = new TodoItem("Chore 2");
            repository.Add(FirstTodoItem);
            repository.Update(null);
        }
        [TestMethod]
        public void UpdatingWithNonexistingItemAddsItem()
        {
            ITodoRepository repository = new TodoRepository();
            var FirstTodoItem = new TodoItem("Chore 1");
            var SecondTodoItem = new TodoItem("Chore 2");
            repository.Add(FirstTodoItem);
            repository.Add(SecondTodoItem);
            ITodoRepository secondRepository = new TodoRepository();
            secondRepository.Add(FirstTodoItem);
            secondRepository.Update(SecondTodoItem);
            Assert.AreEqual(repository.GetAll().Count, secondRepository.GetAll().Count);
        }
    }
}
