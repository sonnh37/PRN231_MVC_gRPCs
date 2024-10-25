using Grpc.Core;
using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.APIService.Grpcs;
using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Data.Models;
using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Service;

namespace Net1711_231_ASM3_SE172092_NGUYENHOANGSON.APIService.Grpcs;

public class TravelGrpcService : TravelGrpcService_.TravelGrpcService_Base
{
    private readonly ITravelService _travelService;

    public TravelGrpcService()
    {
        _travelService ??= new TravelService();
    }

    // CreateTravel implementation
    public override async Task<TravelReply> CreateTravel(TravelRequest request, ServerCallContext context)
    {
        try
        {
            var newTravel = new Travel
            {
                Name = request.Name,
                Location = request.Location,
                Price = (decimal)request.Price,
                Note = request.Note,
                IsDeleted = false
            };

            var result = await _travelService.Save(newTravel);

            if (result.Status == 1) return new TravelReply { Message = "Travel created successfully" };

            return new TravelReply { Message = "Failed to create travel" };
        }
        catch (Exception ex)
        {
            context.Status = new Status(StatusCode.Unknown, ex.Message);
            return new TravelReply { Message = "Error creating travel" };
        }
    }

    // GetTravel implementation
    public override async Task<TravelReply> GetTravel(TravelIdRequest request, ServerCallContext context)
    {
        try
        {
            var result = await _travelService.GetById(Guid.Parse(request.Id));

            if (result.Status == 1 && result.Data != null)
            {
                var travel = result.Data as Travel;

                return new TravelReply
                {
                    Message = $"Found travel with id {request.Id}",
                    Travel = new TravelRequest
                    {
                        Id = travel.Id.ToString(),
                        Name = travel.Name ?? string.Empty,
                        Location = travel.Location ?? string.Empty,
                        Price = travel.Price.HasValue ? (double)travel.Price.Value : 0,
                        Note = travel.Note
                    }
                };
            }

            return new TravelReply { Message = "Travel not found" };
        }
        catch (Exception ex)
        {
            context.Status = new Status(StatusCode.Unknown, ex.Message);
            return new TravelReply { Message = "Error fetching travel" };
        }
    }

    // UpdateTravel implementation
    public override async Task<TravelReply> UpdateTravel(TravelRequest request, ServerCallContext context)
    {
        try
        {
            var travel = new Travel();
            travel.Id = Guid.Parse(request.Id);
            travel.Name = request.Name;
            travel.Location = request.Location;
            travel.Price = (decimal)request.Price;
            travel.Note = request.Note;
            travel.IsDeleted = false;

            var updateResult = await _travelService.Save(travel);

            if (updateResult.Status == 1) return new TravelReply { Message = "Travel updated successfully" };
            return new TravelReply { Message = "Failed to update travel" };
        }
        catch (Exception ex)
        {
            context.Status = new Status(StatusCode.Unknown, ex.Message);
            return new TravelReply { Message = "Error updating travel" };
        }
    }

    // DeleteTravel implementation
    public override async Task<TravelReply> DeleteTravel(TravelIdRequest request, ServerCallContext context)
    {
        try
        {
            var deleteResult = await _travelService.DeleteById(Guid.Parse(request.Id));

            if (deleteResult.Status == 1) return new TravelReply { Message = "Travel deleted successfully" };

            return new TravelReply { Message = "Failed to delete travel" };
        }
        catch (Exception ex)
        {
            context.Status = new Status(StatusCode.Unknown, ex.Message);
            return new TravelReply { Message = "Error deleting travel" };
        }
    }

    // ListTravels implementation
    public override async Task<TravelListReply> ListTravels(Empty request, ServerCallContext context)
    {
        try
        {
            var br = await _travelService.GetAll();

            if (br == null || br.Data == null) return new TravelListReply(); // Return empty list

            var travelList = br.Data as List<Travel> ?? new List<Travel>(); // Convert or create an empty list

            var travelRequests = travelList.Select(travel => new TravelRequest
            {
                Id = travel.Id.ToString(),
                Name = travel.Name ?? string.Empty,
                Location = travel.Location ?? string.Empty,
                Price = travel.Price.HasValue ? (double)travel.Price.Value : 0,
                Note = travel.Note
            }).ToList();

            return new TravelListReply { Travels = { travelRequests } };
        }
        catch (Exception ex)
        {
            context.Status = new Status(StatusCode.Unknown, ex.Message);
            return new TravelListReply(); // Return empty or error
        }
    }
}