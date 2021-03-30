using System.Collections.Generic;

namespace TodoApplication
{
    public class LimitedSizeStack<T>
    {
        private readonly LinkedList<T> items = new LinkedList<T>();
        private readonly int size;
        public LimitedSizeStack(int limit)
        {
            size = limit;
        }

        public void Push(T item)
        {
            if (size <= 0) return;
            if (items.Count == size)
            {
                items.RemoveFirst();
            }
            items.AddLast(item);
        }

        public T Pop()
        {
            var item = items.Last.Value;
            items.RemoveLast();
            return item;
        }
             
        public int Count => items.Count;
    }
}
