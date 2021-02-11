using SharedControls.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace InventoryControl.Util
{
    public static class LogicalTreeHelperExt
    {
		public static IEnumerable<T> FindChildrenElementsOfType<T>(DependencyObject current)
		{
			List <T> res = new List<T>();
			res.AddRange(LogicalTreeHelper.GetChildren(current).OfType<T>());
			foreach (var child in LogicalTreeHelper.GetChildren(current).OfType<DependencyObject>())
			{
				res.AddRange(FindChildrenElementsOfType<T>(child));
			}
			return res;
		}
	}
}
