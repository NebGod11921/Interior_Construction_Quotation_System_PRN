using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public string? FullName { get; set; }
        public string? EmailAddress { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public string? TelephoneNumber { get; set; }
        public string? Gender { get; set; }  
        public byte? Status { get; set; }
        public string? RoleName {  get; set; }
        
        public virtual IEnumerable<Quotation> Quotations { get; set; }
    }
}
