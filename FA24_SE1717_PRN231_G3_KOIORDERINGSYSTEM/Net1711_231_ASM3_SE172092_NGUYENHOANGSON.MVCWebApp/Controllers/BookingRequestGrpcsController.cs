using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.APIService.Grpcs;
using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Common;
using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Data.Models;
using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Service.Base;
using Newtonsoft.Json;

namespace Net1711_231_ASM3_SE172092_NGUYENHOANGSON.MVCWebApp.Controllers;

public class BookingRequestGrpcsController : Controller
{
    private readonly BookingRequestGrpcService_.BookingRequestGrpcService_Client _bookingRequestServiceClient;

    public BookingRequestGrpcsController(BookingRequestGrpcService_.BookingRequestGrpcService_Client bookingRequestServiceClient)
    {
        _bookingRequestServiceClient = bookingRequestServiceClient;
    }

    // Action để tạo chuyến đi
    public async Task<IActionResult> Create()
    {

        ViewBag.Customers = new SelectList(await GetUsersAsync(), "Id", "Username");
        ViewBag.Travels = new SelectList(await GetTravelsAsync(), "Id", "Name");
        ViewBag.Status = new SelectList(Enum.GetValues(typeof(BookingRequestStatus)).Cast<BookingRequestStatus>()
                                .Select(s => new { Value = (int)s, Text = s.ToString() }),
                                "Value", "Text");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(BookingRequestRequest bookingRequestRequest)
    {
        if (ModelState.IsValid)
            using (var httpClient = new HttpClient())
            {
                using (var response =
                       await httpClient.PostAsJsonAsync(Const.APIEndPoint + "bookingRequestsgrpc/", bookingRequestRequest))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BookingRequestReply>(content);
                        if (result.BookingRequestRequest != null)
                            return RedirectToAction(nameof(Index));
                        return View(bookingRequestRequest);
                    }
                }
            }

        return RedirectToAction(nameof(Index));
    }

    // Action để lấy chuyến đi theo ID
    [HttpGet]
    public async Task<IActionResult> Details(string? id)
    {
        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync(Const.APIEndPoint + "bookingRequestsgrpc/" + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BookingRequestReply>(content);

                    if (result != null)
                    {
                        var data = result.BookingRequestRequest;
                        return View(data);
                    }
                }
            }
        }

        return View();
    }

    public async Task<IActionResult> Edit(string? id)
    {
        if (id == null) return RedirectToAction(nameof(Index));

        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync(Const.APIEndPoint + "bookingRequestsgrpc/" + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BookingRequestReply>(content);

                    if (result != null)
                    {
                        ViewBag.Customers = new SelectList(await GetUsersAsync(), "Id", "Username");
                        ViewBag.Travels = new SelectList(await GetTravelsAsync(), "Id", "Name");
                        ViewBag.Status = new SelectList(Enum.GetValues(typeof(BookingRequestStatus)).Cast<BookingRequestStatus>()
                                                .Select(s => new { Value = (int)s, Text = s.ToString() }),
                                                "Value", "Text");
                        var data = result.BookingRequestRequest;
                        return View(data);
                    }
                }
            }
        }

        return View();
    }

    // Action để cập nhật chuyến đi
    [HttpPost]
    public async Task<IActionResult> Edit(BookingRequestRequest bookingRequestRequest)
    {
        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.PutAsJsonAsync(Const.APIEndPoint + "bookingRequestsgrpc/", bookingRequestRequest))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BookingRequestReply>(content);
                    if (result.BookingRequestRequest != null) return RedirectToAction(nameof(Index));
                }
            }
        }

        return View(bookingRequestRequest);
    }

    // Action để xóa chuyến đi
    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.DeleteAsync(Const.APIEndPoint + "bookingRequestsgrpc/" + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BookingRequestReply>(content);

                    if (result.BookingRequestRequest != null)
                        return RedirectToAction(nameof(Index));
                    return View(result);
                }
            }
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(string? id)
    {
        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync(Const.APIEndPoint + "bookingRequestsgrpc/" + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BookingRequestReply>(content);

                    if (result != null)
                    {
                        var data = result.BookingRequestRequest;
                        return View(data);
                    }
                }
            }
        }

        return View();
    }

    private async Task<List<Travel>> GetTravelsAsync()
    {
        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Travels"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                    if (result != null)
                    {
                        var data = JsonConvert.DeserializeObject<List<Travel>>(result.Data.ToString());
                        return data;
                    }
                }
            }
        }
        return new List<Travel>();
    }

    private async Task<List<User>> GetUsersAsync()
    {
        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Users"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                    if (result != null)
                    {
                        var data = JsonConvert.DeserializeObject<List<User>>(result.Data.ToString());
                        return data;
                    }
                }
            }
        }
        return new List<User>();
    }
    // Action để lấy danh sách tất cả chuyến đi
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync(Const.APIEndPoint + "bookingRequestsgrpc"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BookingRequestListReply>(content);
                    if (result != null)
                    {
                        var data = result.BookingRequestRequests.ToList();
                        return View(data);
                    }
                }
            }
        }

        return View();
    }
}