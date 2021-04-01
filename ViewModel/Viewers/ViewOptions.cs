using System;
using System.Collections.Generic;

namespace InventoryControl.ViewModel
{
    public class ViewOptions
    {

        /// <summary>
        /// Exclude all properties with names as in key and values as in value of dict
        /// </summary>
        private HashSet<Tuple<string, object>> Filters { get; } = new HashSet<Tuple<string, object>>();
        /// <summary>
        /// Group by all properties specified in the list
        /// </summary>
        private HashSet<string> GroupingPropertyPaths { get; } = new HashSet<string>();

        /// <summary>
        /// If true, hide all GUI methods to change filtering
        /// </summary>
        public bool HideFilteringSettings { get; set; } = false;
        /// <summary>
        /// If true, hide all GUI methods to change grouping
        /// </summary>
        public bool HideGroupingSettings { get; set; } = false;
        // Limiting control over StorageViewer
        public bool HideStorageSelector { get; set; } = false;

        public void AddFilter(string propertyPath, object value)
        {
            Filters.Add(new Tuple<string, object>(propertyPath, value));
        }
        public void RemoveFilter(string propertyPath)
        {
            foreach(var filter in Filters)
            {
                if(filter.Item1 == propertyPath)
                {
                    Filters.Remove(filter);
                }
            }
        }
        public void RemoveFilter(string propertyPath, object value)
        {
            Filters.Remove(new Tuple<string, object>(propertyPath, value));
        }
        public bool DoesFilter(string propertyPath, object value)
        {
            return Filters.Contains(new Tuple<string, object>(propertyPath, value));
        }
        public bool DoesGroup(string propertyPath)
        {
            return GroupingPropertyPaths.Contains(propertyPath);
        }

        public ViewOptions()
        {
            
        }
    }
}
