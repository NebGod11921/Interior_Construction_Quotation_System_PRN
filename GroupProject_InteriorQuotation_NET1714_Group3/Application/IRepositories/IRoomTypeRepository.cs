using Application.Repositories;
using Application.ViewModels;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IRoomTypeRepository : IGenericRepository<RoomType>
    {
        public IQueryable<RoomType> GetAllRoomTypes();
        Task<List<RoomTypeDTO>> GetAllRoomTypeToAdd();
        Task<RoomTypeDTO> GetRoomTypeNameById(int id);
        //Task<RoomType> GetRoomTypeNameByName(string typename);


        //Luan
        public Task<RoomType> GetRoomTypeById(int roomTypeId);
        public Task<IEnumerable<RoomType>> GetAllRoomTypesById (int roomTypeId);

    }
}
