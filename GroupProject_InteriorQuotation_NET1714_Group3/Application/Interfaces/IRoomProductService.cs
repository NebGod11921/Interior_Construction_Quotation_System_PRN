﻿using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRoomProductService
    {
        Task<bool> CreateRoomProduct(RoomProductDTO roomProductDTO);
    }
}
