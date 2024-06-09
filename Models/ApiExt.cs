using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppSmith.Models {
   
  public enum OpenApiDocStatus { 
    Success = 0, 
    Warnings = 1,
    Error = 2
  }
  public class OpenApiDocResult { 
    public OpenApiDocResult() { }
    public string Url { get; set; }
    public string RawJson { get; set;} = "";
    public string ErrorMessage { get; set;} = "";
    public OpenApiDocStatus Status { get; set; } = OpenApiDocStatus.Success;
    public OpenApiDocument Document { get; set; }
    public OpenApiDiagnostic Diagnostic { get; set; }    
  }
  internal static class ApiExt {

    public static async Task<OpenApiDocResult> GetOpenApiDocFromSite(string baseUrl) {
      OpenApiDocResult result = new OpenApiDocResult(){Url= baseUrl };
      try {                 
        using (HttpClient client = new HttpClient()) {        
          HttpResponseMessage response = await client.GetAsync(result.Url);
          response.EnsureSuccessStatusCode();
          result.RawJson = await response.Content.ReadAsStringAsync();        
          var reader = new OpenApiStringReader();
          OpenApiDiagnostic diagnostic;
          result.Document = reader.Read(result.RawJson, out diagnostic);  
          result.Diagnostic = diagnostic;
          if (diagnostic.Warnings.Any()) {
            result.Status = OpenApiDocStatus.Warnings;
          }
          if (diagnostic.Errors.Any()) {
            result.Status = OpenApiDocStatus.Error;
          }
          return result;          
        } 
      } catch (Exception ex) {
        result.Status = OpenApiDocStatus.Error;
        result.ErrorMessage = ex.Message;
        return result;
      }
    }


  }
}
