using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppSmith.Models {
  public enum TnType {
    Types = 1,
    Server = 18,
    Database = 19,

    // folders

    Tables = 20,
    Views = 21,
    Procedures = 22,
    Functions = 23,

    // objects
    Table = 24,
    View = 25,
    Procedure = 26,
    Function = 27,

    // params columns
    TableColumn = 43,
    ViewColumn = 44,
    ProcedureParam = 45,
    FunctionParam = 46,

    // Api Objects.
    Api = 60,
    Controller = 61,
    Method = 62,
    ProcBody = 63
  }

  public enum Tii {
    Internal = 0,
    Server = 1,
    Database = 2,
    Folder = 3,
    View = 4,
    Table = 5,
    Procedure = 6,
    Function = 7,
    Column = 8,
    News = 9,
    Search = 10,
    Gift = 11,
    Add = 12,
    Play = 13,
    Stop = 14,
    Delete = 15,
    Label = 16,
    Share = 17,
    Setup = 18,
    logo = 19
  }
  public class ItemType : TreeNode {
    public ItemType() { }
    private int _id;
    public int TypeId {
      get { return _id; }
      set {
        _id = value;
        switch (this.TypeId) {
          case (int)TnType.Server:
            this.ImageIndex = (int)Tii.Server;
            this.SelectedImageIndex = (int)Tii.Server;
            break;
          case (int)TnType.Database:
            this.ImageIndex = (int)Tii.Database;
            this.SelectedImageIndex = (int)Tii.Database;
            break;
          case (int)TnType.Api:
            this.ImageIndex = (int)Tii.Server;
            this.SelectedImageIndex = (int)Tii.Server;
            break;
          case (int)TnType.Procedures:
            this.ImageIndex = (int)Tii.Folder;
            this.SelectedImageIndex = (int)Tii.Folder;
            break;
          case (int)TnType.Tables:
            this.ImageIndex = (int)Tii.Folder;
            this.SelectedImageIndex = (int)Tii.Folder;
            break;
          case (int)TnType.Views:
            this.ImageIndex = (int)Tii.Folder;
            this.SelectedImageIndex = (int)Tii.Folder;
            break;
          case (int)TnType.Functions:
            this.ImageIndex = (int)Tii.Folder;
            this.SelectedImageIndex = (int)Tii.Folder;
            break;
          case (int)TnType.Controller:
            this.ImageIndex = (int)Tii.Folder;
            this.SelectedImageIndex = (int)Tii.Folder;
            break;
          case (int)TnType.Table:
            this.ImageIndex = (int)Tii.Table;
            this.SelectedImageIndex = (int)Tii.Table;
            break;
          case (int)TnType.View:
            this.ImageIndex = (int)Tii.View;
            this.SelectedImageIndex = (int)Tii.View;
            break;
          case (int)TnType.Procedure:
            this.ImageIndex = (int)Tii.Procedure;
            this.SelectedImageIndex = (int)Tii.Procedure;
            break;
          case (int)TnType.Function:
            this.ImageIndex = (int)Tii.Function;
            this.SelectedImageIndex = (int)Tii.Function;
            break;
          case (int)TnType.Method:
            this.ImageIndex = (int)Tii.Function;
            this.SelectedImageIndex = (int)Tii.Function;
            break;
          case (int)TnType.TableColumn:
            this.ImageIndex = (int)Tii.Column;
            this.SelectedImageIndex = (int)Tii.Column;
            break;
          case (int)TnType.ViewColumn:
            this.ImageIndex = (int)Tii.Column;
            this.SelectedImageIndex = (int)Tii.Column;
            break;
          case (int)TnType.ProcedureParam:
            this.ImageIndex = (int)Tii.Column;
            this.SelectedImageIndex = (int)Tii.Column;
            break;
          case (int)TnType.FunctionParam:
            this.ImageIndex = (int)Tii.Column;
            this.SelectedImageIndex = (int)Tii.Column;
            break;
          case (int)TnType.ProcBody:
            this.ImageIndex= (int)Tii.Internal;
            this.SelectedImageIndex= (int)Tii.Internal;
            break;
        }
      }
    }
    public int OwnerTypeId { get; set; } = 0;
    public int CatagoryTypeId { get; set; } = 0;
    public int EditorTypeId { get; set; } = 0;
    public int TypeRank { get; set; } = 0;
    public int TypeEnum { get; set; } = 0;
    public new string Name { get { return base.Text; } set { base.Text = value; } }
    public bool Visible { get; set; } = false;
    public string Desc { get; set; } = "";
    public bool Readonly { get; set; } = false;

  }


  public class Types : ConcurrentDictionary<int, ItemType> {
    public Types() : base() {
      Load();
    }
    public void Load() {
      base.Clear();
      this[0] = new ItemType() { TypeId = 0, OwnerTypeId = 0, CatagoryTypeId = 0, EditorTypeId = 0, TypeRank = 0, TypeEnum = 0, Name = "none", Visible = true, Readonly = true, Desc = "" };

      this[1] = new ItemType() { TypeId = 1, OwnerTypeId = 1, CatagoryTypeId = 0, EditorTypeId = 10, TypeRank = 1, TypeEnum = 1, Visible = false, Readonly = true, Name = "Internal", Desc = "Root " };
      this[2] = new ItemType() { TypeId = 2, OwnerTypeId = 1, CatagoryTypeId = 2, EditorTypeId = 10, TypeRank = 1, TypeEnum = 1, Visible = false, Readonly = true, Name = "CatagoryTypes", Desc = "Catagory Type" };
      this[7] = new ItemType() { TypeId = 7, OwnerTypeId = 1, CatagoryTypeId = 3, EditorTypeId = 10, TypeRank = 1, TypeEnum = 1, Visible = false, Readonly = true, Name = "EditorTypes", Desc = "Editor Type" };
      this[17] = new ItemType() { TypeId = 17, OwnerTypeId = 1, CatagoryTypeId = 4, EditorTypeId = 10, TypeRank = 1, TypeEnum = 1, Visible = false, Readonly = true, Name = "thisTypes", Desc = "Model Type" };
      this[28] = new ItemType() { TypeId = 28, OwnerTypeId = 1, CatagoryTypeId = 3, EditorTypeId = 10, TypeRank = 1, TypeEnum = 1, Visible = false, Readonly = true, Name = "Lookups", Desc = "Lookup Editors" };

      this[3] = new ItemType() { TypeId = 3, OwnerTypeId = 2, CatagoryTypeId = 3, EditorTypeId = 10, TypeRank = 1, TypeEnum = 1, Visible = false, Readonly = true, Name = "Internal", Desc = "Catagory Internal" };
      this[4] = new ItemType() { TypeId = 4, OwnerTypeId = 2, CatagoryTypeId = 4, EditorTypeId = 10, TypeRank = 2, TypeEnum = 2, Visible = true, Readonly = true, Name = "Display", Desc = "Catagory Display" };

      this[8] = new ItemType() { TypeId = 8, OwnerTypeId = 7, CatagoryTypeId = 3, EditorTypeId = 8, TypeRank = 1, TypeEnum = 1, Visible = true, Readonly = true, Name = "Int", Desc = "The Int Editor " };
      this[9] = new ItemType() { TypeId = 9, OwnerTypeId = 7, CatagoryTypeId = 3, EditorTypeId = 9, TypeRank = 2, TypeEnum = 2, Visible = true, Readonly = true, Name = "String", Desc = "The String Editor" };
      this[10] = new ItemType() { TypeId = 10, OwnerTypeId = 7, CatagoryTypeId = 3, EditorTypeId = 10, TypeRank = 0, TypeEnum = 0, Visible = false, Readonly = true, Name = "Hidden", Desc = "The Hidden Editor" };
      this[11] = new ItemType() { TypeId = 11, OwnerTypeId = 7, CatagoryTypeId = 3, EditorTypeId = 11, TypeRank = 3, TypeEnum = 3, Visible = true, Readonly = true, Name = "Lookup", Desc = "The Lookup Editor" };
      this[12] = new ItemType() { TypeId = 12, OwnerTypeId = 7, CatagoryTypeId = 3, EditorTypeId = 12, TypeRank = 4, TypeEnum = 4, Visible = true, Readonly = true, Name = "DateTime", Desc = "The DateTime Editor" };
      this[13] = new ItemType() { TypeId = 13, OwnerTypeId = 7, CatagoryTypeId = 3, EditorTypeId = 13, TypeRank = 5, TypeEnum = 5, Visible = true, Readonly = true, Name = "Bool", Desc = "The Bool Editor" };
      this[14] = new ItemType() { TypeId = 14, OwnerTypeId = 7, CatagoryTypeId = 3, EditorTypeId = 14, TypeRank = 6, TypeEnum = 6, Visible = true, Readonly = true, Name = "Color", Desc = "The Color Editor" };
      this[15] = new ItemType() { TypeId = 15, OwnerTypeId = 7, CatagoryTypeId = 3, EditorTypeId = 15, TypeRank = 7, TypeEnum = 7, Visible = true, Readonly = true, Name = "Filename", Desc = "The Filename Editor" };
      this[16] = new ItemType() { TypeId = 16, OwnerTypeId = 7, CatagoryTypeId = 3, EditorTypeId = 16, TypeRank = 8, TypeEnum = 8, Visible = true, Readonly = true, Name = "Password", Desc = "The Password Editor" };
      this[42] = new ItemType() { TypeId = 42, OwnerTypeId = 7, CatagoryTypeId = 3, EditorTypeId = 42, TypeRank = 9, TypeEnum = 9, Visible = true, Readonly = true, Name = "Decimal", Desc = "The Decimal Editor" };

      this[18] = new ItemType() { TypeId = 18, OwnerTypeId = 17, CatagoryTypeId = 4, EditorTypeId = 9, TypeRank = 1, TypeEnum = 1, Visible = true, Readonly = false, Name = "Server", Desc = "Server Type" };
      this[19] = new ItemType() { TypeId = 19, OwnerTypeId = 18, CatagoryTypeId = 4, EditorTypeId = 9, TypeRank = 1, TypeEnum = 2, Visible = true, Readonly = false, Name = "Database", Desc = "Database Type" };
      this[20] = new ItemType() { TypeId = 20, OwnerTypeId = 19, CatagoryTypeId = 4, EditorTypeId = 9, TypeRank = 1, TypeEnum = 3, Visible = true, Readonly = true, Name = "Tables", Desc = "Tables Type" };
      this[21] = new ItemType() { TypeId = 21, OwnerTypeId = 19, CatagoryTypeId = 4, EditorTypeId = 9, TypeRank = 2, TypeEnum = 4, Visible = true, Readonly = true, Name = "Views", Desc = "Views Type" };
      this[22] = new ItemType() { TypeId = 22, OwnerTypeId = 19, CatagoryTypeId = 4, EditorTypeId = 9, TypeRank = 3, TypeEnum = 5, Visible = true, Readonly = true, Name = "Procedures", Desc = "Procedures Type" };
      this[23] = new ItemType() { TypeId = 23, OwnerTypeId = 19, CatagoryTypeId = 4, EditorTypeId = 9, TypeRank = 4, TypeEnum = 6, Visible = true, Readonly = true, Name = "Functions", Desc = "Functions Type" };
      this[24] = new ItemType() { TypeId = 24, OwnerTypeId = 20, CatagoryTypeId = 4, EditorTypeId = 9, TypeRank = 1, TypeEnum = 7, Visible = true, Readonly = false, Name = "Table", Desc = "Table Type" };
      this[25] = new ItemType() { TypeId = 25, OwnerTypeId = 21, CatagoryTypeId = 4, EditorTypeId = 9, TypeRank = 1, TypeEnum = 8, Visible = true, Readonly = false, Name = "View", Desc = "View Type" };
      this[26] = new ItemType() { TypeId = 26, OwnerTypeId = 22, CatagoryTypeId = 4, EditorTypeId = 9, TypeRank = 1, TypeEnum = 9, Visible = true, Readonly = false, Name = "Procedure", Desc = "Procedure Type" };
      this[27] = new ItemType() { TypeId = 27, OwnerTypeId = 23, CatagoryTypeId = 4, EditorTypeId = 9, TypeRank = 1, TypeEnum = 10, Visible = true, Readonly = false, Name = "Function", Desc = "Function Type" };
      this[43] = new ItemType() { TypeId = 43, OwnerTypeId = 24, CatagoryTypeId = 4, EditorTypeId = 29, TypeRank = 1, TypeEnum = 11, Visible = true, Readonly = false, Name = "TableColumn", Desc = "Table Column Type" };
      this[44] = new ItemType() { TypeId = 44, OwnerTypeId = 25, CatagoryTypeId = 4, EditorTypeId = 29, TypeRank = 1, TypeEnum = 12, Visible = true, Readonly = false, Name = "ViewColumn", Desc = "View Column Type" };
      this[45] = new ItemType() { TypeId = 45, OwnerTypeId = 26, CatagoryTypeId = 4, EditorTypeId = 29, TypeRank = 1, TypeEnum = 13, Visible = true, Readonly = false, Name = "ProcedureParam", Desc = "Procedure Param Type" };
      this[46] = new ItemType() { TypeId = 46, OwnerTypeId = 27, CatagoryTypeId = 4, EditorTypeId = 29, TypeRank = 1, TypeEnum = 14, Visible = true, Readonly = false, Name = "FunctionParam", Desc = "Function Param Type" };

      this[60] = new ItemType() { TypeId = 60, OwnerTypeId = 18, CatagoryTypeId = 4, EditorTypeId = 9, TypeRank = 1, TypeEnum = 2, Visible = true, Readonly = false, Name = "Api", Desc = "Api Type" };
      this[61] = new ItemType() { TypeId = 61, OwnerTypeId = 60, CatagoryTypeId = 4, EditorTypeId = 9, TypeRank = 1, TypeEnum = 2, Visible = true, Readonly = false, Name = "Controller", Desc = "Controller Type" };
      this[62] = new ItemType() { TypeId = 62, OwnerTypeId = 61, CatagoryTypeId = 4, EditorTypeId = 9, TypeRank = 1, TypeEnum = 2, Visible = true, Readonly = false, Name = "Method", Desc = "Method Type" };

      this[63] = new ItemType() { TypeId = 63, OwnerTypeId = 26, CatagoryTypeId = 4, EditorTypeId = 9, TypeRank = 1, TypeEnum = 2, Visible = true, Readonly = false, Name = "ProcBody", Desc = "Procedure Body" };

      this[29] = new ItemType() { TypeId = 29, OwnerTypeId = 28, CatagoryTypeId = 3, EditorTypeId = 11, TypeRank = 1, TypeEnum = 1, Visible = false, Readonly = true, Name = "Table Columns", Desc = "Table Column Type Lookup" };

      this[30] = new ItemType() { TypeId = 30, OwnerTypeId = 29, CatagoryTypeId = 4, EditorTypeId = 8, TypeRank = 1, TypeEnum = 1, Visible = false, Readonly = true, Name = "int", Desc = "int" };
      this[31] = new ItemType() { TypeId = 31, OwnerTypeId = 29, CatagoryTypeId = 4, EditorTypeId = 8, TypeRank = 2, TypeEnum = 5, Visible = false, Readonly = true, Name = "bigint", Desc = "bigint" };
      this[32] = new ItemType() { TypeId = 32, OwnerTypeId = 29, CatagoryTypeId = 4, EditorTypeId = 13, TypeRank = 3, TypeEnum = 3, Visible = false, Readonly = true, Name = "bit", Desc = "bit" };
      this[33] = new ItemType() { TypeId = 33, OwnerTypeId = 29, CatagoryTypeId = 4, EditorTypeId = 12, TypeRank = 4, TypeEnum = 4, Visible = false, Readonly = true, Name = "datetime", Desc = "datetime" };
      this[34] = new ItemType() { TypeId = 34, OwnerTypeId = 29, CatagoryTypeId = 4, EditorTypeId = 9, TypeRank = 5, TypeEnum = 2, Visible = false, Readonly = true, Name = "varchar", Desc = "varchar(50)" };
      this[35] = new ItemType() { TypeId = 35, OwnerTypeId = 29, CatagoryTypeId = 4, EditorTypeId = 9, TypeRank = 6, TypeEnum = 6, Visible = false, Readonly = true, Name = "nvarchar", Desc = "nvarchar(50)" };      
      this[38] = new ItemType() { TypeId = 38, OwnerTypeId = 29, CatagoryTypeId = 4, EditorTypeId = 42, TypeRank = 9, TypeEnum = 9, Visible = false, Readonly = true, Name = "decimal", Desc = "decimal(9,2)" };
      this[39] = new ItemType() { TypeId = 39, OwnerTypeId = 29, CatagoryTypeId = 4, EditorTypeId = 42, TypeRank = 10, TypeEnum = 10, Visible = false, Readonly = true, Name = "char", Desc = "char(50)" };
      this[40] = new ItemType() { TypeId = 40, OwnerTypeId = 29, CatagoryTypeId = 4, EditorTypeId = 42, TypeRank = 11, TypeEnum = 11, Visible = false, Readonly = true, Name = "binary", Desc = "binary(14)" };
      this[41] = new ItemType() { TypeId = 41, OwnerTypeId = 29, CatagoryTypeId = 4, EditorTypeId = 42, TypeRank = 12, TypeEnum = 12, Visible = false, Readonly = true, Name = "float", Desc = "float(53)" };

      this[47] = new ItemType() { TypeId = 47, OwnerTypeId = 29, CatagoryTypeId = 4, EditorTypeId = 9, TypeRank = 13, TypeEnum = 13, Visible = false, Readonly = true, Name = "image", Desc = "image" };
      this[48] = new ItemType() { TypeId = 48, OwnerTypeId = 29, CatagoryTypeId = 4, EditorTypeId = 9, TypeRank = 14, TypeEnum = 14, Visible = false, Readonly = true, Name = "money", Desc = "decimal(53)" };
      this[49] = new ItemType() { TypeId = 49, OwnerTypeId = 29, CatagoryTypeId = 4, EditorTypeId = 9, TypeRank = 15, TypeEnum = 15, Visible = false, Readonly = true, Name = "real", Desc = "real(53)" };
      this[50] = new ItemType() { TypeId = 50, OwnerTypeId = 29, CatagoryTypeId = 4, EditorTypeId = 9, TypeRank = 16, TypeEnum = 16, Visible = false, Readonly = true, Name = "nchar", Desc = "nchar(53)" };
      this[51] = new ItemType() { TypeId = 51, OwnerTypeId = 29, CatagoryTypeId = 4, EditorTypeId = 9, TypeRank = 17, TypeEnum = 17, Visible = false, Readonly = true, Name = "ntext", Desc = "ntext" };
      this[52] = new ItemType() { TypeId = 52, OwnerTypeId = 29, CatagoryTypeId = 4, EditorTypeId = 9, TypeRank = 18, TypeEnum = 18, Visible = false, Readonly = true, Name = "smallint", Desc = "smallint" };
      this[53] = new ItemType() { TypeId = 53, OwnerTypeId = 29, CatagoryTypeId = 4, EditorTypeId = 9, TypeRank = 19, TypeEnum = 19, Visible = false, Readonly = true, Name = "smallmoney", Desc = "smallmoney" };
      this[54] = new ItemType() { TypeId = 54, OwnerTypeId = 29, CatagoryTypeId = 4, EditorTypeId = 9, TypeRank = 20, TypeEnum = 20, Visible = false, Readonly = true, Name = "smalldatetime", Desc = "smalldatetime" };
      this[55] = new ItemType() { TypeId = 55, OwnerTypeId = 29, CatagoryTypeId = 4, EditorTypeId = 9, TypeRank = 21, TypeEnum = 21, Visible = false, Readonly = true, Name = "text", Desc = "text" };
      this[56] = new ItemType() { TypeId = 56, OwnerTypeId = 29, CatagoryTypeId = 4, EditorTypeId = 9, TypeRank = 22, TypeEnum = 22, Visible = false, Readonly = true, Name = "timestamp", Desc = "timestamp" };
      this[57] = new ItemType() { TypeId = 57, OwnerTypeId = 29, CatagoryTypeId = 4, EditorTypeId = 9, TypeRank = 23, TypeEnum = 23, Visible = false, Readonly = true, Name = "tinyint", Desc = "tinyint" };
      this[58] = new ItemType() { TypeId = 58, OwnerTypeId = 29, CatagoryTypeId = 4, EditorTypeId = 9, TypeRank = 24, TypeEnum = 24, Visible = false, Readonly = true, Name = "uniqueidentifier", Desc = "uniqueidentifier" };
      this[59] = new ItemType() { TypeId = 59, OwnerTypeId = 29, CatagoryTypeId = 4, EditorTypeId = 9, TypeRank = 25, TypeEnum = 25, Visible = false, Readonly = true, Name = "varbinary", Desc = "varbinary(50)" };


      this[19] = LoadSubtypes(this[19]);

      this[24] = LoadSubtypes(this[24]);
      this[25] = LoadSubtypes(this[25]);
      this[26] = LoadSubtypes(this[26]);
      this[27] = LoadSubtypes(this[27]);
    }

    public IEnumerable<ItemType> GetChildrenItemsNoDef(int id) {
      return this.Select(x => x.Value).Where(x => x.OwnerTypeId == id).OrderBy(x => x.TypeRank);
    }
    public IEnumerable<ItemType> GetChildrenItems(int id) {
      return this.Select(x => x.Value).Where(x => ((x.OwnerTypeId == id) || (x.OwnerTypeId == 0))).OrderBy(x => x.TypeRank);
    }


    public ItemType LoadSubtypes(ItemType item) {
      var items = GetOwnersTypes(item.TypeId);
      if (item.Nodes.Count > 0) item.Nodes.Clear();
      foreach (ItemType it in items) {
        item.Nodes.Add(it);
      }
      return item;
    }

    public IEnumerable<ItemType> GetOwnersTypes(int ownerTypeId) {
      try {
        IEnumerable<ItemType> result = GetChildrenItems(ownerTypeId);
        return result;
      } catch {
        return null;
      }
    }

    public virtual Boolean Contains(int id) {
      try {
        return !(base[id] is null);
      } catch {
        return false;
      }
    }
    public virtual new ItemType this[int id] {
      get { return Contains(id) ? base[id] : base.Values.First<ItemType>(x => x.TypeId > id); }
      set { if (value != null) { base[id] = value; } else { Remove(id); } }
    }

    public virtual void Remove(int id) { if (Contains(id)) { _ = base.TryRemove(id, out _); } }
  }

}

