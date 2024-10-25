using Grpc.Core;
using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Data.Models;
using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Service;
namespace Net1711_231_ASM3_SE172092_NGUYENHOANGSON.APIService.Grpcs;

public class BookingRequestGrpcService : BookingRequestGrpcService_.BookingRequestGrpcService_Base
{
    private readonly IBookingRequestService _bookingRequestService;

    public BookingRequestGrpcService()
    {
        _bookingRequestService ??= new BookingRequestService();
    }

    // CreateBookingRequest implementation
    public override async Task<BookingRequestReply> CreateBookingRequest(BookingRequestRequest request, ServerCallContext context)
    {
        try
        {
            var newBookingRequest = new BookingRequest
            {
                CustomerId = Guid.TryParse(request.CustomerId, out var customerId) ? customerId : (Guid?)null,
                TravelId = Guid.TryParse(request.TravelId, out var travelId) ? travelId : (Guid?)null,
                QuantityService = request.QuantityService,
                NumberOfPerson = request.NumberOfPerson,
                Status = request.Status.ToString(),
                CreatedBy = request.CreatedBy,
                CreatedDate = DateTime.UtcNow,
                UpdatedBy = request.UpdatedBy,
                UpdatedDate = DateTime.UtcNow,
                IsDeleted = request.IsDeleted,
                Note = request.Note
            };

            var result = await _bookingRequestService.Save(newBookingRequest);

            if (result.Status == 1) return new BookingRequestReply { Message = "BookingRequest created successfully" , BookingRequestRequest = request};

            return new BookingRequestReply { Message = "Failed to create bookingRequest", BookingRequestRequest = null };
        }
        catch (Exception ex)
        {
            context.Status = new Status(StatusCode.Unknown, ex.Message);
            return new BookingRequestReply { Message = "Error creating bookingRequest", BookingRequestRequest = null };
        }
    }

    // GetBookingRequest implementation
    public override async Task<BookingRequestReply> GetBookingRequest(BookingRequestIdRequest request, ServerCallContext context)
    {
        try
        {
            var result = await _bookingRequestService.GetById(Guid.Parse(request.Id));

            if (result.Status == 1 && result.Data != null)
            {
                var bookingRequest = result.Data as BookingRequest;

                return new BookingRequestReply
                {
                    Message = $"Found bookingRequest with id {request.Id}",
                    BookingRequestRequest = new BookingRequestRequest
                    {
                        Id = bookingRequest.Id.ToString(),
                        CustomerId = bookingRequest.CustomerId?.ToString(),
                        TravelId = bookingRequest.TravelId?.ToString(),
                        QuantityService = bookingRequest.QuantityService ?? 0,
                        NumberOfPerson = bookingRequest.NumberOfPerson ?? 0,
                        Status = (BookingRequestStatus)Enum.Parse(typeof(BookingRequestStatus), bookingRequest.Status.ToString()),
                        CreatedBy = bookingRequest.CreatedBy,
                        CreatedDate = bookingRequest.CreatedDate?.ToString("o"),
                        UpdatedBy = bookingRequest.UpdatedBy,
                        UpdatedDate = bookingRequest.UpdatedDate?.ToString("o"),
                        IsDeleted = bookingRequest.IsDeleted,
                        Note = bookingRequest.Note
                    }
                };
            }

            return new BookingRequestReply { Message = "BookingRequest not found" };
        }
        catch (Exception ex)
        {
            context.Status = new Status(StatusCode.Unknown, ex.Message);
            return new BookingRequestReply { Message = "Error fetching bookingRequest" };
        }
    }

    // UpdateBookingRequest implementation
    public override async Task<BookingRequestReply> UpdateBookingRequest(BookingRequestRequest request, ServerCallContext context)
    {
        try
        {
            var bookingRequest = new BookingRequest
            {
                Id = Guid.Parse(request.Id),  
                CustomerId = Guid.Parse(request.CustomerId),  
                TravelId = Guid.Parse(request.TravelId),
                QuantityService = request.QuantityService,
                NumberOfPerson = request.NumberOfPerson,
                Status = request.Status.ToString(),
                CreatedBy = request.CreatedBy,
                CreatedDate = DateTime.Parse(request.CreatedDate),
                UpdatedBy = request.UpdatedBy,
                UpdatedDate = DateTime.Parse(request.UpdatedDate),
                IsDeleted = request.IsDeleted,
                Note = request.Note
            };

            var updateResult = await _bookingRequestService.Save(bookingRequest);

            if (updateResult.Status == 1) return new BookingRequestReply { Message = "BookingRequest updated successfully", BookingRequestRequest = request };
            return new BookingRequestReply { Message = "Failed to update bookingRequest", BookingRequestRequest = null };
        }
        catch (FormatException ex)
        {
            context.Status = new Status(StatusCode.InvalidArgument, "Invalid GUID format: " + ex.Message);
            return new BookingRequestReply { Message = "Error updating bookingRequest: Invalid GUID format" , BookingRequestRequest = null };
        }
        catch (Exception ex)
        {
            context.Status = new Status(StatusCode.Unknown, ex.Message);
            return new BookingRequestReply { Message = "Error updating bookingRequest" , BookingRequestRequest = null };
        }
    }

    // DeleteBookingRequest implementation
    public override async Task<BookingRequestReply> DeleteBookingRequest(BookingRequestIdRequest request, ServerCallContext context)
    {
        try
        {
            var deleteResult = await _bookingRequestService.DeleteById(Guid.Parse(request.Id));

            if (deleteResult.Status == 1) return new BookingRequestReply { Message = "BookingRequest deleted successfully", BookingRequestRequest = new BookingRequestRequest() };

            return new BookingRequestReply { Message = "Failed to delete bookingRequest", BookingRequestRequest = null };
        }
        catch (Exception ex)
        {
            context.Status = new Status(StatusCode.Unknown, ex.Message);
            return new BookingRequestReply { Message = "Error deleting bookingRequest", BookingRequestRequest = null };
        }
    }

    // ListBookingRequests implementation
    public override async Task<BookingRequestListReply> ListBookingRequests(Empty request, ServerCallContext context)
    {
        try
        {
            var br = await _bookingRequestService.GetAll();

            if (br == null || br.Data == null)
                return new BookingRequestListReply(); 

            var bookingRequestList = br.Data as List<BookingRequest> ?? new List<BookingRequest>(); 

            var bookingRequestRequests = bookingRequestList.Select(bookingRequest => new BookingRequestRequest
            {
                Id = bookingRequest.Id.ToString(), 
                CustomerId = bookingRequest.CustomerId.ToString(),
                TravelId = bookingRequest.TravelId.ToString(),
                QuantityService = bookingRequest.QuantityService.Value,
                NumberOfPerson = bookingRequest.NumberOfPerson.Value,
                Status = Enum.TryParse<BookingRequestStatus>(bookingRequest.Status, out var status) ? status : BookingRequestStatus.Pending,
                CreatedBy = bookingRequest.CreatedBy,
                CreatedDate = bookingRequest.CreatedDate.ToString(),
                UpdatedBy = bookingRequest.UpdatedBy,
                UpdatedDate = bookingRequest.UpdatedDate.ToString(),
                IsDeleted = bookingRequest.IsDeleted,
                Note = bookingRequest.Note
            }).ToList();

            return new BookingRequestListReply { BookingRequestRequests = { bookingRequestRequests } };
        }
        catch (Exception ex)
        {
            context.Status = new Status(StatusCode.Unknown, ex.Message);
            return new BookingRequestListReply(); // Trả về danh sách rỗng hoặc có lỗi
        }
    }

}