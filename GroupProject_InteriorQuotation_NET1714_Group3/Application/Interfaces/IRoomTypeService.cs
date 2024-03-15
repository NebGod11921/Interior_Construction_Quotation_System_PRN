using Application.Services;
using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRoomTypeService
    {
        List<RoomHomePageTitle> GetAllRoomTypes();
        Task<List<RoomTypeDTO>> GetAllRoomTypeToAdd();
        Task<RoomTypeDTO> GetRoomTypeNameById(int id);
        
        //Luan
        public Task<IEnumerable<RoomTypeDTOS>> GetRoomTypesById(int id);
        public Task<IEnumerable<RoomTypeDTOS>> GetAllTypesRoom();
        public Task<RoomTypeDTOS> GetRoomTypeById(int id);


    }
}
