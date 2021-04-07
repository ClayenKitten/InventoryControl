using System.Collections.Generic;
using System.Linq;

namespace InventoryControl.Model
{
    public class OrganizationalForm
    {
        public string Abbreviation { get; private set; }
        public string Name { get; private set; }

        private OrganizationalForm(string name, string abbreviation)
        {
            Name = name;
            Abbreviation = abbreviation;
            All.Add(this);
        }

        public static HashSet<OrganizationalForm> All = new HashSet<OrganizationalForm>();
        public static HashSet<string> Abbrevations = All.Select(x => x.Abbreviation).ToHashSet();
        public static HashSet<string> Names = All.Select(x => x.Name).ToHashSet();
        // Stock
        public static OrganizationalForm Stock
            => new OrganizationalForm("Акционерное общество", "АО");
        public static OrganizationalForm StockPublic
            => new OrganizationalForm("Публичное акционерное общество", "ПАО");
        public static OrganizationalForm StockNonPublic
        // Individuals
            => new OrganizationalForm("Непубличное акционерное общество", "НАО");
        public static OrganizationalForm Individual
            => new OrganizationalForm("Индивидуальный предприниматель", "ИП");
        public static OrganizationalForm LimitedLiability
            => new OrganizationalForm("Общество с ограниченной ответственностью", "ООО");
        // Unitary
        public static OrganizationalForm Unitary
            => new OrganizationalForm("Унитарное предприятие", "УП");
        public static OrganizationalForm MunicipalUnitary
            => new OrganizationalForm("Муниципальное унитарное предприятие", "МУП");

    }
}
