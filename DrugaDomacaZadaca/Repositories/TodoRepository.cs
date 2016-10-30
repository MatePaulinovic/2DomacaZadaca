using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Interfaces;

namespace Repositories
{
    public class TodoRepository : ITodoRepository
    {
        /// <summary >
        /// Repository does not fetch todoItems from the actual database ,
        /// it uses in memory storage for this excersise .
        /// </ summary >
        private readonly List<TodoItem> _inMemoryTodoDatabase;
        public TodoRepository(List<TodoItem> initialDbState = null)
        {
            if (initialDbState != null)
            {
                _inMemoryTodoDatabase = initialDbState;
            }
            else
            {
                _inMemoryTodoDatabase = new List<TodoItem>();
            }
        }

        public void Add(TodoItem todoItem)
        {
            if (todoItem == null)
            {
                throw new ArgumentNullException();
            }
            else if(_inMemoryTodoDatabase.Where(i => i == todoItem).FirstOrDefault() != null)
            {
                throw new DuplicateTodoItemException();
            }
            else
            {
                _inMemoryTodoDatabase.Add(todoItem);
            }
        }

        public TodoItem Get(Guid todoId)
        {
            if (todoId == null)
            {
                throw new ArgumentNullException();
            }
            return _inMemoryTodoDatabase.Where(i => i.Id == todoId).FirstOrDefault();
        }

        public List<TodoItem> GetActive()
        {
            List<TodoItem> helpList = new List<TodoItem>();
            helpList.AddRange(_inMemoryTodoDatabase.Where(i => i.IsCompleted == false));
            if(helpList.Count == 0)
            {
                return null;
            }
            return helpList;
        }


        public List<TodoItem> GetAll()
        {
            if(_inMemoryTodoDatabase.Count == 0)
            {
                return null;
            }
            return  _inMemoryTodoDatabase;
        }

        public List<TodoItem> GetCompleted()
        {
            List<TodoItem> helpList = new List<TodoItem>();
            helpList.AddRange(_inMemoryTodoDatabase.Where(i => i.IsCompleted == true));
            if(helpList.Count == 0)
            {
                return null;
            }
            return helpList;
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction)
        {
            List<TodoItem> helpList = new List<TodoItem>();
            helpList.AddRange(_inMemoryTodoDatabase.Where(filterFunction));
            if(helpList.Count == 0)
            {
                return null;
            }
            return helpList;
        }

        public bool MarkAsCompleted(Guid todoId)
        {
            if(_inMemoryTodoDatabase.Where(i => i.Id == todoId).FirstOrDefault() == null)
            {
                return false;
            }
            else
            {
                _inMemoryTodoDatabase.Where(i => i.Id == todoId).FirstOrDefault().MarkAsCompleted();
                return true;
            }
        }

        public bool Remove(Guid todoId)
        {
            if( _inMemoryTodoDatabase.Where(i => i.Id == todoId).FirstOrDefault() == null)
            {
                return false;
            }
            _inMemoryTodoDatabase.Remove(_inMemoryTodoDatabase.Where(i => i.Id == todoId).FirstOrDefault());
            return true;
        }

        public void Update(TodoItem todoItem)
        {
            if (todoItem == null)
            {
                throw new ArgumentNullException();
            }
            else if (_inMemoryTodoDatabase.Where(i => i == todoItem).FirstOrDefault() == null)
            {
                _inMemoryTodoDatabase.Add(todoItem);
            }
        }
        // Shorter way to write this in C# using ?? operator :
        // _inMemoryTodoDatabase = initialDbState ?? new List < TodoItem >() ;
        // x ?? y -> if x is not null , expression returns x. Else y.
    }
}
