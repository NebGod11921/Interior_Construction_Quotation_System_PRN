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
        Task<IEnumerable<QuotationDTO>> GetAllQuotation();
        Task<QuotationDTO> GetQuotationById(int id);
        Task<IEnumerable<QuotationDTO>> GetQuotationByCsId(int csId);
        Task<bool> DeleteQuotation(int id);
        Task<bool> CreateQuotation(QuotationDTO quotationDTO);
        Task<bool> UpdateQuotation(QuotationDTO quotationDTO, int id);
        Task<IEnumerable<QuotationDTO>> SearchQuotationByName(string name);
        Task<bool> CancelQuotationStatus(int quotationId, QuotationDTO quotation);
        Task<bool> UpdatesQuotationStatus(int quotationId, QuotationDTO quotation);
        Task<bool> SuccessfulQuotationStatus(int quotationId, QuotationDTO quotation);

    }
}
