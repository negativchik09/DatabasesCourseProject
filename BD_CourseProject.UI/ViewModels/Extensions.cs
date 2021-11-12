using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BD_CourseProject.UI.ViewModels
{
    public static class Extensions
    {
        public static void AddRange<TSource>(this ObservableCollection<TSource> collection,
            IEnumerable<TSource> appending)
        {
            foreach (var element in appending) collection.Add(element);
        }
    }
}