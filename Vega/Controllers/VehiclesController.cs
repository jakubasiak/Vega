using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vega.Models;
using Vega.Controllers.Resources;
using AutoMapper;
using Vega.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Vega.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly IMapper mapper;
        private readonly VegaDbContext vegaDbContext;

        public VehiclesController(IMapper mapper, VegaDbContext vegaDbContext)
        {
            this.mapper = mapper;
            this.vegaDbContext = vegaDbContext;
        }
        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody]VehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = mapper.Map<VehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            vegaDbContext.Vehicles.Add(vehicle);
            await vegaDbContext.SaveChangesAsync();

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody]VehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicleToUpdate = await vegaDbContext.Vehicles.Include(v => v.Features).SingleOrDefaultAsync(v => v.Id == id);

            if (vehicleToUpdate == null)
            {
                return NotFound();

            }
            mapper.Map<VehicleResource, Vehicle>(vehicleResource, vehicleToUpdate);
            vehicleToUpdate.LastUpdate = DateTime.Now;

            await vegaDbContext.SaveChangesAsync();

            var result = mapper.Map<Vehicle, VehicleResource>(vehicleToUpdate);

            return Ok(result);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await vegaDbContext.Vehicles.FindAsync(id);

            if(vehicle == null)
            {
                return NotFound();
            }

            vegaDbContext.Vehicles.Remove(vehicle);
            await vegaDbContext.SaveChangesAsync();

            return Ok(id);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await vegaDbContext.Vehicles.Include(v => v.Features).SingleOrDefaultAsync(v => v.Id == id);

            if (vehicle == null)
            {
                return NotFound();
            }
            var vehicleResource = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(vehicleResource);
        }
    }
}