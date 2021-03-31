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
                    new PointOfSales(id: 0, name: "����� ����", address: "169907, ���� ����������, �. �������, ������� ��������, 8�"),
                    new PointOfSales(id: 1, name: "����� �������", address: "169907, ���� ����������, �. �������, ������� ��������, 8�"),
                    new PointOfSales(id: 2, name: "����� ����", address: "169907, ���� ����������, �. �������, ������� ��������, 8�"),
                    new PointOfSales(id: 3, name: "����� ����", address: "169912, ���� ����������, �. �������, ����� ������, 53�")
                };
            }
        }
    }
}
