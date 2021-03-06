﻿using OnlineServices.Common.DataAccessHelpers;
using OnlineServices.Common.FacilityServices.TransfertObjects;

using System.Collections.Generic;

namespace OnlineServices.Common.FacilityServices.Interfaces.Repositories
{
    public interface IRoomRepository : IRepository<RoomTO, int>
    {
        List<RoomTO> GetRoomsByFloors(FloorTO Floor);
    }
}
