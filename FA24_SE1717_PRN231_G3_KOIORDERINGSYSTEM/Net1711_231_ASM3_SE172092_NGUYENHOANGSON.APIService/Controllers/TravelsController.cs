﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Data.Models;
using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Service;
using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Service.Base;

namespace Net1711_231_ASM3_SE172092_NGUYENHOANGSON.APIService.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TravelsController : ControllerBase
{
    private readonly ITravelService _travelService;

    public TravelsController()
    {
        _travelService ??= new TravelService();
    }

    // GET: api/Travels
    [HttpGet]
    public async Task<IBusinessResult> GetTravels()
    {
        return await _travelService.GetAll();
    }

    // GET: api/Travels/5
    [HttpGet("{id}")]
    public async Task<IBusinessResult> GetTravel(Guid id)
    {
        return await _travelService.GetById(id);
    }

    // PUT: api/Travels/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut]
    public async Task<IBusinessResult> PutTravel(Travel travel)
    {
        return await _travelService.Save(travel);
    }

    // POST: api/Travels
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<IBusinessResult> PostTravel(Travel travel)
    {
        return await _travelService.Save(travel);
    }

    // DELETE: api/Travels/5
    [HttpDelete("{id}")]
    public async Task<IBusinessResult> DeleteTravel(Guid id)
    {
        return await _travelService.DeleteById(id);
    }
}