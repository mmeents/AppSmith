using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AppSmith.Models.ParserExt;

namespace AppSmith.Models {  
  public static class ParserExt {
    public enum sqlTokenType { 
      Keyword, Identifier, ColType, ParanStart, ParanEnd, Comma, Number, Comment 
    }
    public class pscOp { 
      public sqlTokenType OpType;
      public string Content;
    }
    public static List<pscOp> ParseSqlContent(string createQueryStr) {
      List<pscOp> pscOps = new List<pscOp>();      
      var lines = createQueryStr.Parse(Environment.NewLine);
      foreach (var line in lines) { 
        string noComment = line.IndexOf("--") == -1 ? line : line.Substring(0, line.IndexOf("--"));
        var raw = noComment.Replace(",", " , ").Replace("(", " (").Replace(")", " )").Parse(" ");
        foreach (var op in raw) {
          string wop = op;
          while (wop.Length > 0) { 

            if ((wop.Length > 0) && (wop[0]=='(') ) {
              pscOps.Add(new pscOp { OpType = sqlTokenType.ParanStart, Content = wop[0].AsString() });
              wop = wop.Substring(1);             
            } else if ((wop.Length > 0) && (wop[0] == ')')) {
              pscOps.Add(new pscOp { OpType = sqlTokenType.ParanEnd, Content = wop[0].AsString() });
              wop = wop.Substring(1);
            } else if ((wop.Length > 0) && (wop[0] == ',')) {
              pscOps.Add(new pscOp { OpType = sqlTokenType.Comma, Content = wop[0].AsString() });
              wop = wop.Substring(1);          
            } else if ((wop.Length > 0) && wop.IsNumber()) { 
              string sout = wop.ReadNumbers();
              if (sout.Length > 0) {
                pscOps.Add(new pscOp { OpType = sqlTokenType.Number, Content = sout });
                wop = wop.Substring(sout.Length);
              }
            } else if ((wop.Length > 0) && (wop.IsKeyword())) { 
               pscOps.Add(new pscOp { OpType = sqlTokenType.Keyword, Content = wop });
               wop = "";
            } else if ((wop.Length > 0) && (wop.IsColType())) { 
              string sout = wop.ReadColType();
              if (sout.Length > 0) {
                pscOps.Add(new pscOp { OpType = sqlTokenType.ColType, Content = sout });
                wop = wop.Substring(sout.Length);
              }
            } else if ((wop.Length > 0) && (wop.IsIdentifier())) {
              string sout = wop.ReadIdentifier();
              if (sout.Length > 0) {
                string display = sout.RemoveChar('[').RemoveChar(']');
                pscOps.Add(new pscOp { OpType = sqlTokenType.Identifier, Content = display });
                wop = wop.Substring(sout.Length);
              }
            }
          }
        }
      }
      return pscOps;
    }

