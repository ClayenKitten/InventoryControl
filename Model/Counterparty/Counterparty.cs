using InventoryControl.ORM;
using InventoryControl.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Model
{
    public class Counterparty : IEntity, INamed, ITransferSpot
    {
        public static PropertyEntityTable<Counterparty> Table { get; } = new PropertyEntityTable<Counterparty>
        (
            new List<Counterparty>()
            {
                new Counterparty()
                {
                    Id = 0,
                    Name = "Управляемая организация",
                    Role = -1,
                }
            },
            // Main
            new PropertyColumn<Counterparty, string>("FullName"),
            new PropertyColumn<Counterparty, string>("Name"),
            new PropertyColumn<Counterparty, string>("LegalAddress"),
            new PropertyColumn<Counterparty, string>("ActualAddress"),
            // Contacts
            new PropertyColumn<Counterparty, string>("Phone"),
            new PropertyColumn<Counterparty, string>("Fax"),
            new PropertyColumn<Counterparty, string>("Email"),
            new PropertyColumn<Counterparty, string>("Website"),
            // Legal
            new PropertyColumn<Counterparty, string>("MSRN"),
            new PropertyColumn<Counterparty, string>("TIN"),
            new PropertyColumn<Counterparty, string>("CRR"),
            // Banking
            new PropertyColumn<Counterparty, string>("BIC"),
            new PropertyColumn<Counterparty, string>("PaymentAccount"),
            new PropertyColumn<Counterparty, string>("CorrespondentAccount"),
            new PropertyColumn<Counterparty, int>("Role", Constraint.NotNull | Constraint.DefaultValue(0))
        );

        public long Id { get; set; }
        
        public string FullName { get; set; }
        public string Name { get; set; }
        public string LegalAddress { get; set; }
        public string ActualAddress { get; set; }

        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }

        public string MSRN { get; set; } // ОГРН
        public string TIN { get; set; } // ИНН
        public string CRR { get; set; } // КПП
        public string BIC { get; set; } // БИК
        public string PaymentAccount { get; set; } // Р/С
        public string CorrespondentAccount { get; set; } // К/С

        public int Role { get; set; }

        public Counterparty() {}
    }
}
