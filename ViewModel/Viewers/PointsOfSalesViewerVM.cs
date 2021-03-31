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
                    new PointOfSales(id: 0, name: "Тиман хлеб", address: "169907, Коми Республика, г. Воркута, бульвар Шерстнёва, 8Б"),
                    new PointOfSales(id: 1, name: "Тиман колбаса", address: "169907, Коми Республика, г. Воркута, бульвар Шерстнёва, 8Б"),
                    new PointOfSales(id: 2, name: "Тиман мясо", address: "169907, Коми Республика, г. Воркута, бульвар Шерстнёва, 8Б"),
                    new PointOfSales(id: 3, name: "Рынок мясо", address: "169912, Коми Республика, г. Воркута, улица Ленина, 53Б")
                };
            }
        }
    }
}
