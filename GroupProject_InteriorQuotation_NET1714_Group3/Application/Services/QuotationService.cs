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

		public async Task<bool> CancelQuotationStatus(int quotationId, QuotationDTO quotation)
		{
			try
            {
                var getQuotationId = await _unitOfWork.QuotationRepository.GetByIdAsync(quotationId);
                if (getQuotationId == null)
                {
                    return false;
                }
                else
                {
                    getQuotationId.Status = 0;
                     _unitOfWork.QuotationRepository.Update(getQuotationId);
                    var IsSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                    if (IsSuccess)
                    {
                        _mapper.Map<QuotationDTO>(getQuotationId);
						return true;
					}
					return false;
				}
                
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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

        public async Task<IEnumerable<QuotationDTO>> GetAllQuotation()
        {
            try
            {
                var result = await _unitOfWork.QuotationRepository.GetAllAsync();
                if (result != null)
                {
                    var mappedResult = _mapper.Map<IEnumerable<QuotationDTO>>(result);
                    return mappedResult;
                } else
                {
                    return null;
                }


            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
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

        public async Task<QuotationDTO> GetQuotationById(int id)
        {
            try
            {
                var result = await _unitOfWork.QuotationRepository.GetByIdAsync(id);
                if (result != null)
                {
                    var mapped = _mapper.Map<QuotationDTO>(result);
                    return mapped;
                }
                else
                {
                    return null;
                }


            } catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<QuotationDTO>> SearchQuotationByName(string name)
        {
            try
            {
                var result = await _unitOfWork.QuotationRepository.SearchQuotationByQuotationName(name);
                if (result == null)
                {
                    return null;
                }
                else
                {
                    var mappedResult = _mapper.Map<IEnumerable<QuotationDTO>>(result);
                    return mappedResult;
                }

            } catch (Exception e)
            {
                throw new Exception(e.Message);
            
            }
        }

		public async Task<bool> SuccessfulQuotationStatus(int quotationId, QuotationDTO quotation)
		{
			try
			{
				var getQuotationId = await _unitOfWork.QuotationRepository.GetByIdAsync(quotationId);
				if (getQuotationId == null)
				{
					return false;
				}
				else
				{
					getQuotationId.Status = 2;
					_unitOfWork.QuotationRepository.Update(getQuotationId);
					var IsSuccess = await _unitOfWork.SaveChangeAsync() > 0;
					if (IsSuccess)
					{
						_mapper.Map<QuotationDTO>(getQuotationId);
						return true;
					}
					return false;
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<bool> UpdateQuotation(QuotationDTO quotationDTO, int id)
        {
            try
            {
                var getQuotationId = await _unitOfWork.QuotationRepository.GetByIdAsync(id);
                if (getQuotationId != null)
                {
                    getQuotationId.QuotationName = quotationDTO.QuotationName;
                    getQuotationId.CreateDate = quotationDTO.CreateDate;
                    getQuotationId.TotalPrice = quotationDTO.TotalPrice;
                    getQuotationId.UnitPrice = quotationDTO.UnitPrice;
                    var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                    if (isSuccess)
                    {
                        return true;
                    }
                }
                return false;


            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

		public async Task<bool> UpdatesQuotationStatus(int quotationId, QuotationDTO quotation)
		{
			try
			{
				var getQuotationId = await _unitOfWork.QuotationRepository.GetByIdAsync(quotationId);
				if (getQuotationId == null)
				{
					return false;
				}
				else
				{
					getQuotationId.Status = 1;
                    _unitOfWork.QuotationRepository.Update(getQuotationId);
                    var IsSuccess = await _unitOfWork.SaveChangeAsync() > 0;
					if (IsSuccess)
					{
						_mapper.Map<QuotationDTO>(getQuotationId);
						return true;
					}
					return false;
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
