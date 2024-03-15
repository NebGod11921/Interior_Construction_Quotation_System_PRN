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
        //Task<List<RoomTypeDTO>> GetAllRoomTypeDTOs();
        Task<List<RoomTypeDTO>> GetAllRoomTypeToAdd();
        Task<RoomTypeDTO> GetRoomTypeNameById(int id);
        Task<List<RoomTypeDTO1>> GetAllRoomTypeDTOs();


    }
}
