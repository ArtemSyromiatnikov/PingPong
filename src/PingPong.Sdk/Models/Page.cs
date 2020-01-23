using System.Collections.Generic;

namespace PingPong.Sdk.Models
{
    public class Page<T>
    {
        public int     TotalItems { get; set; }
        public List<T> Items      { get; set; }

        public Page()
        {
            TotalItems = 0;
            Items      = new List<T>();
        }

        public Page(int totalItems, List<T> items)
        {
            TotalItems = totalItems;
            Items      = items;
        }
    }
}