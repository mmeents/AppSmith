using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using SharpYaml.Tokens;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
          OpenApiDiagnostic diagnostic;
          HttpResponseMessage response = await client.GetAsync(result.Url);
          response.EnsureSuccessStatusCode();
          result.RawJson = await response.Content.ReadAsStringAsync();        
          var reader = new OpenApiStringReader();          
          result.Document = reader.Read(result.RawJson, out diagnostic);

          var parsedJson = JsonSerializer.Deserialize<ExpandoObject>(result.RawJson);
          var options = new JsonSerializerOptions() { WriteIndented = true };
          result.RawJson = JsonSerializer.Serialize(parsedJson, options);           
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

    public class ParseOpenApiRequestBodyResponse {      
      public List<string> MethodParams { get; set;} = new List<string>();
    }
    public static int GetParamTypefor(this string interfaceType, Types types) {
      int ret = 85;
      string testval = interfaceType;
      if (testval.ToLower() == "int64") { 
        testval = "long";
      }
      if (testval != "integer") {
        var al = types.GetChildrenItems(79).ToArray<ItemType>();
        var tm = al.FirstOrDefault<ItemType>(x => x.Name == testval);
        if (tm == null) {
          ret = 80;
        } else {
          ret = tm.TypeId;
        }
      }
      return ret;
    }

    public static ParseOpenApiRequestBodyResponse ParseParameter( this OpenApiParameter parameter, Types types ) {
      if (parameter == null) { return null; }
      var ret = new ParseOpenApiRequestBodyResponse();
      var pin = parameter.In.ToString().ToLower();
      string attr = "";
      if (pin == "path") { attr = "[FromRoute] "; }
      if (pin == "body") { attr = "[FromBody] ";}
      if (pin == "header") { attr = "[FromHeader] ";}
      if (pin == "formData") { attr = "[FromForm] ";}
      if (pin == "query") { attr = "[FromQuery] "; }
      if (pin == "cookie") { attr = "[FromCookie] ";}

      var paramName = attr+parameter.Name;
      
      var paramSchemaType = parameter.Schema?.Type ?? "";
      var paramContent = parameter.Content.Values.FirstOrDefault();
      var paramSchemaRef = paramContent?.Schema?.Reference?.Id??"";
      string ParamType = String.IsNullOrEmpty( paramSchemaType ) ? 
        String.IsNullOrEmpty(paramSchemaRef) ? null : paramSchemaRef
        : paramSchemaType;
      if (ParamType == "integer") { 
        string fmt = paramContent?.Schema?.Format ?? "";
        if (!String.IsNullOrEmpty(fmt)) { 
          ParamType = fmt;
        }
      }
      if (ParamType == "array") { 
        ParamType = paramName.ParseLast(" ").AsUpperCaseFirstLetter()+"[]";
      }
      int mpTypeId = ParamType.GetParamTypefor(types);
      ret.MethodParams.Add($"{paramName},{mpTypeId},{ParamType}");
      return ret;
    }

    public static ParseOpenApiRequestBodyResponse ParseRequestBody( this OpenApiRequestBody item, Types types) { 
      if (item == null) return null;
      var result = new ParseOpenApiRequestBodyResponse(); 
      string bodyRef = "";
      if (item.Content.Count > 0) {
        foreach( var contentName in item.Content.Keys) { 
          var content = item.Content[contentName];
          if (content == null) continue;
          
          bodyRef = content.Schema?.Reference?.Id ?? "";
          if (!String.IsNullOrEmpty(bodyRef)) {
            if (bodyRef[0] == 'I') { bodyRef = bodyRef.Substring(1);}
            var refName = bodyRef.AsLowerCaseFirstLetter();
            int refTypeId = bodyRef.GetParamTypefor(types);
            string refType = (refTypeId == 80 ? "I"+refName.AsUpperCaseFirstLetter(): types[refTypeId].Name);
            string valToAdd = $"{refName},{refTypeId},{refType}";
            if (!result.MethodParams.Contains(valToAdd)) {
              result.MethodParams.Add(valToAdd);
            }            
          } else { 
            int propCount = content.Schema?.Properties?.Count ?? 0;
            if (propCount > 0) {
              foreach(var prop in content.Schema.Properties ) {
                if (prop.Value == null) continue;
                var theProp = prop.Value;
                string theType = theProp.Type;
                string theName = prop.Key;
                int refTypeId = theType.GetParamTypefor(types);
                string refType = (refTypeId == 80 ? "I" + theType.AsUpperCaseFirstLetter() : types[refTypeId].Name);
                string valToAdd = $"{theName},{refTypeId},{refType}";
                if (!result.MethodParams.Contains(valToAdd)) {
                  result.MethodParams.Add(valToAdd);
                }
              }
            }
          }
        }
      }     
      return result;
    }

    public static int GetMethodTypeFor(this OperationType item, Types types) {
      int ret = 66;
      string httpMethodType = Enum.GetName(typeof(OperationType), item);
      var al = types.GetChildrenItems(65).ToArray<ItemType>();
      var match = al.First<ItemType>(x => x.Name == httpMethodType);
      if (match != null) {
        ret = match.TypeId;
      }
      return ret;
    }
    public static string ParseOpenApiOpForMethod(this OpenApiOperation item, OperationType opKey, Types types) {      
      var methodName = item.OperationId;      
      int ValueTypeId = opKey.GetMethodTypeFor(types);
      string returnType = "ActionResult";
      foreach(var resp in item.Responses) { 
        if (resp.Key == "200") {
          var aResp = resp.Value;
          if ((aResp != null)&&(aResp.Content != null)) {  
            var firstKey = aResp.Content.FirstOrDefault();
            var content = firstKey.Value;            
            var tty = content.Schema?.Type?? "";
            string testReturnType = $"ActionResult";
            if (tty == "array") {
              testReturnType = content.Schema?.Items?.Reference?.Id ?? ""; 
              if (!String.IsNullOrEmpty(testReturnType)) {
                testReturnType = $"ActionResult<IEnumerable<{testReturnType}>>";
              }
            } else { 
              testReturnType = content.Schema?.Reference?.Id ?? "";   
              if ( !String.IsNullOrEmpty( testReturnType)) { 
                returnType = $"ActionResult<{testReturnType}>" ;
              }
            }
          }
        }        
      }

      return $"{returnType},{methodName},{ValueTypeId}";
    }

  }
}
