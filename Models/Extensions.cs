using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace AppSmith.Models {
  public static class Ext {

    /// <summary>
    /// async read file from file system into string
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static async Task<string> ReadAllTextAsync(this string filePath) {
      using (var streamReader = new StreamReader(filePath)) {
        return await streamReader.ReadToEndAsync();
      }
    }

    /// <summary>
    /// async write content to fileName location on file system. 
    /// </summary>
    /// <param name="Content"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static async Task<int> WriteAllTextAsync(this string Content, string fileName) {
      using (var streamWriter = new StreamWriter(fileName)) {
        await streamWriter.WriteAsync(Content);
      }
      return 1;
    }
    /// <summary>
    /// Remove all instances of CToRemove from content
    /// </summary>
    /// <param name="content"></param>
    /// <param name="CToRemove"></param>
    /// <returns></returns>    
    public static string RemoveChar(this string content, char CToRemove) {
      string text = content;
      while (text.Contains(CToRemove)) {
        text = text.Remove(text.IndexOf(CToRemove), 1);
      }

      return text;
    }
    public static string MakeIndentSpace(int depth, string space ) {       
      string ret = "";
      if (depth > 0) {
        int CountDown = depth;
        while (CountDown > 0) { 
          ret += space;
          CountDown--;
        }
      }
      return ret;
    }
    /// <summary>
    ///   Splits content on each character in delims string returns string[]
    /// </summary>
    /// <param name="content"></param>
    /// <param name="delims"></param>
    /// <returns></returns>
    public static string[] Parse(this string content, string delims) {
      return content.Split(delims.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
    }

    /// <summary>
    /// Splits contents by delims and takes first string
    /// </summary>
    /// <param name="content"></param>
    /// <param name="delims"></param>
    /// <returns></returns>
    public static string ParseFirst(this string content, string delims) {
      string[] sr = content.Parse(delims);
      if (sr.Length > 0) { 
        return sr[0]; 
      }
      return "";
    }

    /// <summary>
    /// Splits contents by delims and takes last string
    /// </summary>
    /// <param name="content"></param>
    /// <param name="delims"></param>
    /// <returns></returns>
    public static string ParseLast(this string content, string delims) {
      string[] sr = content.Parse(delims);
      if (sr.Length > 0) {
        return sr[sr.Length - 1];
      }
      return "";
    }

    public static StringDict AsDict(this string content, string delims) {
      var list = content.Parse(delims);
      var ret = new StringDict();
      foreach(string item in list) { 
        ret.Add(item);
      }
      return ret;
    }

    /// <summary>
    /// general case conversion to string or empty if null
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string AsString(this object obj) {
      try {
        return Convert.ToString(obj) ?? string.Empty;
      } catch {
        return string.Empty;
      }
    }
    /// <summary>
    /// on fail or null return 0
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>int</returns>
    public static int AsInt(this string obj) {
      return int.TryParse(obj, out int r) ? r : 0;
    }
    public static int AsInt(this object obj) {
      return int.TryParse(obj.AsString(), out int r) ? r : 0;
    }
    /// <summary>
    /// byte[] to utf8 string; use AsString() to reverse
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static byte[] AsBytes(this string text) {
      return Encoding.UTF8.GetBytes(text);
    }

    /// <summary>
    /// Adds AsString support for byte[] 
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static string AsString(this byte[] bytes) {
      return Encoding.UTF8.GetString(bytes);
    }

    /// <summary>
    ///     Base 64 encodes string variant uses ? as fillers instead of = for inifiles.
    /// </summary>
    /// <param name="Text"></param>
    /// <returns></returns>
    public static string AsBase64Encoded(this string Text) {
      return Convert.ToBase64String(Encoding.UTF8.GetBytes(Text)).Replace('=', '?');
    }

    /// <summary>
    /// Base 64 decodes string variant uses converts ? back to = as fillers for inifiles.
    /// </summary>
    /// <param name="Text"></param>
    /// <returns></returns>    
    public static string AsBase64Decoded(this string Text) {
      if (string.IsNullOrEmpty(Text)) return "";
      byte[] bytes = Convert.FromBase64String(Text.Replace('?', '='));
      return Encoding.UTF8.GetString(bytes);
    }
    /// <summary>
    /// lower case first letter of content concat with remainder.
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>    
    public static string AsLowerCaseFirstLetter(this string content) {
      if (string.IsNullOrEmpty(content)) return "";
      return content.Substring(0, 1).ToLower() + content.Substring(1);
    }
    /// <summary>
    /// Uppercase first letter of content concat with rest of content.
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public static string AsUpperCaseFirstLetter(this string content) {
      if (string.IsNullOrEmpty(content)) return "";
      return content.Substring(0, 1).ToUpper() + content.Substring(1);
    }


  }

  public static class Cs {
    public static string nl { get { return Environment.NewLine; } }

    public static string GetNamespaceText(string sDB) {
      return
        "using Dapper;  // data io is based on Dapper." + nl +
        "using System.Data;" + nl +
      //  "using System.Data.SqlClient;  // from nuget as well."+nl+
      //  "using StaticExtensions;  // see StaticExtensions in nuget."+nl+nl+
       $"namespace {sDB}" + "{" + nl + nl;
    }

    public static string SQLDefNullValueSQL(string sqlType) {
      string w = sqlType.ToLower().ParseFirst(" ()");
      string result = "";
      if (w == "char") result = "''";
      else if (w == "varchar") result = "''";
      else if (w == "int") result = "0";
      else if (w == "bigint") result = "0";
      else if (w == "binary") result = "null";
      else if (w == "bit") result = "0";
      else if (w == "datetime") result = "null";
      else if (w == "decimal") result = "0.0";
      else if (w == "float") result = "0.0";
      else if (w == "image") result = "null";
      else if (w == "money") result = "0.0";
      else if (w == "numeric") result = "0.0";
      else if (w == "nchar") result = "''";
      else if (w == "ntext") result = "''";
      else if (w == "nvarchar") result = "''";
      else if (w == "real") result = "0.0";
      else if (w == "smallint") result = "0";
      else if (w == "smallmoney") result = "0.0";
      else if (w == "smalldatetime") result = "null";
      else if (w == "text") result = "''";
      else if (w == "timestamp") result = "null";
      else if (w == "tinyint") result = "0";
      else if (w == "uniqueidentifier") result = "''";
      else if (w == "varbinary") result = "null";
      return result;
    }

    public static string GetCTypeFromSQLType(string sqlType) {
      string w = sqlType.ToLower().ParseFirst(" ()");
      string result = "";
      if (w == "char") result = "string ";
      else if (w == "varchar") result = "string";
      else if (w == "int") result = "int";
      else if (w == "bigint") result = "long";
      else if (w == "binary") result = "byte";
      else if (w == "bit") result = "bool";
      else if (w == "datetime") result = "DateTime";
      else if (w == "decimal") result = "decimal";
      else if (w == "float") result = "float";
      else if (w == "image") result = "Image";
      else if (w == "money") result = "decimal";
      else if (w == "numeric") result = "decimal";
      else if (w == "nchar") result = "byte";
      else if (w == "ntext") result = "string";
      else if (w == "nvarchar") result = "string";
      else if (w == "real") result = "decimal";
      else if (w == "smallint") result = "short";
      else if (w == "smallmoney") result = "decimal";
      else if (w == "smalldatetime") result = "DateTime";
      else if (w == "text") result = "string";
      else if (w == "timestamp") result = "DateTime";
      else if (w == "tinyint") result = "short";
      else if (w == "uniqueidentifier") result = "string";
      else if (w == "varbinary") result = "byte";
      return result;
    }

    public static string GetColumnTypeFromSQLType(string sqlType) {
      string w = sqlType.ToLower().ParseFirst(" ()");
      string result = "";
      if (w == "char") result = "String ";
      else if (w == "varchar") result = "String";
      else if (w == "int") result = "Int32";
      else if (w == "bigint") result = "Int64";
      else if (w == "binary") result = "Byte";
      else if (w == "bit") result = "Boolean";
      else if (w == "datetime") result = "DateTime";
      else if (w == "decimal") result = "Decimal";
      else if (w == "float") result = "Decimal";
      else if (w == "image") result = "Image";
      else if (w == "money") result = "Decimal";
      else if (w == "numeric") result = "Decimal";
      else if (w == "nchar") result = "Byte";
      else if (w == "ntext") result = "String";
      else if (w == "nvarchar") result = "String";
      else if (w == "real") result = "Decimal";
      else if (w == "smallint") result = "Int32";
      else if (w == "smallmoney") result = "Decimal";
      else if (w == "smalldatetime") result = "DateTime";
      else if (w == "text") result = "String";
      else if (w == "timestamp") result = "DateTime";
      else if (w == "tinyint") result = "Int32";
      else if (w == "uniqueidentifier") result = "String";
      else if (w == "varbinary") result = "Byte";
      return result;
    }

    public static string GetToTypeFromStringValue(string sqlType) {
      string w = sqlType.ToLower().ParseFirst(" ()");
      string result = "";
      if (w == "char") result = "";
      else if (w == "varchar") result = "";
      else if (w == "int") result = ".AsInt32()";
      else if (w == "bigint") result = ".AsInt64()";
      else if (w == "binary") result = ".AsByte()";
      else if (w == "bit") result = ".AsBoolean()";
      else if (w == "datetime") result = ".AsDateTime()";
      else if (w == "decimal") result = ".AsDecimal()";
      else if (w == "float") result = ".AsDecimal()";
      //else if (w == "image") result = "AsImage()";
      else if (w == "money") result = ".AsDecimal()";
      else if (w == "numeric") result = ".AsDecimal()";
      else if (w == "nchar") result = ".AsByte()";
      else if (w == "ntext") result = "";
      else if (w == "nvarchar") result = "";
      else if (w == "real") result = ".AsDecimal()";
      else if (w == "smallint") result = ".AsInt32()";
      else if (w == "smallmoney") result = ".AsDecimal()";
      else if (w == "smalldatetime") result = ".AsDateTime()";
      else if (w == "text") result = "";
      else if (w == "timestamp") result = ".AsDateTime()";
      else if (w == "tinyint") result = ".AsInt32()";
      else if (w == "uniqueidentifier") result = "";
      else if (w == "varbinary") result = ".AsByte()";
      return result;
    }

    public static string SQLDefNullValueCSharp(string sqlType) {
      string w = sqlType.ToLower().ParseFirst(" ()");
      string result = "";
      if (w == "char") result = "\"\"";
      else if (w == "varchar") result = "\"\"";
      else if (w == "int") result = "0";
      else if (w == "bigint") result = "0";
      else if (w == "binary") result = "null";
      else if (w == "bit") result = "false";
      else if (w == "datetime") result = "null";
      else if (w == "decimal") result = "0.0";
      else if (w == "float") result = "0.0";
      else if (w == "image") result = "null";
      else if (w == "money") result = "0.0";
      else if (w == "numeric") result = "0.0";
      else if (w == "nchar") result = "\"\"";
      else if (w == "ntext") result = "\"\"";
      else if (w == "nvarchar") result = "\"\"";
      else if (w == "real") result = "0.0";
      else if (w == "smallint") result = "0";
      else if (w == "smallmoney") result = "0.0";
      else if (w == "smalldatetime") result = "null";
      else if (w == "text") result = "\"\"";
      else if (w == "timestamp") result = "null";
      else if (w == "tinyint") result = "0";
      else if (w == "uniqueidentifier") result = "\"\"";
      else if (w == "varbinary") result = "null";
      return result;
    }

  }

  public static class Ic {
    public static string GenerateApi(this Item tnApi, Types types) { 
      StringBuilder res = new StringBuilder();
      res.AppendLine($"namespace {tnApi.Text} {{ ");
      foreach(Item tnController in tnApi.Nodes) {
        if (tnController.TypeId == (int)TnType.Controller) {
          res.AppendLine(tnController.GenerateController(types, false));
        } else {
          res.AppendLine(tnController.GenerateClass(types, false));
        }        
      }
      res.AppendLine($"}} ");
      return res.ToString();
    }
    public static string GenerateMethodParam(this Item tnMethodParam, Types types, bool ForIntf) {
       int tId = tnMethodParam.ValueTypeId;
       string paramType = "";
       if (( tId == 80) || (tId == 81)) { 
         paramType = tnMethodParam.Code;
       } else { 
         paramType = types[tId].Name;
       }
      string ParamName = tnMethodParam.Text;
      string ParamAttrb = "";
      if (ParamName.Parse(" ").Length > 1) {
        ParamName = ParamName.ParseLast(" ");
        if (!ForIntf) { 
          ParamAttrb = tnMethodParam.Text.Substring(0, tnMethodParam.Text.Length - ParamName.Length).Trim()+" ";
        }
      }
      return ParamAttrb+ paramType+" "+ ParamName;
    }

    public static string GenerateAccessibility(this Item tnClass, Types types) {
      var sa = tnClass.Code.Parse(",");
      if ((sa == null) || (sa.Length != 7)) {
        sa = "false,false,false,false,false,NULL,74".Parse(",");
      }
      bool isAsync = bool.Parse(sa[0]);
      bool isVirtual = bool.Parse(sa[1]);
      bool isStatic = bool.Parse(sa[2]);
      bool isAbstract = bool.Parse(sa[3]);
      bool isSealed = bool.Parse(sa[4]);
      int Access =  int.TryParse(sa[6], out int iAccess) ? iAccess: 0;
      string baseAccess = (Access > 0)  ? types[Access].Desc : "";
      string AccessibilityClause = $"{baseAccess} {(isAsync ? "async " : "")}{(isVirtual ? "virtual " : "")}{(isStatic ? "static " : "")}{(isAbstract ? "abstract " : "")}{(isSealed ? "sealed " : "")}";
      return AccessibilityClause;
    }
    public static string GenerateBaseType(this Item tnClass) {
      var sa = tnClass.Code.Parse(",");
      if ((sa == null) || (sa.Length != 7)) {
        sa = "false,false,false,false,false,NULL,74".Parse(",");
      }
      string baseType = sa[5] == "NULL" ? "" : sa[5];
      return sa[5] == "NULL" ? "" : sa[5]+" ";
    }

    public static string GenerateControllerIntf(this Item tnController, Types types, bool incluedNameSpace) {
      StringBuilder res = new StringBuilder();
      string className = "I" + tnController.Text;
      string ver = tnController.ValueTypeSize;
      string AccessibilityClause = tnController.GenerateAccessibility(types);
      string baseType = tnController.GenerateBaseType().Trim();
      if ((baseType == className) || (baseType == "object")) baseType = "";
      baseType = string.IsNullOrWhiteSpace(baseType)? "" : " : "+baseType;

      if (incluedNameSpace) {
        res.AppendLine($"namespace {tnController.Parent.Text} {{ ");
      }      
      res.AppendLine($"{Cs.nl}    {AccessibilityClause}interface {className}{baseType}{Cs.nl}    {{"); // begin controller class      
      foreach (Item tnMethod in tnController.Nodes) {                                        // Methods 
        if (tnMethod.TypeId == (int)TnType.Property) {
          string ac = tnMethod.GenerateAccessibility(types).Trim();
          string bt = tnMethod.GenerateBaseType();
          if (ac.Length>0 && ac.Parse(" ")[0] == "public") { 
            ac = ac + " ";
            res.AppendLine("        " + ac + bt + " " + tnMethod.Text + ";");
          }
        }
      }
      foreach (Item tnMethod in tnController.Nodes) {
        if (tnMethod.TypeId == (int)TnType.Method) {
          string ac = tnMethod.GenerateAccessibility(types).Trim();
          if (ac.Length > 0 && ac.Parse(" ")[0] == "public") {
            res.Append(tnMethod.GenerateMethodIntf(types));
          }
        }
      }
      res.AppendLine($"    }}");  // end controller class
      if (incluedNameSpace) {
        res.AppendLine($"}}");
      }
      return res.ToString();
    }
    public static string GenerateController(this Item tnController, Types types, bool incluedNameSpace) {
      StringBuilder res = new StringBuilder();
      string className = tnController.Text;
      string ver = tnController.ValueTypeSize.ParseFirst(",");
      string AccessibilityClause = tnController.GenerateAccessibility(types);
      string baseType = tnController.GenerateBaseType();
      if ((baseType == className) || (baseType == "object")) baseType = "";
      baseType = string.IsNullOrWhiteSpace(baseType) ? "" : " : " + baseType;
      if (incluedNameSpace) {
        res.AppendLine($"namespace {tnController.Parent.Text} {{ ");
      }
      res.AppendLine(tnController.GenerateControllerIntf(types, false));
      res.AppendLine( "    [Route(\"api/[controller]\")]");
      res.AppendLine( "    [ApiController]");
      res.AppendLine($"    [ApiVersion(\"{ver}\")]");
      res.AppendLine($"    [Authorize]");
      res.AppendLine($"    {AccessibilityClause}class {className}{baseType}{Cs.nl}    {{"); // begin controller class      
      foreach (Item tnMethod in tnController.Nodes) {                                        // Methods 
        if (tnMethod.TypeId == (int)TnType.Property) {
          string ac = tnMethod.GenerateAccessibility(types);
          string bt = tnMethod.GenerateBaseType();
          res.AppendLine("        "+ac + bt + " " + tnMethod.Text+";");
        }
      }
      foreach (Item tnMethod in tnController.Nodes) {
        if (tnMethod.TypeId == (int)TnType.Method) {
          res.Append(tnMethod.GenerateMethod(types)); 
        }
      }
      res.AppendLine($"    }}");  // end controller class
      if (incluedNameSpace) {
        res.AppendLine($"}}");
      }
      return res.ToString();
    }
    

    public static string GenerateClass(this Item tnClass, Types types, bool incluedNameSpace) {
      StringBuilder res = new StringBuilder();
      string className = tnClass.Text;
      string ver = tnClass.ValueTypeSize;      
      string AccessibilityClause = tnClass.GenerateAccessibility(types);
      string baseType = tnClass.GenerateBaseType();
      if ((baseType == className) || (baseType == "object")) baseType ="";
      baseType = string.IsNullOrWhiteSpace(baseType) ? "" : " : " + baseType;
      if (incluedNameSpace) {
        if (tnClass.Parent != null) { 
          res.AppendLine($"namespace {tnClass.Parent.Text} {{ ");
        }
      }
      res.AppendLine(tnClass.GenerateControllerIntf(types, false));
      res.AppendLine($"    {AccessibilityClause}class {className}{baseType}{Cs.nl}    {{"); // begin class
      foreach (Item tnMethod in tnClass.Nodes) {                                        // Methods 
        if (tnMethod.TypeId == (int)TnType.Property) {
          string ac = tnMethod.GenerateAccessibility(types);
          string bt = tnMethod.GenerateBaseType();
          res.AppendLine("        " + ac + bt + " " + tnMethod.Text+";");
        }        
      }
      foreach (Item tnMethod in tnClass.Nodes) {
        if (tnMethod.TypeId == (int)TnType.Method) {
          res.AppendLine(tnMethod.GenerateMethod(types));
        }
      }

      res.AppendLine($"    }}");  // end class
      if (incluedNameSpace) {
        res.AppendLine($"}}");
      }
      return res.ToString();
    }
    public static string GenerateMethodIntf(this Item tnMethod, Types types) {
      StringBuilder res = new StringBuilder();
      string ac = tnMethod.GenerateAccessibility(types).Trim();
      string bt = tnMethod.GenerateBaseType();
      Item ParentItem = (Item)tnMethod.Parent;
      bool drawRounts = (ParentItem.TypeId == (int)TnType.Controller);
      string msgParams = "";
      foreach (Item tnParam in tnMethod.Nodes) {
        if (tnParam.TypeId == (int)TnType.MethodParam) {          
          msgParams = msgParams + ((msgParams == "") ? tnParam.GenerateMethodParam(types,true) : ", " + tnParam.GenerateMethodParam(types, true));
        }
      }
      if (bt.Trim() == tnMethod.Text.Trim()) { // in case of constructor, method name and type are same. 
        bt = "";
      }
      string MethodName = tnMethod.Text;      
      if (ac.Length > 0 && ac.Parse(" ")[0] == "public") {
        res.AppendLine($"        {ac} {bt}{MethodName}({msgParams});");
      }
      return res.ToString();
    }
    public static string GenerateMethod(this Item tnMethod, Types types) {
      StringBuilder res = new StringBuilder();
      string ac = tnMethod.GenerateAccessibility(types);
      string bt = tnMethod.GenerateBaseType();
      Item ParentItem = (Item)tnMethod.Parent;
      bool drawRounts = (ParentItem.TypeId == (int)TnType.Controller);
      if (drawRounts) {
        res.AppendLine($"        [Route(\"{tnMethod.ValueTypeSize.ParseLast(",")}\", Name = \"{tnMethod.Text.AsUpperCaseFirstLetter()}\")]");
        if (tnMethod.ValueTypeId != 0){ 
          string decor = types[tnMethod.ValueTypeId].Desc;
          res.AppendLine($"        {decor}");
        }
      }
      string msgParams = "";
      foreach (Item tnParam in tnMethod.Nodes) {
        if (tnParam.TypeId == (int)TnType.MethodParam) {
          msgParams = msgParams + ((msgParams=="") ? tnParam.GenerateMethodParam(types, false) : ", "+tnParam.GenerateMethodParam(types, false));
        }
      }
      if (bt.Trim() == tnMethod.Text.Trim()) { // in case of constructor, method name and type are same. 
        bt = "";
      }
      res.AppendLine($"        {ac}{bt}{tnMethod.Text}({msgParams}) {{  }}"); 
      return res.ToString(); 
    }
    public static string GenerateSqlCreateTable(this Item tnTable, Types types) {
      string r = $"-- a table create {Cs.nl}Create Table {tnTable.Text}({Cs.nl}";
      bool hasId = false; bool ftt = true;
      string identityColName = "";
      foreach (Item tn in tnTable.Nodes) {
        string src = tn?.Code ?? "";
        bool isIdentity = (src.IndexOf("IDENTITY", StringComparison.CurrentCultureIgnoreCase) >= 0);
        if (isIdentity && !hasId) hasId = true;
        if (isIdentity) {
          identityColName = tn.Text;
        }
        bool isNotNull = (src.IndexOf("NOT NULL", StringComparison.CurrentCultureIgnoreCase) >=0);
        if (tn.Text.ToLower() == "id") {          
          r += (!ftt ? "," + Cs.nl : "") + 
            "    " + tn.Text +" "+ types[tn.ValueTypeId].Name + tn.ValueTypeSize+" NOT NULL IDENTITY(1,1)";
          hasId = true;
        } else {
          r += (!ftt ? "," + Cs.nl: "")+ 
            "    " + tn.Text + " " + types[tn.ValueTypeId].Name+ tn.ValueTypeSize + (isNotNull ? " NOT" : "") + " NULL"+(isIdentity? " IDENTITY(1,1)" : "");          
        }
        if (ftt) ftt = false;
      }
      if (hasId) { 
        r += "," + Cs.nl+$"    CONSTRAINT [PK_{tnTable.Text.RemoveChar('.')}_{identityColName}] PRIMARY KEY CLUSTERED ([{identityColName}])";
      }
      return r+ $"{Cs.nl})";
    }
          
    public static string GenerateSQLAddUpdateStoredProc(this Item tnTable, Types types) {
      Item cn = tnTable;
      string nl = Environment.NewLine;
      string tblText = tnTable.Text;
      string tblTitle = tnTable.Text.RemoveChar('.');
      string sSQLParam1 = tnTable.GetSQLParamList(types);
      string sInsertListAsSQlParams = tnTable.GetSQLInsertListAsSQLParam();
      string sColList = tnTable.GetSQLColumnList(false);
      string sAssignColSQL = GetAssignChildSQLColList(tnTable);
      string sKeyType = "", sKey = "", sKeyB = "";
      Item keyCol;
      if (tnTable.Nodes.Count > 0) {
        keyCol = (Item)tnTable.Nodes[0];
        sKey = keyCol.Text;
        sKeyB = "[" + sKey + "]";        
        sKeyType = types[keyCol.ValueTypeId].Name+keyCol.ValueTypeSize;
      }
      string sDefNullValue = Cs.SQLDefNullValueSQL(sKeyType);
      return
        "-- Add Update SQL Stored Proc for " + tblText + "" + nl +
        "Create Procedure dbo.sp_AddUpdate" + tblTitle + " (" + nl +
        "  " + sSQLParam1 + nl +
        ") as " + nl +
        "  set nocount on " + nl +
       $"  declare @a {sKeyType} set @a = case when (@{sKey}=0) then 0 else isnull((select " + sKeyB + " from " + tblText +
          " with(nolock) where " + sKeyB + " = @" + sKey + "), " + sDefNullValue + ") end  " + nl +
        "  if (@a = " + sDefNullValue + ") begin" + nl +
        "    Insert into " + tblText + " (" + nl +
        "      " + sColList + nl +
        "    ) values (" + nl + "      " + sInsertListAsSQlParams + ")" + nl +
        "    set @a = @@Identity " + nl +
        "  end else begin" + nl +
        "    Update " + tblText +
             " set" + nl + sAssignColSQL + nl +
          "    where " + sKeyB + " = @a" + nl +
        "  end" + nl +
        "  select @a " + sKey + nl + "return";
    }

    public static string GenerateSQLStoredProc(this Item tnProc, Types types) {
      Item cn = tnProc;
      string tblText = tnProc.Text;
      string sSQLParam1 = tnProc.GetSQLParamList(types);
      string sSqlDeclare = tnProc.GetDeclareSQLParam(types);      
      string sSqlBody = tnProc.GetStProcBody();
      //sSqlBody = Formatter.Format(sSqlBody);
      return        
        "Create Procedure " + tblText + " (" + Cs.nl +
        "  " + sSQLParam1 + Cs.nl +
        ") as " + Cs.nl +
        sSqlBody + Cs.nl + Cs.nl +
        "-- call to execute" + Cs.nl +
        sSqlDeclare + Cs.nl +
        $"Exec {tnProc.Text} {tnProc.GetSQLInsertListAsSQLParam(true)}";
    }

    public static string GenerateSQLFunction(this Item tnFunction, Types types) {
      Item cn = tnFunction;
      string tblText = tnFunction.Text;
      string sSQLParam1 = tnFunction.GetSQLParamList(types);
      return
        "-- Add Update SQL Stored Proc for " + tblText + "" + Cs.nl +
        "Create Function " + tblText + " (" + Cs.nl +
        "  " + sSQLParam1 + Cs.nl +
        ") as " + Cs.nl +
        "  " + Cs.nl + Cs.nl +
        "return";
    }

    public static string GetSQLParamList(this Item tnTable, Types types) {
      string sRes = "";
      foreach (Item tnColumn in tnTable.Nodes) {
        if (tnColumn.TypeId != (int)TnType.ProcBody) { 
          var paramName = "@" + tnColumn.Text.RemoveChar('@');
          if (sRes == "") {
            sRes = paramName + $" {types[tnColumn.ValueTypeId].Name}{tnColumn.ValueTypeSize}";
          } else {
            sRes = sRes + "," + Environment.NewLine + "  " + paramName + $" {types[tnColumn.ValueTypeId].Name}{tnColumn.ValueTypeSize}";
          }
        }
      }
      return sRes;
    }

    public static string GetSQLInsertListAsSQLParam(this Item tnTable, bool IncludeFirstCol = false) {
      string sRes = ""; string sFTT = "true";
      foreach (Item tn in tnTable.Nodes) {
        var paramName = "@"+tn.Text.RemoveChar('@');
        if (sFTT == "true") {
          sFTT = "false";
          if (IncludeFirstCol) {
            sRes = paramName;
          }
        } else {
          if (sRes == "") {
            sRes = paramName;
          } else {
            sRes = sRes + ", " + paramName;
          }
        }
      }
      return sRes;
    }

    public static string GetSQLColumnList(this Item tnTable, Boolean IncludeKeyField) {
      string sRes = ""; string sFTT = (IncludeKeyField ? "false" : "true");
      foreach (Item tn in tnTable.Nodes) {
        if (sFTT == "true") {
          sFTT = "false";  // don't include the first Keyfield.
        } else {
          if (sRes == "") {
            sRes = "[" + tn.Text + "]";
          } else {
            sRes = sRes + ", [" + tn.Text + "]";
          }
        }
      }
      return sRes;
    }

    public static string GetAssignChildSQLColList(this Item tnTable) {
      string sRes = ""; string sFTT = "true";
      foreach (Item tn in tnTable.Nodes) {
        string sCurCol = tn.Text;
        if (sFTT == "true") {
          sFTT = "false";
        } else {
          if (sRes == "") {
            sRes = "      [" + sCurCol + "] = @" + sCurCol;
          } else {
            sRes = sRes + "," + Environment.NewLine + "      [" + sCurCol + "] = @" + sCurCol;
          }
        }
      }
      return sRes;
    }

    public static string GetDeclareSQLParam(this Item tnStProc) {
      string s = "";
      foreach (Item cn in tnStProc.Nodes) {
        s = s + "declare " + cn.Text + " set " + cn.Text.ParseFirst(" ") + " = " + Cs.SQLDefNullValueSQL(cn.Text.ParseLast(" ")) + ";" + Cs.nl;
      }
      return s;
    }
    public static string GetExecSQLStoredProcedure(this Item tnStProc) {
      return "--  the call to execute " + Cs.nl
        + GetDeclareSQLParam(tnStProc) + Cs.nl
        + $"Exec {tnStProc.Text} {tnStProc.GetSQLInsertListAsSQLParam(true)}";
    }

    public static string GetCSharpColAsProps(this Item cn, Types types) {
      string sRes = "";
      string nl = Environment.NewLine;
      foreach (Item tn in cn.Nodes) {
        string scol = tn.Text.AsUpperCaseFirstLetter();
        sRes = sRes + $"    public {Cs.GetCTypeFromSQLType(types[tn.ValueTypeId].Name+tn.ValueTypeSize)} {scol}" + "{get; set;} = " + 
          Cs.SQLDefNullValueCSharp(types[tn.ValueTypeId].Name + tn.ValueTypeSize) + ";" + nl;
      }
      return sRes;
    }

    public static string GenerateCSharpRepoLikeClassFromTable(this Item tnTable, Types types, bool IncludeNamespace = true) {
      Item cn = tnTable;
      string tblName = cn.Text;
      String sDB = cn.Parent.Parent.Text;
      string sColListb = cn.GetSQLColumnList(true);
      string nl = Environment.NewLine;
      string sFirstCol;
      string sKeyType = "", sKey = "";
      if (cn.Nodes.Count > 0) {
        Item keyCol = (Item)cn.Nodes[0];
        sFirstCol = keyCol.Text;
        sKey = sFirstCol.AsLowerCaseFirstLetter();
        sKeyType = Cs.GetCTypeFromSQLType(types[keyCol.ValueTypeId].Desc);
      }
      string a = "";
      string d = "";
      string t = "";
      string ar = "";
      string art = "";
      string b = "";
      string pU;
      string pL;
      string sqlT;
      for (Int32 i = 0; i < tnTable.Nodes.Count; i++) {
        Item iI = (Item)tnTable.Nodes[i];
        pU = iI.Text.AsUpperCaseFirstLetter();
        pL = iI.Text.AsLowerCaseFirstLetter();
        sqlT = types[iI.ValueTypeId].Desc;
        a = a + (a == "" ? "" : ", ") + $"{pU} = {pL}";
        d = d + (d == "" ? "" : ", ") + $"{Cs.GetCTypeFromSQLType(sqlT)} {pL}";
        t = t + (t == "" ? "" : Environment.NewLine) + $"        _table.AddColumn(\"{pU}\", ColumnType.{Cs.GetColumnTypeFromSQLType(sqlT)});";
        art = Cs.GetColumnTypeFromSQLType(sqlT);
        art = art == "String" ? "" : ".AsString()";
        ar = ar + (ar == "" ? "" : Environment.NewLine) + $"      _table.Rows[RowKey][\"{pU}\"].Value = item.{pU}{art};";        
        b = b + (b =="" ? "" : ","+Environment.NewLine)     + $"          {pU} = _table.Rows[RowKey][\"{pU}\"].Value{Cs.GetToTypeFromStringValue(sqlT)}";
      }
      string className = tblName.RemoveChar('.').AsUpperCaseFirstLetter();
      string classVarName = className.AsLowerCaseFirstLetter();
      return
        (IncludeNamespace ? Cs.GetNamespaceText(sDB) : "") +
        "  // C Entity Class " + nl +
       $"  public class {className}" + "{" + nl +
       $"    public {tblName.ParseLast(".")}()" + "{}" + nl +
               cn.GetCSharpColAsProps(types) +
        "  }" + nl + nl +
       $"  public class {className}Repository" + "{" + nl +
       $"    // C Dapper Load List of {className}" + nl +
       $"    public async Task<ActionResult> {className}IndexAsync()" + "{" + nl +
       $"      IEnumerable<{className}> result;" + nl +
        "      string connectionString = GetConnectionString();" + nl +
        "      using (SqlConnection connection = new SqlConnection(connectionString)) {" + nl +
       $"        result = await connection.QueryAsync<{className}>(" + nl +
       $"          \"select {sColListb} from {tblName} \");" + nl +
        "      }" + nl +
        "      return Ok(result);" + nl +
        "    }" + nl + nl +
        "    // C Dapper Load single Item" + nl +
       $"    public async Task<ActionResult> {className}ItemAsync({sKeyType} {sKey})" + "{" + nl +
        "      string connectionString = GetConnectionString();" + nl +
       $"      {className} result;" + nl +
        "      using (SqlConnection connection = new SqlConnection(connectionString)) {" + nl +
        "        var param = new {" + sKey + "};" + nl +
       $"        result = await connection.QueryFirstOrDefaultAsync<{className}>(" + nl +
       $"          \"select {sColListb} from {tblName} where {sKey} = @{sKey} \", param);" + nl +
        "      }" + nl +
        "      return Ok($\"{result.AsJson()}\");" + nl +
        "    }" + nl + nl +
        "    // C Dapper Edit via Add Update stored procdure" + nl +
       $"    public async Task<int> {className}EditAsync({className} {classVarName}) " + "{" + nl +
        "      string connectionString = GetConnectionString();" + nl +
       $"      int result = 0;" + nl +
        "      using (SqlConnection connection = new SqlConnection(connectionString)) {" + nl +
       $"        result = await connection.QueryFirstOrDefaultAsync<int>(" + nl +
       $"          \"dbo.sp_AddUpdate{className}\", {classVarName}, commandType: CommandType.StoredProcedure);" + nl +
        "      }" + nl +
        "      return result;" + nl +
        "    }" + nl + nl +
        "    // C Dapper Edit via Add Update stored procdure x2" + nl +
       $"    public async Task<int> {className}Edit2Async({d}) " + "{" + nl +
        "      string connectionString = GetConnectionString();" + nl +
       $"      int result = 0;" + nl +
        "      using (SqlConnection connection = new SqlConnection(connectionString)) {" + nl +
       $"        {className} {classVarName} = new {className}(){{ {a} }};" + nl +
       $"        result = await connection.QueryFirstOrDefaultAsync<int>(" + nl +
       $"          \"dbo.sp_AddUpdate{className}\", {classVarName}, commandType: CommandType.StoredProcedure);" + nl +
        "      }" + nl +
        "      return result;" + nl +
        "    }" + nl + nl +
        "    // C Dapper delete via Execute" + nl +
       $"    public async Task<ActionResult> {className}DeleteAsync({sKeyType}{sKey})" + "{" + nl +
        "      int result = 0;" + nl +
        "      string connectionString = GetConnectionString();" + nl +
        "      var param = new {" + $"{sKey}" + "};" + nl +
        "      using (SqlConnection connection = new SqlConnection(connectionString)) {" + nl +
        "        result = await connection.ExecuteAsync(" + nl +
        "          \"delete from " + $"{tblName} where [{sKey}] = @{sKey} " + "\", param);" + nl +
        "      }" + nl +
        "      return Ok(result == 1);" + nl +
        "    }" + nl +
        "  }" + nl + nl + 
       $"  public class {className}FileTable" + "{" + nl +
        "    private readonly FileTable _table;"+nl+
        "    public Columns Columns { get { return _table.Columns; }}"+ nl +
        "    public Rows Rows { get { return _table.Rows; }}" + nl +
       $"    public {className}FileTable(string fileName)"+"{"+nl+
        "      _table = new FileTable(fileName); " + nl +
        "      _table.Active = true;" + nl +
        "      if (_table.Columns.Count()==0){" + nl +     
        t+ nl +
        "      }" + nl +
        "    }" + nl +
       $"    public {className}? Get(int id)"+"{"+nl+
        "      if(_table.Rows.Contains(id)){"+nl+
       $"        return new {className}()" + "{" + nl + b +nl+
        "        }"+nl+
        "      } else { return null; }"+nl+
        "    }"+nl+
       $"    public void Insert({className} item)"+"{" + nl +
        "      int RowKey = _table.AddRow();" + nl +
        ar+ nl +
        "      _table.Save();" + nl +
        "    }" + nl +
       $"    public void Update({className} item)" + "{" + nl +
        "      int RowKey = item.Id;" + nl + 
        ar + nl +
        "      _table.Save();" + nl +
        "    }" + nl +
       $"    public void Delete({className} item)" + "{" + nl +
        "      int RowKey = item.Id;" + nl +
        "      _table.Rows.Remove(RowKey, out Row? _);" + nl +
        "      _table.Save();" + nl +
        "    }" + nl +
     //   "      " + nl +

       "  }" + nl + nl +

        (IncludeNamespace ? "}" : "");


    }

    public static string GenerateCSharpExecStoredProc(this Item tnStProc, Types types) {
      string sDBName = tnStProc.Parent.Text.ParseFirst(":");
      string a = "";
      string d = "";
      for (Int32 i = 0; i < tnStProc.Nodes.Count; i++) {
        if (((Item)tnStProc.Nodes[i]).TypeId != (int)TnType.ProcBody) {
          a = a + (a == "" ? "" : ", ") + tnStProc.Nodes[i].Text.ParseFirst(" @");
          d = d + (d == "" ? "" : ", ") + $"{Cs.GetCTypeFromSQLType(types[((Item)tnStProc.Nodes[i]).ValueTypeId].Desc)} {tnStProc.Nodes[i].Text.ParseFirst(" @")}";
        }
      }
      string className = tnStProc.Text.ParseLast(".").AsUpperCaseFirstLetter();
      var s = "" + Cs.nl +
        $"    public async Task<ActionResult> Exec{className}Async({d}) " + "{" + Cs.nl +
        $"      string connectionString = Settings.GetConnectionString(\"{sDBName}\");" + Cs.nl +
        $"      {className}Result result;" + Cs.nl +
         "      var params = new {" + $"{a}" + "};" + Cs.nl +
         "      using (SqlConnection connection = new SqlConnection(connectionString)) {" + Cs.nl +
        $"        result = await connection.QueryAsync<{className}Result>(\"{tnStProc.Text.ParseFirst(" ")}\", params, commandType: CommandType.StoredProcedure);" + Cs.nl +
         "      }" + Cs.nl +
         "      return Ok(result.ToJson());" + Cs.nl +
         "    }" + Cs.nl + Cs.nl;
      return s;
    }

    public static string GetDeclareSQLParam(this Item tnStProc, Types types) {
      string s = "";
      foreach (Item cn in tnStProc.Nodes) {
        if (cn.TypeId != (int)TnType.ProcBody) {
          s = s + $"declare @{cn.Text.RemoveChar('@')} {types[cn.ValueTypeId].Name}{cn.ValueTypeSize} set @" + cn.Text.RemoveChar('@') + " = " + Cs.SQLDefNullValueSQL(types[cn.ValueTypeId].Desc) + ";" + Cs.nl;
        }
      }
      return s;
    }

    public static string GetStProcBody(this Item tnStProc) { 
      string ret = "";
      foreach(TreeNode x in tnStProc.Nodes) { 
        Item child = x as Item;
        if ((child != null)&&(child.TypeId==(int)TnType.ProcBody)) {
          ret = ret + child.Code;
        }
      }      
      return ret.Replace(" ,", ",").Replace("( ", "(").Replace(" (", "(").Replace(" )", ")").Replace(") ", ")");
    }

    public static string GetSQLDeclareVarColList(this Item rn, Types types) {
      string sReturn = "";
      foreach (Item cn in rn.Nodes) {
        if (sReturn == "") {
          sReturn = "  Declare @a" + cn.Text.ParseFirst(" ") + " " + types[cn.ValueTypeId].Name+cn.ValueTypeSize;
        } else {
          sReturn = sReturn + Environment.NewLine + "  Declare @a" + cn.Text.ParseFirst(" ") + " " + types[cn.ValueTypeId].Name + cn.ValueTypeSize;
        }
      }
      return sReturn;
    }

    public static string GetSQLColumnVarList(this Item rn, Types types) {
      string sReturn = "";
      foreach (Item cn in rn.Nodes) {
        if (sReturn == "") {
          sReturn = "@a" + cn.Text;
        } else {
          sReturn = sReturn + ", @a" + cn.Text;
        }
      }
      return sReturn;
    }
    public static string GetSQLCursor(this Item tnTable, Types types) {
      string sSQLDeclareList = tnTable.GetSQLDeclareVarColList(types);
      string sSQLColumnList = tnTable.GetSQLColumnList(true);
      string sSQLColumnVarList = tnTable.GetSQLColumnVarList(types);
      string sObjName = tnTable.Text.RemoveChar('.').AsUpperCaseFirstLetter();
      return Environment.NewLine + Environment.NewLine + "--  SQL Stored Proc dbo.sp_Foreach" + sObjName + " Cursor iterater" + Environment.NewLine +
        "Create Procedure dbo.sp_Foreach" + sObjName + "() as begin " + Environment.NewLine +
           sSQLDeclareList + Environment.NewLine +
        "  declare aCur cursor local fast_forward for " + Environment.NewLine +
        "  select " + sSQLColumnList + Environment.NewLine +
        "    from " + tnTable.Text + Environment.NewLine +
        "  open aCur fetch aCur into " + Environment.NewLine +
        "    " + sSQLColumnVarList + Environment.NewLine +
        "  while @@fetch_status = 0 begin " + Environment.NewLine +
        "    " + Environment.NewLine +
        "    " + Environment.NewLine +
        "    fetch aCur into " + Environment.NewLine +
        "      " + sSQLColumnVarList + Environment.NewLine +
        "  end" + Environment.NewLine +
        "  close aCur" + Environment.NewLine +
        "  deallocate aCur" + Environment.NewLine +
        "end" + Environment.NewLine;
    }

  }

  public class StringDict : ConcurrentDictionary<string, string> {
    public virtual Boolean Contains(String key) {
      try {
        return (!(base[key] is null));
      } catch {
        return false;
      }
    }
    public virtual string Add(string value) { 
      if (!Contains(value)) {
        TryAdd(value, value);
      }
      return value;
    }

    public string AsString() { 
      StringBuilder ret = new StringBuilder();
      foreach (string key in base.Keys) { 
        ret.Append(key+Environment.NewLine);
      }
      return ret.ToString();
    }

  }
}