    public static bool IsNumber(this string content) {      
      return ((content.Length > 0) && ("0123456789.-".IndexOf(content[0]) >= 0));
    }
    public static string ReadNumbers(this string content) {
      if (content == null) return "";
      if ((content.Length > 0) && ("0123456789.-".IndexOf(content[0]) >= 0)){ 
        int i = 0;
        string sout ="";
        while((i <= content.Length-1 ) && ("0123456789.-".IndexOf(content[i]) >= 0)) {
          sout += content[i];
          i++;
        }
        return sout;
      }
      return "";
    }
    public static bool IsKeyword(this string content) {
      if (content == null) return false;
      var CappedContent = content.ToUpper();
      switch (CappedContent) {
        case "ADD": return true;
        case "ALL": return true;
        case "ALTER": return true;
        case "AND": return true;
        case "ANY": return true;
        case "AS": return true;
        case "ASC": return true;
        case "AUTHORIZATION": return true;
        case "BACKUP": return true;
        case "BEGIN": return true;
        case "BETWEEN": return true;
        case "BREAK": return true;
        case "BROWSE": return true;
        case "BULK": return true;
        case "BY": return true;
        case "CASCADE": return true;
        case "CASE": return true;
        case "CHECK": return true;
        case "CHECKPOINT": return true;
        case "CLOSE": return true;
        case "CLUSTERED": return true;
        case "COALESCE": return true;
        case "COLLATE": return true;
        case "COLUMN": return true;
        case "COMMIT": return true;
        case "COMPUTE": return true;
        case "CONSTRAINT": return true;
        case "CONTAINS": return true;
        case "CONTAINSTABLE": return true;
        case "CONTINUE": return true;
        case "CONVERT": return true;
        case "CREATE": return true;
        case "CROSS": return true;
        case "CURRENT": return true;
        case "CURRENT_DATE": return true;
        case "CURRENT_TIME": return true;
        case "CURRENT_TIMESTAMP": return true;
        case "CURRENT_USER": return true;
        case "CURSOR": return true;
        case "DATABASE": return true;
        case "DBCC": return true;
        case "DEALLOCATE": return true;
        case "DECLARE": return true;
        case "DEFAULT": return true;
        case "DELETE": return true;
        case "DENY": return true;
        case "DESC": return true;
        case "DISK": return true;
        case "DISTINCT": return true;
        case "DISTRIBUTED": return true;
        case "DOUBLE": return true;
        case "DROP": return true;
        case "DUMP": return true;
        case "ELSE": return true;
        case "END": return true;
        case "ERRLVL": return true;
        case "ESCAPE": return true;
        case "EXCEPT": return true;
        case "EXEC": return true;
        case "EXECUTE": return true;
        case "EXISTS": return true;
        case "EXIT": return true;
        case "EXTERNAL": return true;
        case "FETCH": return true;
        case "FILE": return true;
        case "FILLFACTOR": return true;
        case "FOR": return true;
        case "FOREIGN": return true;
        case "FREETEXT": return true;
        case "FREETEXTTABLE": return true;
        case "FROM": return true;
        case "FULL": return true;
        case "FUNCTION": return true;
        case "GOTO": return true;
        case "GRANT": return true;
        case "GROUP": return true;
        case "HAVING": return true;
        case "HOLDLOCK": return true;
        case "IDENTITY": return true;
        case "IDENTITY_INSERT": return true;
        case "IDENTITYCOL": return true;
        case "IF": return true;
        case "IN": return true;
        case "INDEX": return true;
        case "INNER": return true;
        case "INSERT": return true;
        case "INTERSECT": return true;
        case "INTO": return true;
        case "IS": return true;
        case "JOIN": return true;
        case "KEY": return true;
        case "KILL": return true;
        case "LEFT": return true;
        case "LIKE": return true;
        case "LINENO": return true;
        case "LOAD": return true;
        case "MERGE": return true;
        case "NATIONAL": return true;
        case "NOCHECK": return true;
        case "NONCLUSTERED": return true;
        case "NOT": return true;
        case "NULL": return true;
        case "NULLIF": return true;
        case "OF": return true;
        case "OFF": return true;
        case "OFFSETS": return true;
        case "ON": return true;
        case "OPEN": return true;
        case "OPENDATASOURCE": return true;
        case "OPENQUERY": return true;
        case "OPENROWSET": return true;
        case "OPENXML": return true;
        case "OPTION": return true;
        case "OR": return true;
        case "ORDER": return true;
        case "OUTER": return true;
        case "OVER": return true;
        case "PROCEDURE": return true;
        case "PUBLIC": return true;
        case "RAISERROR": return true;
        case "READ": return true;
        case "READTEXT": return true;
        case "RECONFIGURE": return true;
        case "REFERENCES": return true;
        case "REPLICATION": return true;
        case "RESTORE": return true;
        case "RESTRICT": return true;
        case "RETURN": return true;
        case "REVERT": return true;
        case "REVOKE": return true;
        case "RIGHT": return true;
        case "ROLLBACK": return true;
        case "ROWCOUNT": return true;
        case "ROWGUIDCOL": return true;
        case "RULE": return true;
        case "SAVE": return true;
        case "SCHEMA": return true;
        case "SECURITYAUDIT": return true;
        case "SELECT": return true;
        case "SEMANTICKEYPHRASETABLE": return true;
        case "SEMANTICSIMILARITYDETAILSTABLE": return true;
        case "SEMANTICSIMILARITYTABLE": return true;
        case "SESSION_USER": return true;
        case "SET": return true;
        case "SETUSER": return true;
        case "SHUTDOWN": return true;
        case "SOME": return true;
        case "STATISTICS": return true;
        case "SYSTEM_USER": return true;
        case "TABLE": return true;
        case "TABLESAMPLE": return true;
        case "TEXTSIZE": return true;
        case "THEN": return true;
        case "TO": return true;
        case "TOP": return true;
        case "TRAN": return true;
        case "TRANSACTION": return true;
        case "TRIGGER": return true;
        case "TRUNCATE": return true;
        case "TRY_CONVERT": return true;
        case "TSEQUAL": return true;
        case "UNION": return true;
        case "UNIQUE": return true;
        case "UNPIVOT": return true;
        case "UPDATE": return true;
        case "UPDATETEXT": return true;
        case "USE": return true;
        case "USER": return true;
        case "VALUES": return true;
        case "VARYING": return true;
        case "VIEW": return true;
        case "WAITFOR": return true;
        case "PERCENT": return true;
        case "PIVOT": return true;
        case "PLAN": return true;
        case "PRECISION": return true;
        case "PRIMARY": return true;
        case "PRINT": return true;
        case "PROC": return true;
        case "WHEN": return true;
        case "WHERE": return true;
        case "WHILE": return true;
        case "WITH": return true;
        case "WITHIN": return true;
        case "WRITETEXT": return true;
      }
      return false;
    }
    public static bool IsColType(this string content) {
      if (content == null) return false;
      var CappedContent = content.ToLower().Replace('[', ' ').Replace(']', ' ').Replace('(', ' ').ParseFirst(" ,");
      switch (CappedContent) {        
        case "char": return true;
        case "varchar": return true;
        case "int": return true;
        case "bigint": return true;
        case "binary": return true;
        case "bit": return true;
        case "datetime": return true;
        case "decimal": return true;
        case "float": return true;
        case "image": return true;
        case "money": return true;
        case "numeric": return true;
        case "nchar": return true;
        case "ntext": return true;
        case "nvarchar": return true;
        case "real": return true;
        case "smallint": return true;
        case "smallmoney": return true;
        case "smalldatetime": return true;
        case "text": return true;
        case "timestamp": return true;
        case "tinyint": return true;
        case "uniqueidentifier": return true;
        case "varbinary": return true;
        default: return false;
      }      
    }
    public static string ReadColType(this string content) {
      if (content == null) return "";
      string OutVar = content.Replace('(', ' ').ParseFirst(" ,");
      string CappedContent = OutVar.ToLower().Replace('[', ' ').Replace(']', ' ').ParseFirst(" ,");
      switch (CappedContent) {
        case "char": return OutVar;
        case "varchar": return OutVar;
        case "int": return OutVar;
        case "bigint": return OutVar;
        case "binary": return OutVar;
        case "bit": return OutVar;
        case "datetime": return OutVar;
        case "decimal": return OutVar;
        case "float": return OutVar;
        case "image": return OutVar;
        case "money": return OutVar;        
        case "nchar": return OutVar;
        case "ntext": return OutVar;
        case "nvarchar": return OutVar;
        case "real": return OutVar;
        case "smallint": return OutVar;
        case "smallmoney": return OutVar;
        case "smalldatetime": return OutVar;
        case "text": return OutVar;
        case "timestamp": return OutVar;
        case "tinyint": return OutVar;
        case "uniqueidentifier": return OutVar;
        case "varbinary": return OutVar;
        default:
          break;
      }
      return "";
    }

