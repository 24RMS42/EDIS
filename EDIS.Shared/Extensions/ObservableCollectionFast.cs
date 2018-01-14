using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace EDIS.Shared.Extensions
{
    public class ObservableCollectionFast<T> : ObservableCollection<T>
    {
        public ObservableCollectionFast() : base() { }
        public ObservableCollectionFast(IEnumerable<T> collection) : base(collection) { }
        public ObservableCollectionFast(List<T> collection) : base(collection) { }

        public void AddRange(IEnumerable<T> range)
        {
            var oldCount = Items.Count;

            foreach (var item in range)
                Items.Add(item);

            if (oldCount == Items.Count)
                return;

            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}