using Application.Interfaces;
using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class QuotationService : IQuotationService
    {
        public Task<bool> CreateQuotation(QuotationDTO quotationDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteQuotation(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<QuotationDTO>> GetAllQuotation()
        {
            throw new NotImplementedException();
        }

        public Task<List<QuotationDTO>> GetQuotationByCsId(int csId)
        {
            throw new NotImplementedException();
        }

        public Task<QuotationDTO> GetQuotationById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateQuotation(QuotationDTO quotationDTO, int id)
        {
            throw new NotImplementedException();
        }
    }
}
