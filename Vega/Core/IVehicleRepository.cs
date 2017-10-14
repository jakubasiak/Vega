﻿using System.Threading.Tasks;
using Vega.Core.Models;

namespace Vega.Core
{
    public interface IVehicleRepository
    {
            void Add(Vehicle vehicle);
            Task<Vehicle> GetVehicle(int id, bool includeRelated = true);
            void Remove(Vehicle vehicle);
    }
}