    public static int SqlTypeToLookupId(this string colSqlType) {
      if (colSqlType == null) return 0;
      string OutVar = colSqlType.Replace('(', ' ').ParseFirst(" ,");
      string CappedContent = OutVar.ToLower().Replace('[', ' ').Replace(']', ' ').ParseFirst(" ,");
      switch (CappedContent) {
        case "char": return 39;
        case "varchar": return 34;
        case "int": return 30;
        case "bigint": return 31;
        case "binary": return 40;
        case "bit": return 32;
        case "datetime": return 33;
        case "decimal": return 38;
        case "float": return 41;
        case "image": return 47;
        case "money": return 48;
        case "nchar": return 50;
        case "ntext": return 51;
        case "nvarchar": return 35;
        case "real": return 49;
        case "smallint": return 52;
        case "smallmoney": return 53;
        case "smalldatetime": return 54;
        case "text": return 55;
        case "timestamp": return 56;
        case "tinyint": return 57;
        case "uniqueidentifier": return 58;
        case "varbinary": return 59;
        default:
          break;
      }
      return 0;
    }
    public static bool IsIdentifier(this string content) {
      string wop = content;
      if ((wop.Length > 0) && (wop[0] == '(')) {
        return false;
      }
      if ((wop.Length > 0) && (wop[0] == ')')) {
        return false;
      }
      if ((wop.Length > 0) && (wop[0] == ',')) {
        return false;
      }
      if ((wop.Length > 0) && wop.IsNumber()) {
        return false;
      }
      if ((wop.Length > 0) && (wop.IsKeyword())) {
        return false;
      }
      if ((wop.Length > 0) && (wop.IsColType())) {
        return false;
      }
      if (wop.Length > 0) return true;
      return false;
    }
    public static string ReadIdentifier(this string content) {
      if (content == null) return "";
      return content.Replace('(', ' ').Replace(')', ' ').ParseFirst(" ,");
    }


    


  }
}
