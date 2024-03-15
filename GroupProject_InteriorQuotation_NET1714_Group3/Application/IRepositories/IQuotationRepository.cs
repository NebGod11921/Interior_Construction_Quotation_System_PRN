using Application.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IQuotationRepository : IGenericRepository<Quotation>
    {
        List<Quotation> GetQuotationsByCsID(int csID);
        Task<IEnumerable<Quotation>> SearchQuotationByQuotationName(string name);
    }
}
