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
                    new PointOfSales() { Id = 0, Name="����� ����", Address="169907, ���� ����������, �. �������, ������� ��������, 8�" },
                    new PointOfSales() { Id = 1, Name="����� �������", Address="169907, ���� ����������, �. �������, ������� ��������, 8�" },
                    new PointOfSales() { Id = 2, Name="����� ����", Address="169907, ���� ����������, �. �������, ������� ��������, 8�" },
                    new PointOfSales() { Id = 3, Name="����� ����", Address="169912, ���� ����������, �. �������, ����� ������, 53�" },
                };
            }
        }
    }
}
