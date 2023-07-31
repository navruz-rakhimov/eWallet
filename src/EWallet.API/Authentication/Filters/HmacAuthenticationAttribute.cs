using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EWallet.API.Authentication.Filters;

public class HmacAuthenticationAttribute : ActionFilterAttribute
{
    private static readonly Dictionary<string, string> ClientApiKeys = new();

    public HmacAuthenticationAttribute()
    {
        // Let's just use a single (not per client) hard-coded 'API Key' for all clients
        // instead of storing in db - for test project
        
        // Let's use just User-Id instead instead of an 'AppId'
        ClientApiKeys["1"] = "MyPrivateKey";
    }

    private static string ComputeHashString(string contentString, string sharedSecretKey)
    {
        var contentByteArray = Encoding.UTF8.GetBytes(contentString.Replace("\r\n", "\n"));
        var sharedKeyByteArray = Encoding.UTF8.GetBytes(sharedSecretKey);
        
        using var hmac = new HMACSHA1(sharedKeyByteArray);
        byte[] signature = hmac.ComputeHash(contentByteArray);
        
        var sb = new StringBuilder();
        foreach (var hashByte in signature)
        {
            sb.Append($"{hashByte:x2}");
        }

        var hashString = sb.ToString();
        return hashString;
    }
    
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var request = context.HttpContext.Request;
        var contentString = "";
        
        using StreamReader sr = new(request.Body);
        if (request.Body.CanSeek)
            request.Body.Seek(0, SeekOrigin.Begin);
        if (request.Body.CanRead)
            contentString = await sr.ReadToEndAsync();
        
        if (!request.Headers.TryGetValue("X-UserId", out var userIdHeaderValue))
        {
            context.Result = new BadRequestObjectResult("X-UserId header is missing");
            return;
        }

        if (string.IsNullOrEmpty(contentString))
        {
            await next();
            return;
        }
        
        if (!request.Headers.TryGetValue("X-Digest", out var digestHeaderValue))
        {
            context.Result = new BadRequestObjectResult("X-Digest header is missing");
            return;
        }

        if (!ClientApiKeys.ContainsKey(userIdHeaderValue.ToString()))
        {
            context.Result = new BadRequestObjectResult($"API Key not found for user with id = {userIdHeaderValue}");
            return;
        }

        var hashString = ComputeHashString(contentString , ClientApiKeys[userIdHeaderValue.ToString()]);

        if (!hashString.Equals(digestHeaderValue))
        {
            context.Result = new BadRequestObjectResult("Invalid Request");
            return;
        }
        
        await next();
    }
};