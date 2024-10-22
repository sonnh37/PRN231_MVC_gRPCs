using Azure;
using KoiOrderingSystem.APIService.Grpcs;
using KoiOrderingSystem.Common;
using KoiOrderingSystem.Data.Models;
using KoiOrderingSystem.Service.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;

namespace KoiOrderingSystem.MVCWebApp.Controllers
{

    public class TravelGrpcsController : Controller
    {
        private readonly TravelGrpcService_.TravelGrpcService_Client _travelServiceClient;

        public TravelGrpcsController(TravelGrpcService_.TravelGrpcService_Client travelServiceClient)
        {
            _travelServiceClient = travelServiceClient;
        }

        // Action để tạo chuyến đi
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TravelRequest travelRequest)
        {
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(Const.APIEndPoint + "travelsgrpc/", travelRequest))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<TravelReply>(content);
                            if (result.Message.Contains("successfully"))
                            {
                                return RedirectToAction(nameof(Index));
                            }
                            else
                            {
                                return View(travelRequest);
                            }
                        }
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
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "travelsgrpc/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<TravelReply>(content);

                        if (result != null)
                        {
                            var data = result.Travel;
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
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "travelsgrpc/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<TravelReply>(content);

                        if (result != null)
                        {
                            var data = result.Travel;
                            return View(data);
                        }
                    }
                }
            }
            return View();
        }
        // Action để cập nhật chuyến đi
        [HttpPost]
        public async Task<IActionResult> Edit(TravelRequest travelRequest)
        {

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsJsonAsync(Const.APIEndPoint + "travelsgrpc/", travelRequest))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<TravelReply>(content);
                        if (result.Message.Contains("successfully"))
                        {
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
            }
            return View(travelRequest);
        }

        // Action để xóa chuyến đi
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(Const.APIEndPoint + "travelsgrpc/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<TravelReply>(content);

                        if (result.Message.Contains("successfully"))
                        {
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            return View(result);
                        }
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string? id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "travelsgrpc/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<TravelReply>(content);

                        if (result != null)
                        {
                            var data = result.Travel;
                            return View(data);
                        }
                    }
                }
            }
            return View();
        }


        // Action để lấy danh sách tất cả chuyến đi
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "travelsgrpc"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<TravelListReply>(content);
                        if (result != null)
                        {
                            var data = result.Travels.ToList();
                            return View(data);
                        }
                    }
                }
            }
            return View();
        }
    }

}
