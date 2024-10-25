using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Data.Models;
using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Service;
using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Service.Base;

namespace Net1711_231_ASM3_SE172092_NGUYENHOANGSON.APIService.Controllers;

[Route("api/[controller]")]
[ApiController]
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

    
}