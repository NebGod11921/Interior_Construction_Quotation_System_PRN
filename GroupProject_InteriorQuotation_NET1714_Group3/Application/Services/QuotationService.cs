using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class QuotationService : IQuotationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public QuotationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateQuotation(QuotationDTO quotationDTO)
        {
            try
            {
                Quotation q_mapper = _mapper.Map<Quotation>(quotationDTO);
                await _unitOfWork.QuotationRepository.AddAsync(q_mapper);
                if (await _unitOfWork.SaveChangeAsync() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public Task<bool> DeleteQuotation(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<QuotationDTO>> GetAllQuotation()
        {
            throw new NotImplementedException();
        }

        public List<QuotationDTO> GetQuotationByCsId(int csId)
        {
            try
            {
                var quotations = _unitOfWork.QuotationRepository.GetQuotationsByCsID(csId);
                var quotationDTOs = _mapper.Map<List<QuotationDTO>>(quotations);
                if (quotations == null && quotations.Count > 0) 
                {
                    return quotationDTOs;
                }
                else
                {
                    return null;
                }
            }catch {
             throw new NotImplementedException();
            }
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
