using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.APIService.Grpcs;
using Microsoft.AspNetCore.Mvc;

namespace Net1711_231_ASM3_SE172092_NGUYENHOANGSON.APIService.Controllers;

[Route("api/travelsgrpc")]
[ApiController]
public class TravelsGRPCController : ControllerBase
{
    private readonly TravelGrpcService_.TravelGrpcService_Client _travelService;

    public TravelsGRPCController(TravelGrpcService_.TravelGrpcService_Client travelServiceClient)
    {
        _travelService = travelServiceClient;
    }

    // GET: api/Travels
    [HttpGet]
    public async Task<IActionResult> GetTravels()
    {
        var response = await _travelService.ListTravelsAsync(new Empty());
        return Ok(response);
    }

    // GET: api/Travels/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTravel(string? id)
    {
        var response = await _travelService.GetTravelAsync(new TravelIdRequest { Id = id });
        return Ok(response);
    }

    // PUT: api/Travels/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut]
    public async Task<IActionResult> PutTravel(TravelRequest travel)
    {
        var response = await _travelService.UpdateTravelAsync(travel);
        return Ok(response);
    }

    // POST: api/Travels
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<IActionResult> PostTravel(TravelRequest travel)
    {
        var response = await _travelService.CreateTravelAsync(travel);
        return Ok(response);
    }

    // DELETE: api/Travels/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTravel(string id)
    {
        var response = await _travelService.DeleteTravelAsync(new TravelIdRequest { Id = id });
        return Ok(response);
    }
}