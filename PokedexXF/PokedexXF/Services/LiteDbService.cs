﻿using LiteDB;
using System.Collections.Generic;

namespace PokedexXF.Services
{
    public class LiteDbService<T>
    {
        protected LiteCollection<T> _collection;
        public LiteDbService()
        {
            _collection = (LiteCollection<T>)App.Database.GetCollection<T>();
        }

        public IEnumerable<T> FindAll()
        {
            return _collection.FindAll();
        }

        public T FindById(int id)
        {
            return _collection.FindById(id);
        }

        public bool UpsertItem(T item)
        {
            return _collection.Upsert(item);
        }

        public bool DeleteAll()
        {
            return _collection.DeleteAll() > 0;
        }
    }
}
