using System.Text;
using System.Text.Json;
using Assignment2.Data.ClientInterfaces;

namespace Assignment2.Data; 

public class UsersService : IUsersService {
    private readonly HttpClientHandler _handler;
    private const string BasePath = "https://localhost:7173";

    public UsersService() {
        // Trusting all certificates
        //https://stackoverflow.com/a/46626858
        _handler = new HttpClientHandler();
        _handler.ClientCertificateOptions = ClientCertificateOption.Manual;
        _handler.ServerCertificateCustomValidationCallback =
            (httpRequestMessage, cert, cetChain, policyErrors) => {
                return true; 
            };
    }

    public async Task<User> Register(User submittedUser) {
        // executing request
        using HttpClient client = new HttpClient(_handler);
        string userAsJson = JsonSerializer.Serialize(submittedUser);
        var requestContent = new StringContent(userAsJson, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(BasePath+"/Users", requestContent);

        // handling error responses
        if (!response.IsSuccessStatusCode) {
            Console.WriteLine("Error while registering user!");
            Console.WriteLine("JSON request: "+userAsJson);
            Console.WriteLine("Error code: "+response.StatusCode);
            Console.WriteLine("Error message: "+ await response.Content.ReadAsStringAsync());
            throw new Exception($@"Failed to register user! Error: {await response.Content.ReadAsStringAsync()}");
        }

        // handling successful responses
        var responseJsonString = await response.Content.ReadAsStringAsync();
        var returnedUser = JsonSerializer.Deserialize<User>(responseJsonString);

        if (returnedUser == null)
            throw new JsonException("Couldn't parse returned user!");
        
        return returnedUser;
    }

    public async Task<string> Logout(User submittedUser) {
        // executing request
        using HttpClient client = new HttpClient(_handler);
        string userAsJson = JsonSerializer.Serialize(submittedUser);
        var requestContent = new StringContent(userAsJson, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(BasePath+"/Logout", requestContent);
        
        // handling error responses
        if (!response.IsSuccessStatusCode) {
            Console.WriteLine("Error while logging out user!");
            Console.WriteLine("JSON request: "+userAsJson);
            Console.WriteLine("Error code: "+response.StatusCode);
            Console.WriteLine("Error message: "+ await response.Content.ReadAsStringAsync());
            throw new Exception($@"Failed to logout user! Error: {await response.Content.ReadAsStringAsync()}");
        }

        // handling successful responses
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> Login(User submittedUser) {
        // executing request
        using HttpClient client = new HttpClient(_handler);
        string userAsJson = JsonSerializer.Serialize(submittedUser);
        var requestContent = new StringContent(userAsJson, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(BasePath+"/Login", requestContent);
        
        // handling error responses
        if (!response.IsSuccessStatusCode) {
            Console.WriteLine("Error while logging in user!");
            Console.WriteLine("JSON request: "+userAsJson);
            Console.WriteLine("Error code: "+response.StatusCode);
            Console.WriteLine("Error message: "+ await response.Content.ReadAsStringAsync());
            throw new Exception($@"Failed to login user! Error: {await response.Content.ReadAsStringAsync()}");
        }

        // handling successful responses
        return await response.Content.ReadAsStringAsync();
    }
}