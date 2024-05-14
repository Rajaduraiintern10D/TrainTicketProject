using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using TicketBookingProject.Data.Dto;
using TicketBookingProject.Models;
using TicketBookingProjectUI.Models;

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
        #region AuthenticateAsync
        public async Task<string> AuthenticateAsync(LoginViewModel loginViewModel)
        {

            var response = await _httpClient.PostAsJsonAsync("api/Auth/authenticate", loginViewModel);
            if (response.IsSuccessStatusCode)
            {
                // Authentication successful, extract JWT token from response
                var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
                // Return the token
                return tokenResponse.Token;
            }
            else
            {
                return null;
            }

        }
        #endregion


        public async Task<string> GetDataAsync()
        {
            var  request = new HttpRequestMessage(HttpMethod.Get, "api/TrainDetails/GetAllTrains");

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
                // Create a new HttpRequestMessage with the GET method
                var request = new HttpRequestMessage(HttpMethod.Get, $"api/Passenger/GetPassengerById/{passengerID}");

                // Get the JWT token from session storage
                var jwtToken = _httpContextAccessor.HttpContext.Session.GetString("JWTToken");

                // If a JWT token exists, include it in the authorization header
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    // Include JWT token in the authorization header
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                }

                // Send the request
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
                var jwtToken = _httpContextAccessor.HttpContext.Session.GetString("JWTToken");

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
        public async Task<string> GetSeatAvailabilityAsync(int trainNumber)
        {
            try
            {
                // Construct the URI with the train number
                var requestUri = $"api/TrainDetails/{trainNumber}";

                var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

                // Get the JWT token from session storage
                var jwtToken = _httpContextAccessor.HttpContext.Session.GetString("JWTToken");

                // If a JWT token exists, include it in the authorization header
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    // Include JWT token in the authorization header
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                }

                // Send the request
                var response = await _httpClient.SendAsync(request);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the content of the response as a string
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    // If the response is not successful, handle the error condition
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

        public async Task<bool> UpdatePassengerAsync(int passengerId, PassengerDto passengerDto)
        {
            try
            {
                var apiUrl = $"api/Passenger/UpdatePassengerDetails/{passengerId}";

                var jsonContent = new StringContent(JsonConvert.SerializeObject(passengerDto), Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(HttpMethod.Put, apiUrl);
                request.Content = jsonContent;

                var jwtToken = _httpContextAccessor.HttpContext.Session.GetString("JWTToken");
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                }

                var response = await _httpClient.SendAsync(request);

                // Check if the response is successful
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false; // Return false to indicate failure
            }
        }
        public async Task<string> GetAllTrainDetailsAsync()
        {
            try
            {
                string url = "api/TrainDetails/GetAllTrains";

                // Create a new GET request
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

                // Get the JWT token from session storage
                var jwtToken = _httpContextAccessor.HttpContext.Session.GetString("JWTToken");

                // If a JWT token exists, include it in the authorization header
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    // Include JWT token in the authorization header
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                }

                // Send the request
                HttpResponseMessage response = await _httpClient.SendAsync(request);

                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the content of the response as a string
                    string responseData = await response.Content.ReadAsStringAsync();
                    return responseData;
                }
                else
                {
                    // If the response is not successful, throw an exception with the status code
                    throw new Exception($"Failed to retrieve all train details. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the request
                throw new Exception($"An error occurred while retrieving all train details: {ex.Message}");
            }
        }


    }
}
