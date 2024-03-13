using Application.ViewModels;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IQuotationService
    {
        Task<List<QuotationDTO>> GetAllQuotation();
        Task<QuotationDTO> GetQuotationById(int id);
        Task<List<QuotationDTO>> GetQuotationByCsId(int csId);
        Task<bool> DeleteQuotation(int id);
        Task<bool> CreateQuotation(QuotationDTO quotationDTO);
        Task<bool> UpdateQuotation(QuotationDTO quotationDTO, int id);
    }
}
