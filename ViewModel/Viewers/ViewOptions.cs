using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryControl.ViewModel
{
    public class ViewOptions
    {
        private HashSet<Tuple<string, object>> Filters { get; } = new HashSet<Tuple<string, object>>();
        private HashSet<string> Groupings { get; } = new HashSet<string>();

        // Limiting control over filtering and grouping from GUI
        private bool hideFilteringSettings = false;
        public bool HideFilteringSettings 
        {
            get => hideFilteringSettings;
            set
            {
                hideFilteringSettings = value;
                Update();
            }
        }
        private bool hideGroupingSettings = false;
        public bool HideGroupingSettings
        {
            get => hideGroupingSettings;
            set
            {
                hideGroupingSettings = value;
                Update();
            }
        }
        // Limiting control over StorageViewer
        [Obsolete]
        public bool HideStorageSelector { get; set; } = false;

        // Filtering methods
        public void AddFilter(string propertyPath, object value)
        {
            if (Filters.Add(new Tuple<string, object>(propertyPath, value)))
            {
                Update();
            }
        }
        public void RemoveFilter(string propertyPath)
        {
            bool isUpdated = false;
            foreach(var filter in Filters)
            {
                if(filter.Item1 == propertyPath)
                {
                    isUpdated |= Filters.Remove(filter);
                }
            }
        }
        public void RemoveFilter(string propertyPath, object value)
        {
            if (Filters.Remove(new Tuple<string, object>(propertyPath, value)))
            {
                Update();
            }
        }
        public void SetFilter(bool enabled, string propertyPath, object value)
        {
            if (enabled)
            {
                AddFilter(propertyPath, value);
            }
            else
            {
                RemoveFilter(propertyPath, value);
            }
        }
        public bool DoesFilter(string propertyPath, object value)
        {
            return Filters.Contains(new Tuple<string, object>(propertyPath, value));
        }
        public bool Filter(object obj)
        {
            return Filters.All
            (
                filterTuple =>
                {
                    var prop = obj.GetType().GetProperty(filterTuple.Item1);
                    if (prop != null)
                    {
                        return prop.GetValue(obj).Equals(filterTuple.Item2);
                    }
                    else
                    {
                        return false;
                    }
                }
            );
        }
        // Grouping methods
        public void AddGroup(string propertyPath)
        {
            if (Groupings.Add(propertyPath))
            {
                Update();
            }
        }
        public void RemoveGroup(string propertyPath)
        {
            if (Groupings.Remove(propertyPath))
            {
                Update();
            }            
        }
        public void SetGroup(bool enabled, string propertyPath)
        {
            if (enabled)
            {
                AddGroup(propertyPath);
            }
            else
            {
                RemoveGroup(propertyPath);
            }
        }
        public bool DoesGroup(string propertyPath)
        {
            return Groupings.Contains(propertyPath);
        }

        public ViewOptions()
        {
        }

        private void Update()
        {
            ViewOptionsChanged?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler ViewOptionsChanged;
    }
}
