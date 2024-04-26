    using System;
    using TicketBookingProjectUI.Models;
    using System.Net.Http;
    using TicketBookingProject.Models;
    using System.Net.Http.Json;
    using System.Text.Json;
    using System.Threading.Tasks;
    using TicketBookingProjectUI.Models;
    using System.Net.Http.Headers;
    using TicketBookingProject.Data.Dto;
    using System.Linq.Expressions;
    using Azure.Core;
    using Newtonsoft.Json;
    using System.Text;
    using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using static System.Runtime.InteropServices.JavaScript.JSType;

    namespace TicketBookingProjectUI.ConstantFile
    {
        public class ApiService
        {
            private readonly HttpClient _httpClient;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public ApiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
            {
                _httpClient = httpClientFactory.CreateClient();
                _httpClient.BaseAddress = new System.Uri("http://localhost:5121");
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<bool> AuthenticateAsync(LoginViewModel loginViewModel)
            {

                var response = await _httpClient.PostAsJsonAsync("api/Auth/authenticate", loginViewModel);
                if (response.IsSuccessStatusCode)
                {
                    // Authentication successful, extract JWT token from response
                    var tokenresponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
                    // Store the token in session storage
                    _httpContextAccessor.HttpContext.Session.SetString("JWTToken", tokenresponse.Token);
                    return true;
                }
                else
                {
                    return false;
                }

            }
            public async Task<string> GetDataAsync()
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "api/TrainDetails/GetAllTrains");

                // Get JWT token from session storage
                var jwtToken = _httpContextAccessor.HttpContext.Session.GetString("JWTToken");
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    // Include JWT token in the authorization header
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                }

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    // Handle error response
                    return null;
                }
            }
            public async Task<bool> ForgetPasswordAsync(ForgotPasswordViewModel model)
            {
                try
                {
                    var updatePasswordDto = new UpdatePasswordDto
                    {
                        UserNameOrEmail = model.UsernameOrEmail,
                        NewPassword = model.NewPassword
                    };
                    var response = await _httpClient.PutAsJsonAsync("api/Auth/UpdatePassword", updatePasswordDto);
                    return response.IsSuccessStatusCode;
                    // If verification succeeds, proceed to update the password

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            public async Task<bool> ResgisterAsync(RegisterUser registerUser)
            {
                try
                {
                    var UsersCredential = new UsersCredential
                    {
                        User_Name = registerUser.User_Name,
                        User_Password = registerUser.User_Password,
                        Role = registerUser.Role,
                        MailId = registerUser.MailId


                    };
                    var response = await _httpClient.PostAsJsonAsync("api/Auth/Register", UsersCredential);
                    return response.IsSuccessStatusCode;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            public async Task<HttpResponseMessage> PassengerAsync(PassengerDto passenger)
            {
                try
                {

                    var request = new HttpRequestMessage(HttpMethod.Post, "api/Passenger/AddPassenger");

                    var jsonContent = new StringContent(JsonConvert.SerializeObject(passenger), Encoding.UTF8, "application/json");
                    request.Content = jsonContent;


                    var jwtToken = _httpContextAccessor.HttpContext.Session.GetString("JWTToken");
                    if (!string.IsNullOrEmpty(jwtToken))
                    {

                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                    }


                    var response = await _httpClient.SendAsync(request);

                    return response;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        public async Task<string> GetPassengerAsync(int passengerID)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"api/Passenger/GetPassengerById/{passengerID}");


                //Get the Jwt token from session Storage
                var jwtToken =_httpContextAccessor.HttpContext.Session.GetString("JWTToken");
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    //include Jwttoken in the authorization header
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer",jwtToken);
                }
                //send the request
                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    // Read the content of the response as a string
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    // If the response is not successful, return null or handle the error as needed
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the request
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<string> SendDateRangeData(DateTime fromDate, DateTime toDate)
        {
            try
            {
                //Get Jwt Token From session Storage
                var jwtToken=_httpContextAccessor.HttpContext.Session.GetString("JWTToken");

                // Construct the URL for the API endpoint
                var toDateParameter = toDate == DateTime.MinValue ? "" : toDate.ToString("yyyy-MM-dd");
                var url = $"api/TrainDetails/?fromDate={fromDate.ToString("yyyy-MM-dd")}&toDate={toDateParameter}";
                // Create a request message
                var request = new HttpRequestMessage(HttpMethod.Get, url);

                // Add JWT token to the authorization header if available
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                }

                // Send the request and get the response
                var response = await _httpClient.SendAsync(request);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read and return the response content
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    // Handle error response
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the request
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
            }

    }
}
