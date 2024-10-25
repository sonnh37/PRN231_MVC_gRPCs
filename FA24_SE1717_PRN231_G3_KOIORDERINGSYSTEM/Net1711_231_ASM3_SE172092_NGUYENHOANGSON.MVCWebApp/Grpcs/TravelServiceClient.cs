namespace Net1711_231_ASM3_SE172092_NGUYENHOANGSON.MVCWebApp.Grpcs;

//public class TravelServiceClient
//{
//    private readonly TravelGrpcService_.TravelGrpcService_Client _client;

//    public TravelServiceClient(GrpcChannel channel)
//    {
//        _client = new TravelGrpcService_.TravelGrpcService_Client(channel);
//    }

//    public async Task<TravelReply> CreateTravelAsync(TravelRequest request)
//    {
//        return await _client.CreateTravelAsync(request);
//    }

//    public async Task<TravelReply> GetTravelAsync(TravelIdRequest request)
//    {
//        return await _client.GetTravelAsync(request);
//    }

//    public async Task<TravelListReply> ListTravelsAsync(Empty request)
//    {
//        return await _client.ListTravelsAsync(request);
//    }
//    // Thêm các phương thức khác như UpdateTravel, DeleteTravel, ListTravels
//}