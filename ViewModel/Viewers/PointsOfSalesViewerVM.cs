using InventoryControl.Model;
using System.Collections.Generic;

namespace InventoryControl.ViewModel
{
    public class PointsOfSalesViewerVM
    {
        public List<PointOfSales> Content
        {
            get
            {
                return new List<PointOfSales>
                {
                    new PointOfSales() { Id = 0, Name="Тиман хлеб", Address="169907, Коми Республика, г. Воркута, бульвар Шерстнёва, 8Б" },
                    new PointOfSales() { Id = 1, Name="Тиман колбаса", Address="169907, Коми Республика, г. Воркута, бульвар Шерстнёва, 8Б" },
                    new PointOfSales() { Id = 2, Name="Тиман мясо", Address="169907, Коми Республика, г. Воркута, бульвар Шерстнёва, 8Б" },
                    new PointOfSales() { Id = 3, Name="Рынок мясо", Address="169912, Коми Республика, г. Воркута, улица Ленина, 53Б" },
                };
            }
        }
    }
}
