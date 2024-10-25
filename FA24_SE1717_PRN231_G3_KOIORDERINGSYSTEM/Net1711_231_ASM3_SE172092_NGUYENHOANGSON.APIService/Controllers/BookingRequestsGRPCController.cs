using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.APIService.Grpcs;
using Microsoft.AspNetCore.Mvc;

namespace Net1711_231_ASM3_SE172092_NGUYENHOANGSON.APIService.Controllers;

[Route("api/bookingRequestsgrpc")]
[ApiController]
public class BookingRequestsGRPCController : ControllerBase
{
    private readonly BookingRequestGrpcService_.BookingRequestGrpcService_Client _bookingRequestService;

    public BookingRequestsGRPCController(BookingRequestGrpcService_.BookingRequestGrpcService_Client bookingRequestServiceClient)
    {
        _bookingRequestService = bookingRequestServiceClient;
    }

    // GET: api/BookingRequests
    [HttpGet]
    public async Task<IActionResult> GetBookingRequests()
    {
        var response = await _bookingRequestService.ListBookingRequestsAsync(new Empty());
        return Ok(response);
    }

    // GET: api/BookingRequests/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookingRequest(string? id)
    {
        var response = await _bookingRequestService.GetBookingRequestAsync(new BookingRequestIdRequest { Id = id });
        return Ok(response);
    }

    // PUT: api/BookingRequests/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut]
    public async Task<IActionResult> PutBookingRequest(BookingRequestRequest bookingRequest)
    {
        var response = await _bookingRequestService.UpdateBookingRequestAsync(bookingRequest);
        return Ok(response);
    }

    // POST: api/BookingRequests
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<IActionResult> PostBookingRequest(BookingRequestRequest bookingRequest)
    {
        var response = await _bookingRequestService.CreateBookingRequestAsync(bookingRequest);
        return Ok(response);
    }

    // DELETE: api/BookingRequests/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBookingRequest(string id)
    {
        var response = await _bookingRequestService.DeleteBookingRequestAsync(new BookingRequestIdRequest { Id = id });
        return Ok(response);
    }
}