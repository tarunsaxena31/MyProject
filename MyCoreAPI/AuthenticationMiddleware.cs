using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoreAPI
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private MasterDbContext _masterContext;
        public AuthenticationMiddleware(RequestDelegate next, MasterDbContext masterDevContext)
        {
            _next = next;
            _masterContext = masterDevContext;
        }
        public async Task Invoke(HttpContext context, EmployeeDBContext employeeDBContext)
        {
            try
            {
                string authHeader = context.Request.Headers["Authorization"];
                if (authHeader != null && authHeader.StartsWith("Basic"))
                {
                    //Extract credentials    
                    string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
                    Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                    var usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));
                    int seperatorIndex = usernamePassword.IndexOf(':');
                    var username = usernamePassword.Substring(0, 9);
                    var password = usernamePassword.Substring(seperatorIndex + 1);
                    if (true) //check if your credentials are valid    
                    {
                        //EmployeeDBContext.ConnectionString = "" ; //_masterContext.Retrive //Your subscriber connection string here    
                        if (string.IsNullOrEmpty(EmployeeDBContext.ConnectionString))
                        {
                            //no authorization header    
                            context.Response.StatusCode = 401; //Unauthorized    
                            return;
                        }
                        await _next.Invoke(context);
                    }
                    else
                    {
                        context.Response.StatusCode = 401; //Unauthorized    
                        return;
                    }
                }
                else
                {
                    // no authorization header    
                    context.Response.StatusCode = 401; //Unauthorized    
                    return;
                }
            }
            catch (Exception e)
            {
                // no authorization header    
                context.Response.StatusCode = 400;
                return;
            }
        }
    }
}
