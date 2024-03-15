using Application.IRepositories;
using Domain.Entities;
using Infrustructure;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class QuotationRepository : GenericRepository<Quotation>, IQuotationRepository
    {
        private readonly AppDbContext _appDbContext;
        public QuotationRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

		

		public List<Quotation> GetQuotationsByCsID(int csID)
        {
            try
            {
                var quotations = _appDbContext.Quotations.Where(x => x.User.Id == csID).ToList();
                if (quotations.Count > 0)
                {
                    return quotations;
                }
                else
                {
                    return new List<Quotation>();
                }

            }catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Quotation>> SearchQuotationByQuotationName(string name)
        {
            try
            {
                var quotation = await _appDbContext.Quotations.Where(x => x.QuotationName.Contains(name)).ToListAsync();
                if (quotation.Count > 0)
                {
                    return quotation;
                }
                else
                {
                    return null;
                }



            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
