using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class QuotationDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string QuotationName { get; set; }
        public float UnitPrice { get; set; }
        public float TotalPrice { get; set; }
        public byte Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual int RoomId { get; set; }
        public virtual Room? Room { get; set; }
        //add roomTypename
        
        public int UserId { get; set; }
    }
}
