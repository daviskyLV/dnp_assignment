namespace Assignment2.Data; 

public abstract class Testing {
    //testing
    public static async Task<string> FetchThing() {
        // Trusting all certificates
        //https://stackoverflow.com/a/46626858
        var handler = new HttpClientHandler();
        handler.ClientCertificateOptions = ClientCertificateOption.Manual;
        handler.ServerCertificateCustomValidationCallback =
            (httpRequestMessage, cert, cetChain, policyErrors) => {
            return true;
        };

        using HttpClient client = new HttpClient(handler);
        HttpResponseMessage responseMessage = await client.GetAsync("http://localhost:5166/Posts");
        
        Console.WriteLine("Blazor server print?");
        Console.WriteLine(await responseMessage.Content.ReadAsStringAsync());
        
        if (!responseMessage.IsSuccessStatusCode)
            Console.WriteLine("Didnt work at least it works!");
        else
            Console.WriteLine("Works what!");

        return "Response status code: "+responseMessage.StatusCode;
    }
}