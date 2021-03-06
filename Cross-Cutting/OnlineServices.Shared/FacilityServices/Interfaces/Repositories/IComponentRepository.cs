﻿using OnlineServices.Common.DataAccessHelpers;
using OnlineServices.Common.FacilityServices.TransfertObjects;

using System.Collections.Generic;

namespace OnlineServices.Common.FacilityServices.Interfaces.Repositories
{
    public interface IComponentRepository : IRepository<ComponentTO, int>
    {
        List<ComponentTO> GetComponentByRoom(RoomTO Room);

    }
}
