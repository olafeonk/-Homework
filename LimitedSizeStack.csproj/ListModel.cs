using System;
using System.Collections.Generic;

namespace TodoApplication
{
    public class ListModel<TItem>
    {
        public List<TItem> Items { get; }
        private readonly LimitedSizeStack<Tuple<TItem, int?>> limitedSizeStack;

        public ListModel(int limit)
        {
            Items = new List<TItem>();
            limitedSizeStack= new LimitedSizeStack<Tuple<TItem, int?>>(limit);
        }

        public void AddItem(TItem item)
        {
            Items.Add(item);
            limitedSizeStack.Push(new Tuple<TItem, int?> (item, null));
        }

        public void RemoveItem(int index)
        {
            limitedSizeStack.Push(new Tuple<TItem, int?>(Items[index], index));
            Items.RemoveAt(index);
        }

        public bool CanUndo()
        {
            return limitedSizeStack.Count > 0;
        }

        public void Undo()
        {
            var (value, index) = limitedSizeStack.Pop();
            if (index == null)
            {
                Items.RemoveAt(Items.Count - 1);
            }
            else
            {
                Items.Insert((int)index, value);
            }
        }
    }
}
