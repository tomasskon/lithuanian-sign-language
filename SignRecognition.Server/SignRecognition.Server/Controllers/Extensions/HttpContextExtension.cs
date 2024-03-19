namespace SignRecognition.Server.Controllers.Extensions;

public static class HttpContextExtension
{
    public static string GetAuthorizationToken(this HttpContext httpContext)
    {
        if (httpContext.Request.Headers.TryGetValue("Authorization", out var values))
        {
            var jwt = values.ToString();
            return jwt.Replace("Bearer", "").Trim();
        }
        
        return string.Empty;
    }
}