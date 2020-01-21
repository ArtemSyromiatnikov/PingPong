using System.Collections;
using System.Collections.Generic;

namespace PingPong.Sdk.Models
{
    public class Page<T>
    {
        public long    TotalItems { get; set; }
        public List<T> Items      { get; set; }

        public Page()
        {
            TotalItems = 0;
            Items      = new List<T>();
        }

        public Page(long totalItems, List<T> items)
        {
            TotalItems = totalItems;
            Items      = items;
        }

        /*public int  IndexOf(T item)  => Items.IndexOf(item);
        public bool Contains(T item) => Items.Contains(item);

        public void Add(T item)                       => Items.Add(item);
        public void Insert(int index, T item)         => Items.Insert(index, item);
        public bool Remove(T item)                    => Items.Remove(item);
        public void RemoveAt(int index)               => Items.RemoveAt(index);
        public void Clear()                           => Items.Clear();
        public void CopyTo(T[] array, int arrayIndex) => Items.CopyTo(array, arrayIndex);

        public T this[int index]
        {
            get => Items[index];
            set => Items[index] = value;
        }

        public int  Count      => Items.Count;
        public bool IsReadOnly => false;

        public IEnumerator<T>   GetEnumerator() => Items.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();*/
    }
}