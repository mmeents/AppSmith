using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.Windows.Forms;
using System.Collections.Concurrent;
using System.Xml.Linq;
using FastColoredTextBoxNS;

namespace AppSmith.Models {

  public class Item : TreeNode {
    private int _TypeId = 0;
    private int _ItemRank = 0;
    private int _NullTypeId = 0;
    private int _AccessTypeId = 0;
    private int _SQLTypeId = 0;
    private int _CSharpTypeId  = 0;
    private int _MethodTypeId  = 0;
    private int _ValueTypeId = 0;   
    
    private string _IsArrayTypes = "";   
    private string _BaseClass = "";
    private string _Code = "";
    private string _ReturnType = "";
    private string _Route  = "";
    private string _SQLTypeSize= "";
    private string _Url = "";
    private string _Version  = "";
    private string _ValueTypeSize = "";

    public bool Dirty = false;
    public Item() : base() { }
    public int Id { get; set; } = 0;
    public int OwnerId { get; set; } = 0;
    public int TypeId {
      get { return _TypeId; }
      set {
        Dirty = true;
        _TypeId = value;
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
            this.ImageIndex = (int)Tii.Share;
            this.SelectedImageIndex = (int)Tii.Share;
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
            this.ImageIndex = (int)Tii.Share;
            this.SelectedImageIndex = (int)Tii.Share;
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
            this.ImageIndex = (int)Tii.Internal;
            this.SelectedImageIndex = (int)Tii.Internal;
            break;
          case (int)TnType.MethodParam:
            this.ImageIndex = (int)Tii.Column;
            this.SelectedImageIndex = (int)Tii.Column;
            break;
          case (int)TnType.Class:
            this.ImageIndex = (int)Tii.Table;
            this.SelectedImageIndex = (int)Tii.Table;
            break;
          case (int)TnType.Property:
            this.ImageIndex = (int)Tii.Column;
            this.SelectedImageIndex = (int)Tii.Column;
            break;
        }
      }
    }    
    public int ItemRank { get { return _ItemRank; } set { _ItemRank = value; Dirty = true; } }
    public int NullTypeId { get { return _NullTypeId; } set { _NullTypeId = value; Dirty = true; } }
    public int AccessTypeId { get { return _AccessTypeId; } set { _AccessTypeId = value; Dirty = true; } }
    public int SQLTypeId { get { return _SQLTypeId; } set { _SQLTypeId = value; Dirty = true; } }
    public int CSharpTypeId { get { return _CSharpTypeId; } set { _CSharpTypeId = value; Dirty = true; } }
    public int MethodTypeId { get { return _MethodTypeId; } set { _MethodTypeId = value; Dirty = true; } }
    public int ValueTypeId { get { return _ValueTypeId; } set { _ValueTypeId = value; Dirty = true; } }
    

    public string IsArrayTypes { get { return _IsArrayTypes; } set { _IsArrayTypes = value; Dirty = true; } }
    public string BaseClass { get { return _BaseClass; } set { _BaseClass = value; Dirty = true; } }
    public string Code { get { return _Code; } set { _Code = value; Dirty = true; } }
    public new string Name { get { return base.Text; } set { base.Text = value; Dirty = true; } }
    public string ReturnType { get { return _ReturnType; } set { _ReturnType = value; Dirty = true; } }
    public string Route { get { return _Route; } set { _Route = value; Dirty = true; } }
    public string SQLTypeSize { get { return _SQLTypeSize; } set { _SQLTypeSize = value; Dirty = true; } }
    public string Url { get { return _Url; } set { _Url = value; Dirty = true; } }
    public string Version { get { return _Version; } set { _Version = value; Dirty = true; } }
    public string ValueTypeSize { get { return _ValueTypeSize;} set { _ValueTypeSize = value; Dirty = true; } }


    

    public Item FromChunk(string chunk) {
      var base1 = chunk.AsBase64Decoded().Parse(" ");
      Id = base1[0].AsInt();
      OwnerId = base1[1].AsInt();
      TypeId = base1[2].AsInt();
      _ItemRank = base1[3].AsInt();
      _NullTypeId = base1[4].AsInt();
      _AccessTypeId = base1[5].AsInt();
      _SQLTypeId = base1[6].AsInt();
      _CSharpTypeId = base1[7].AsInt();
      _MethodTypeId = base1[8].AsInt();
      _ValueTypeId = base1[9].AsInt();

      _IsArrayTypes = base1[10].AsBase64Decoded();
      _BaseClass = base1[11].AsBase64Decoded();
      _Code = base1[12].AsBase64Decoded();
      base.Text = base1[13].AsBase64Decoded();
      _ReturnType = base1[14].AsBase64Decoded();
      _Route = base1[15].AsBase64Decoded();
      _SQLTypeSize = base1[16].AsBase64Decoded();
      _Url = base1[17].AsBase64Decoded();
      _Version = base1[18].AsBase64Decoded();
      _ValueTypeSize = base1[19].AsBase64Decoded();

      if (_IsArrayTypes == "<NULL>") _IsArrayTypes = "";
      if (_BaseClass == "<NULL>") _BaseClass = "";
      if (_Code == "<NULL>") _Code= "";
      if (_ReturnType == "<NULL>") _ReturnType = "";
      if (_SQLTypeSize == "<NULL>") _SQLTypeSize = "";
      if (_Url == "<NULL>") _Url = "";
      if (_Version == "<NULL>") _Version = "";
      if (_ValueTypeSize == "<NULL>") _ValueTypeSize = "";           

      Dirty = false;
      return this;
    }
    public string AsChunk() {

      var aIAT = _IsArrayTypes;
      var aBC =_BaseClass;
      var aC =_Code;      
      var aRT = _ReturnType;
      var aR = _Route;
      var aSTS = _SQLTypeSize;
      var aU = _Url;
      var aV = _Version;
      var aVTS = _ValueTypeSize;

      if (string.IsNullOrEmpty(this._IsArrayTypes)) aIAT = "<NULL>";
      if (string.IsNullOrEmpty(this._BaseClass)) aBC = "<NULL>";
      if (string.IsNullOrEmpty(this._Code)) aC = "<NULL>";
      if (string.IsNullOrEmpty(this._ReturnType)) aRT = "<NULL>";
      if (string.IsNullOrEmpty(this._Route)) aR = "<NULL>";
      if (string.IsNullOrEmpty(this._SQLTypeSize)) aSTS = "<NULL>";
      if (string.IsNullOrEmpty(this._Url)) aU  = "<NULL>";
      if (string.IsNullOrEmpty(this._Version)) aV = "<NULL>";
      if (string.IsNullOrEmpty(this._ValueTypeSize)) aVTS = "<NULL>";
      
      return ($"{Id} {OwnerId} {_TypeId} {_ItemRank} {_NullTypeId} {_AccessTypeId} {_SQLTypeId} {_CSharpTypeId} {_MethodTypeId} {_ValueTypeId} "+
             $"{aIAT.AsBase64Encoded()} {aBC.AsBase64Encoded()} {aC.AsBase64Encoded()} {base.Text.AsBase64Encoded()} {aRT.AsBase64Encoded()} "+
             $"{aR.AsBase64Encoded()} {aSTS.AsBase64Encoded()} {aU.AsBase64Encoded()} {aV.AsBase64Encoded()} {aVTS.AsBase64Encoded()}").AsBase64Encoded();
    }

    public bool CanSwitchDown() {
      if (Parent == null) return false;
      var ImChildNo = Parent.Nodes.IndexOf(this);
      if (ImChildNo < 0) return false;
      return ImChildNo < Parent.Nodes.Count - 1;
    }
    public Item GetSwitchDownItem() {
      if (Parent == null) return null;
      var ImChildNo = Parent.Nodes.IndexOf(this);
      if (ImChildNo < 0) return null;
      if (ImChildNo + 1 <= Parent.Nodes.Count - 1) {
        return ((Item)Parent.Nodes[ImChildNo + 1]);
      }
      return null;
    }
    public bool SwitchRankDown() {
      if (Parent == null) return false;
      var ImChildNo = Parent.Nodes.IndexOf(this);
      if (ImChildNo < 0) return false;
      if (ImChildNo + 1 <= Parent.Nodes.Count - 1) {
        var rank = ItemRank;
        ItemRank = ((Item)Parent.Nodes[ImChildNo + 1]).ItemRank;
        ((Item)Parent.Nodes[ImChildNo + 1]).ItemRank = rank;
        return true;
      }
      return false;
    }

    public bool CanSwitchUp() {
      if (Parent == null) return false;
      return Parent.Nodes.IndexOf(this) >= 1;
    }

    public Item GetSwitchUpItem() {
      if (Parent == null) return null;
      var ImChildNo = Parent.Nodes.IndexOf(this);
      if (ImChildNo < 1) return null;
      return ((Item)Parent.Nodes[ImChildNo - 1]);
    }

    public bool SwitchRankUp() {
      if (Parent == null) return false;
      var ImChildNo = Parent.Nodes.IndexOf(this);
      if (ImChildNo < 1) return false;
      var rank = ItemRank;
      ItemRank = ((Item)Parent.Nodes[ImChildNo - 1]).ItemRank;
      ((Item)Parent.Nodes[ImChildNo - 1]).ItemRank = rank;
      return false;
    }

    public Item AsClone() {
      Item item = new Item();
      return item.FromChunk(AsChunk());
    }


  }

  public class Items : ConcurrentDictionary<int, Item> {
    public Items() : base() { }
    public virtual Boolean Contains(int id) {
      try {
        return !(base[id] is null);
      } catch {
        return false;
      }
    }
    public virtual new Item this[int id] {
      get { return Contains(id) ? base[id] : null; }
      set { if (value != null) { base[id] = value; } else { Remove(id); } }
    }
    public virtual void Remove(int id) { if (Contains(id)) { _ = base.TryRemove(id, out _); } }

    public IEnumerable<Item> GetChildrenItems(int id) {
      return this.Select(x => x.Value).Where(x => x.OwnerId == id).OrderBy(x => x.ItemRank);
    }
    public int GetNextId() {
      int max = 0;
      if (this.Keys.Count > 0) {
        max = this.Select(x => x.Value).Max(x => x.Id);
      }
      return max + 1;
    }

    public ICollection<string> AsList {
      get {
        List<string> retList = new List<string>();
        foreach (Item i in Values) {
          retList.Add(i.AsChunk());
        }
        return retList;
      }
      set {
        base.Clear();
        foreach (var x in value) {
          try { 
            Item n = new Item().FromChunk(x);
            this[n.Id] = n;
          } catch { }
        }
      }
    }
  }

  public class ItemCaster {
    private TreeView _tv;
    private FilePackage _package;
    private Items _items;
    private Form1 _ownerForm;
    private Types _types;
    private bool _inUpdate = false;

    public bool InUpdate {
      get { return _inUpdate; }
      set {
        _inUpdate = value;
        if (!_inUpdate) {
          _package.PackageItems = _items;
          _package.Save();
        }
      }
    }
    public ItemCaster(Form1 ownerform, System.Windows.Forms.TreeView tv, FilePackage package, Types types) {
      _package = package;
      _tv = tv;
      _ownerForm = ownerform;
      _items = package.PackageItems;
      _types = types;
    }

   

    public void LoadTreeviewItemsAsync(TreeView ownerItem) {
      ownerItem.Nodes.Clear();
      IEnumerable<Item> result = _items.GetChildrenItems(0);      
      foreach (Item item in result) {
        ownerItem.Nodes.Add(LoadChildren(item));      
      }
    }

    public Item LoadChildren(Item item) {
      var items = GetOwnersItemsAsync(item.Id);
      if (item.Nodes.Count > 0) item.Nodes.Clear();
      foreach (Item it in items) {
        if (!item.Nodes.Contains(it)) item.Nodes.Add(it);
      }
      item.Dirty = false;
      return item;
    }

    public IEnumerable<Item> GetOwnersItemsAsync(int ownerItemId) {
      IEnumerable<Item> result = _items.GetChildrenItems(ownerItemId);
      List<Item> cloned = new List<Item>();
      foreach (Item i in result) {
        cloned.Add(i.AsClone());
      }
      return cloned;
    }

    public Item GetOwnersItemsAsync(Item ownerItem) {
      foreach (Item item in ownerItem.Nodes) {
        ReloadChildren(LoadChildren(item));
      }
      return ownerItem;
    }

    public Item ReloadChildren(Item child) {
      List<Item> temp = new List<Item>();
      foreach (Item item in child.Nodes) {
        temp.Add(item);
      }
      foreach (Item item in temp) {
        child.Nodes.Remove(item);
        child.Nodes.Add(LoadChildren(item));
      }
      child.Dirty = false;
      return child;
    }

    public Item SaveItem(Item item) {
      if (item == null) return null;
      if (item.Id == 0) {
        item.Id = _items.GetNextId();
      }
      _items[item.Id] = item;
      if (!InUpdate) {
        _package.PackageItems = _items;
        _package.Save();
      }
      item.Dirty = false;
      return item;
    }

    private Item AddSubItemFromType(Item ownerItem, ItemType itemType) {

      int NextRank = ownerItem.Nodes.Count + 1;
      int NextId = _items.GetNextId();
      Item dbs = new Item() {
        Id = NextId,
        OwnerId = ownerItem.Id,
        ItemRank = NextRank,
        TypeId = itemType.TypeId,
        Name = itemType.Name
      };
      if (ownerItem != null) {
        ownerItem.Nodes.Add(dbs);
        ownerItem.Expand();
      }
      SaveItem(dbs);
      var components = _types.GetChildrenItemsNoDef(itemType.TypeId);
      foreach (ItemType i in components) {
        AddSubItemFromType(dbs, i);
      }
      return dbs;
    }

    public Item SaveNewItemFromType(Item ownerItem, ItemType itemType) {
      Item dbs;
      int NextId = _items.GetNextId();
      if (ownerItem == null) {
        dbs = new Item() {
          Id = NextId,
          OwnerId = 0,
          ItemRank = 1,
          TypeId = itemType.TypeId,
          Name = itemType.Name
        };
      } else {
        int NextRank = ownerItem.Nodes.Count + 1;
        dbs = new Item() {
          Id = NextId,
          OwnerId = ownerItem.Id,
          ItemRank = NextRank,          
          TypeId = itemType.TypeId,
          Name = itemType.Name          
        };
      }
      if (dbs == null) return null;
      SaveItem(dbs);
      this.InUpdate = true;
      var components = _types.GetChildrenItemsNoDef(itemType.TypeId);
      foreach (ItemType i in components) {
        AddSubItemFromType(dbs, i);
      }
      if (ownerItem != null) {
        ownerItem.Nodes.Add(dbs);
        ownerItem.Expand();
      }
      this.InUpdate = false;
      return dbs;
    }

    public Item MoveItemSave(Item newOwnerItem, Item DragItem) {
      bool SaveDragged = false;
      if (newOwnerItem == null) {
        if (!_tv.Nodes.Contains(DragItem)) {
          if (DragItem.Parent.Nodes.Contains(DragItem)) {
            DragItem.Parent.Nodes.Remove(DragItem);
          }
          _tv.Nodes.Add(DragItem);
          DragItem.OwnerId = 0;
          SaveDragged = true;
        } else {
        }
      } else {
        if (!newOwnerItem.Nodes.Contains(DragItem)) {
          if (DragItem.Parent.Nodes.Contains(DragItem)) {
            DragItem.Parent.Nodes.Remove(DragItem);
          }
          newOwnerItem.Nodes.Add(DragItem);
          DragItem.OwnerId = newOwnerItem.Id;
          SaveDragged = true;
        }
      }
      if (SaveDragged) SaveItem(DragItem);
      return DragItem;
    }

    public Item SaveNewChildItemsFromText(Item ownerItem, ItemType itemType, string text) {
      Item curParent = ownerItem;
      Item returnItem = ownerItem;
      string[] lines = text.Parse(Environment.NewLine);
      this.InUpdate = true;
      int newItemType = itemType.TypeId;
      if (lines.Count() > 0) {
        foreach (string line in lines) {
          bool goneNested = false;
          if (line.Trim().Last<char>() == ':') {
            newItemType = itemType.TypeId;
            curParent = ownerItem;
            goneNested = true;
          }
          int newID = 0;
          int newItemRank = 0;
          if (curParent != null) {
            newID = curParent.Id;
            newItemRank = curParent.Nodes.Count + 1;
          }
          Item dbs = new Item() {
            Id = _items.GetNextId(),
            OwnerId = newID,
            ItemRank = newItemRank,            
            TypeId = newItemType,
            Name = line
          };
          if (curParent != null) {
            curParent.Nodes.Add(dbs);
          } else {
            _tv.Nodes.Add(dbs);
          }
          SaveItem(dbs);
          returnItem = dbs;

          if (goneNested) {
            curParent = dbs;
          }
        }
        if (!(ownerItem == null)) {
          ownerItem.Expand();
        }

      }
      this.InUpdate = false;
      return returnItem;
    }

    public void NestedRemoveItem(Item item) {
      if (item == null) return;
      if (item.Nodes.Count == 0) {
        _items.Remove(item.Id);
      } else {
        foreach (Item cItem in item.Nodes) {
          NestedRemoveItem(cItem);
        }
        _items.Remove(item.Id);
      }
    }

    public void RemoveItem(Item item) {
      if (item == null) return;
      NestedRemoveItem(item);
      _package.PackageItems = _items;
      _ = Task.Run(async () => await _package.SaveAsync().ConfigureAwait(false));
    }

    public Item GetTablesItem(Item start) { 
      if (start == null) { return null;}
      if (start.TypeId == (int)TnType.Tables) { 
        return start;
      }
      Item ParentItem = (Item)start.Parent;
      if (ParentItem.TypeId == (int)TnType.Tables) { 
        return ParentItem;
      } else { 
        while ((ParentItem != null)&&(ParentItem.TypeId != (int)TnType.Database)) {
          ParentItem = (Item)ParentItem.Parent;
        }
        if (ParentItem.TypeId == (int)TnType.Database){ 
          Item tbls = ParentItem.FirstNode as Item;
          if ((tbls != null)&&(tbls.TypeId == (int)TnType.Tables)) { 
            return tbls;
          }
        }
      }
      return null;
    }

    public Item GetProceduresItem(Item start) {
      if (start == null) { return null; }
      if (start.TypeId == (int)TnType.Procedures) {
        return start;
      }
      Item ParentItem = (Item)start.Parent;
      if (ParentItem.TypeId == (int)TnType.Procedures) {
        return ParentItem;
      } else {
        while ((ParentItem != null) && (ParentItem.TypeId != (int)TnType.Database)) {
          ParentItem = (Item)ParentItem.Parent;
        }
        if (ParentItem.TypeId == (int)TnType.Database) {
          Item tbls = ParentItem.LastNode as Item;
          if ((tbls != null) && (tbls.TypeId == (int)TnType.Procedures)) {
            return tbls;
          }
        }
      }
      return null;
    }

    public Item ParseSqlContentIntoItems(Item ownerItem, string text) {
      try { 
        string indentStr = "  ";
        this.InUpdate = true;   
        Item curParent = ownerItem;
        Item tableItem = null;
        Item procItem = null;
        Item returnItem = ownerItem;

        bool InCreateState = false;
        bool InCreateTableState = false;
        bool InCreateProcState = false;
        bool InCreateProcBody = false;
        bool HasTableName = false;
        bool InTableCreateNameCols = false;
        bool InProcCreateNameParam = false;
        bool HasProcName = false;
        bool NeedsAdvance = true;

        int i =0;
        string curTableName = "";
        string curProcName = "";
        string curColName ="";
        string curColType ="";      
        string curColSize = "";
        string curCode = "";
        bool curColIsConstraint = false;
        bool curColIsIdentity = false;

        int newID = 0;
        int newItemRank = 0;
        var outArr = ParserExt.ParseSqlContent(text);
        int outArrCount = outArr.Count - 1;
        while ((i <= outArrCount) ) {

          if ((!InTableCreateNameCols)&&(!InProcCreateNameParam)) { 

            if ((i <= outArrCount)
              && (!InCreateState) 
              && (outArr[i].OpType == ParserExt.sqlTokenType.Keyword)
              && (outArr[i].Content.ToUpper() == "CREATE")) {
              InCreateState = true;
              InCreateTableState = false;
              InCreateProcState = false;
              HasTableName = false;
              curTableName = "";
              HasProcName = false;
              curProcName = "";
              curCode = outArr[i].Content+" ";
              i++; NeedsAdvance = false;            
            }

            if ((i <= outArrCount)  // alter on procedure interpreted as create
             && (!InCreateState)
             && (outArr[i].OpType == ParserExt.sqlTokenType.Keyword)
             && (outArr[i].Content.ToUpper() == "ALTER")) {
              InCreateState = true;
              InCreateTableState = false;
              InCreateProcState = false;
              HasProcName = false;
              curProcName = "";
              curCode = outArr[i].Content + " ";
              i++; NeedsAdvance = false;
            }

            if ((i <= outArrCount)
              && InCreateState  && (!InCreateTableState)
              && (outArr[i].OpType == ParserExt.sqlTokenType.Keyword)
              && (outArr[i].Content.ToUpper()== "TABLE")) {
              InCreateTableState = true;
              curCode = curCode +outArr[i].Content + " ";
              i++; NeedsAdvance = false;
            }

            if ((i <= outArrCount)
              && InCreateState
              && (!InCreateTableState) && (!InCreateProcState)
              && (outArr[i].OpType == ParserExt.sqlTokenType.Keyword)
              && ((outArr[i].Content.ToUpper() == "PROCEDURE")||(outArr[i].Content.ToUpper() == "PROC"))) {
              InCreateProcState = true;
              curCode = curCode + outArr[i].Content + " ";
              i++; NeedsAdvance = false;
            }

            if ((i <= outArrCount)  // record table Name 
              && (InCreateTableState && InCreateState) && (!HasTableName)           
              && (outArr[i].OpType == ParserExt.sqlTokenType.Identifier)) {
              HasTableName = true;
              curTableName = outArr[i].Content;
              curCode = curCode + outArr[i].Content + " ";
              i++; NeedsAdvance = false;
            }

            if ((i <= outArrCount) // record Procedure Name and make the node.
              && (InCreateProcState && InCreateState) && (!HasProcName) 
              && (outArr[i].OpType == ParserExt.sqlTokenType.Identifier)) {
              InProcCreateNameParam = true;
              HasProcName = true;
              curProcName = outArr[i].Content;
              curCode = curCode + outArr[i].Content + " ";

              newID = curParent.Id;
              newItemRank = curParent.Nodes.Count + 1;
              procItem = new Item() {
                Id = _items.GetNextId(),
                OwnerId = newID,
                ItemRank = newItemRank,
                TypeId = (int)TnType.Procedure,
                Name = curProcName,
                ValueTypeId = 0,
                ValueTypeSize = "",
                Code = curCode
              };
              curParent = GetProceduresItem(curParent);
              if (curParent != null) {
                curParent.Nodes.Add(procItem);
              }
              SaveItem(procItem);
              curCode = "";
              i++; NeedsAdvance = false;
            }

            if ((i <= outArrCount) // Paran start and Make the table node.
              && HasTableName  &&  (InCreateTableState)             
              && (outArr[i].OpType == ParserExt.sqlTokenType.ParanStart) ) {          
            
              curCode = curCode + " (";           
              newID = curParent.Id;
              newItemRank = curParent.Nodes.Count + 1;          
              tableItem = new Item() {
                Id = _items.GetNextId(),
                OwnerId = newID,
                ItemRank = newItemRank,
                TypeId = (int)TnType.Table,
                Name = curTableName,
                ValueTypeId = 0,
                ValueTypeSize = "",
                Code = curCode
              };
              curParent = GetTablesItem(curParent);
              if (curParent != null) {
                curParent.Nodes.Add(tableItem);
              }
              SaveItem(tableItem);

              InTableCreateNameCols = true;
              i++; NeedsAdvance = false;
              curCode = "";
            }
          } 
          if (InTableCreateNameCols) { // table create name the cols 

            if ((i <= outArrCount)
            && outArr[i].OpType == ParserExt.sqlTokenType.Keyword
            ) {
              if (outArr[i].Content.ToUpper() == "CONSTRAINT") {
                curColIsConstraint = true;
              }
              if (outArr[i].Content.ToUpper() == "IDENTITY") { 
                curColIsIdentity = true;
              }
              curColType += " " + outArr[i].Content;
              curCode = curCode + outArr[i].Content + " ";
              i++; NeedsAdvance = false;
            }

            if ((i <= outArrCount) 
              && outArr[i].OpType == ParserExt.sqlTokenType.Identifier
              ) { 
              curColName = outArr[i].Content;
              curCode = curCode + outArr[i].Content + " ";
              i++; NeedsAdvance = false;
            }

            if ((i <= outArrCount) 
              && outArr[i].OpType == ParserExt.sqlTokenType.ColType
              ) {
              curColType = outArr[i].Content;
              curCode = curCode + outArr[i].Content + " ";
              i++; NeedsAdvance = false;            
            }

            if ((i <= outArrCount) && outArr[i].OpType == ParserExt.sqlTokenType.ParanStart) {
              string aSize = "("; var depth = 1; i++;
              while (i <= outArrCount && (outArr[i].OpType != ParserExt.sqlTokenType.ParanEnd) && (depth > 0)) {
                if ((i <= outArrCount) && outArr[i].OpType == ParserExt.sqlTokenType.ParanStart) {
                  depth++;
                }
                if ((i <= outArrCount) && outArr[i].OpType == ParserExt.sqlTokenType.ParanEnd) {
                  depth--;
                }
                aSize = aSize + outArr[i].Content;
                i++; NeedsAdvance = false;
              }
              if ((i<= outArrCount) &&(outArr[i].OpType == ParserExt.sqlTokenType.ParanEnd)) {
                aSize = aSize + outArr[i].Content;
                i++; NeedsAdvance = false;
              }
              curCode = curCode + aSize + " ";
              if (!curColIsIdentity) {
                curColSize = aSize;
              }
            }

            if ((i <= outArrCount) 
             && outArr[i].OpType == ParserExt.sqlTokenType.Keyword
             ) {
              if (outArr[i].Content.ToUpper() == "IDENTITY") {
                curColIsIdentity = true;
              }
              curColType += " "+outArr[i].Content;
              curCode = curCode + outArr[i].Content + " ";
              i++; NeedsAdvance = false;
            }

            if ((i <= outArrCount) 
              && outArr[i].OpType == ParserExt.sqlTokenType.Comma
              ) {
              if (!curColIsConstraint) { 
                curCode = curCode.Trim() + ",";
                if (tableItem != null) {
                  newID = tableItem.Id;
                  newItemRank = tableItem.Nodes.Count + 1;
                  var ColumnItem = new Item() {
                    Id = _items.GetNextId(),
                    OwnerId = newID,
                    ItemRank = newItemRank,
                    TypeId = (int)TnType.TableColumn,
                    Name = curColName,
                    SQLTypeId = curColType.SqlTypeToLookupId(),
                    SQLTypeSize = curColSize,
                    Code = curCode                
                  };
                  if (tableItem != null) {
                      tableItem.Nodes.Add(ColumnItem);
                  }
                  SaveItem(ColumnItem);
                }
                i++; NeedsAdvance = false;
                curCode = "";
                curColSize = "";
                curColIsConstraint = false;
                curColIsIdentity = false;
              }
            }

            if ((i <= outArrCount) 
              && outArr[i].OpType == ParserExt.sqlTokenType.ParanEnd
              ) {
              if (!curColIsConstraint) { 
                curCode = curCode.Trim() ;
                if (tableItem != null) {
                  newID = tableItem.Id;
                  newItemRank = tableItem.Nodes.Count + 1;
                  var ColumnItem = new Item() {
                    Id = _items.GetNextId(),
                    OwnerId = newID,
                    ItemRank = newItemRank,
                    TypeId = (int)TnType.TableColumn,
                    Name = curColName,
                    SQLTypeId = curColType.SqlTypeToLookupId(),
                    SQLTypeSize = curColSize,
                    Code = curCode
                  };
                  if (tableItem != null) {
                    tableItem.Nodes.Add(ColumnItem);
                  }
                  SaveItem(ColumnItem);
                }
              }
              i++; NeedsAdvance = false;
              InCreateState = false;
              curCode = "";
              curColSize = "";
              InTableCreateNameCols = false;
              curColIsConstraint = false;
              curColIsIdentity = false;
            }                
          }
          if (InProcCreateNameParam) { // if in stored proc name the columns section

            if ((i <= outArrCount) && outArr[i].OpType == ParserExt.sqlTokenType.ParanStart) {
              string aSize = "(";             
              curCode = curCode + aSize + " ";
              i++; NeedsAdvance = false;
            }

            if ((i <= outArrCount) && outArr[i].OpType == ParserExt.sqlTokenType.Identifier ) {
              curColName = outArr[i].Content;
              curCode = curCode + outArr[i].Content + " ";
              i++; NeedsAdvance = false;
            }

            if ((i <= outArrCount) && outArr[i].OpType == ParserExt.sqlTokenType.ColType ) {
              curColType = outArr[i].Content;
              curCode = curCode + outArr[i].Content + " ";
              i++; NeedsAdvance = false;
            }

            if ((i <= outArrCount) && outArr[i].OpType == ParserExt.sqlTokenType.ParanStart) {
              string aSize = "("; var depth = 1; i++;
              while (i <= outArrCount && (outArr[i].OpType != ParserExt.sqlTokenType.ParanEnd) && (depth > 0)) {
                if ((i <= outArrCount) && outArr[i].OpType == ParserExt.sqlTokenType.ParanStart) {
                  depth++;
                }
                if ((i <= outArrCount) && outArr[i].OpType == ParserExt.sqlTokenType.ParanEnd) {
                  depth--;
                }
                aSize = aSize + outArr[i].Content;
                i++; NeedsAdvance = false;
              }
              if ((i <= outArrCount) && (outArr[i].OpType == ParserExt.sqlTokenType.ParanEnd)) {
                aSize = aSize + outArr[i].Content;
                i++; NeedsAdvance = false;
              }
              curCode = curCode + aSize + " ";
              curColSize = aSize;            
            }

            if ((i <= outArrCount) && outArr[i].OpType == ParserExt.sqlTokenType.Comma ) {            
              curCode = curCode.Trim() + ",";
              if (procItem != null) {
                newID = procItem.Id;
                newItemRank = procItem.Nodes.Count + 1;
                var ColumnItem = new Item() {
                  Id = _items.GetNextId(),
                  OwnerId = newID,
                  ItemRank = newItemRank,
                  TypeId = (int)TnType.ProcedureParam,
                  Name = curColName,
                  SQLTypeId = curColType.SqlTypeToLookupId(),
                  SQLTypeSize = curColSize,
                  Code = curCode
                };
                if (procItem != null) {
                  procItem.Nodes.Add(ColumnItem);
                }
                SaveItem(ColumnItem);
              }
              i++; NeedsAdvance = false;
              curCode = "";
              curColSize = "";
              curColIsConstraint = false;
              curColIsIdentity = false;            
            }

            if ((i <= outArrCount) && outArr[i].OpType == ParserExt.sqlTokenType.ParanEnd) {
              string aSize = ")";
              curCode = curCode + aSize + " ";
              i++; NeedsAdvance = false;
            }

            if ((i <= outArrCount) && outArr[i].OpType == ParserExt.sqlTokenType.Keyword
                && (outArr[i].Content.ToUpper() == "AS")) {
            
              if (procItem != null) {
                newID = procItem.Id;
                newItemRank = procItem.Nodes.Count + 1;
                var ColumnItem = new Item() {
                  Id = _items.GetNextId(),
                  OwnerId = newID,
                  ItemRank = newItemRank,
                  TypeId = (int)TnType.ProcedureParam,
                  Name = curColName,
                  SQLTypeId = curColType.SqlTypeToLookupId(),
                  SQLTypeSize = curColSize,
                  Code = curCode
                };
                if (procItem != null) {
                  procItem.Nodes.Add(ColumnItem);
                }
                SaveItem(ColumnItem);
              }

              curCode = "";
              i++; NeedsAdvance = false;            
              curColSize = "";
              curColName = "";            
              curColIsConstraint = false;
              curColIsIdentity = false;
              InCreateProcBody = true;
              InProcCreateNameParam = false;
            }
          }
          if (InCreateProcBody) {
            while ((i <= outArrCount ) && InCreateProcBody) { 
              if (outArr[i].OpType == ParserExt.sqlTokenType.NewLine) {
                curCode = curCode.TrimEnd() + outArr[i].Content+Ext.MakeIndentSpace(1, indentStr);
              } else { 
                curCode = curCode + outArr[i].Content+" ";
              }

              if ((i <= outArrCount) && (outArr[i].OpType == ParserExt.sqlTokenType.Keyword) && 
                ((outArr[i].Content.ToUpper() == "BEGIN")|| (outArr[i].Content.ToUpper() == "WHEN"))) {
                var depth = 1; i++;
                while ( (i <= outArrCount)                 
                  && ((outArr[i].Content.ToUpper() != "END")) && (depth > 0)) {

                  if ((i <= outArrCount) && (outArr[i].OpType == ParserExt.sqlTokenType.Keyword) && 
                    ((outArr[i].Content.ToUpper() == "BEGIN") || (outArr[i].Content.ToUpper() == "WHEN"))) {
                    depth++;
                  }
                  if ((i <= outArrCount) && (outArr[i].OpType == ParserExt.sqlTokenType.Keyword) && (outArr[i].Content.ToUpper() == "END")) {
                    depth--;
                  }
                  if (outArr[i].OpType == ParserExt.sqlTokenType.NewLine) {
                    curCode = curCode.TrimEnd() + outArr[i].Content+Ext.MakeIndentSpace(depth+1, indentStr); ;
                  } else {
                    curCode = curCode + outArr[i].Content + " ";
                  }
                  i++; NeedsAdvance = false;
                }
                if ((i <= outArrCount) && (outArr[i].OpType == ParserExt.sqlTokenType.Keyword)
                 && (outArr[i].Content.ToUpper() == "END")) {
                  curCode = curCode + outArr[i].Content + " ";
                  i++; NeedsAdvance = false;
                }
              }
            
              if ((i <= outArrCount) && (outArr[i].OpType == ParserExt.sqlTokenType.Keyword)
                 &&  (outArr[i].Content.ToUpper() == "RETURN")) {
                curCode = curCode + outArr[i].Content + " ";
                InCreateProcBody = false; NeedsAdvance = false;
              }

              if (NeedsAdvance) {
                i++;
              } else {
                NeedsAdvance = true;
              }
            }

            if (procItem != null) {
              newID = procItem.Id;
              newItemRank = procItem.Nodes.Count + 1;
              var ColumnItem = new Item() {
                Id = _items.GetNextId(),
                OwnerId = newID,
                ItemRank = newItemRank,
                TypeId = (int)TnType.ProcBody,
                Name = "Body",
                SQLTypeId = curColType.SqlTypeToLookupId(),                
                Code = curCode
              };
              if (procItem != null) {
                procItem.Nodes.Add(ColumnItem);
              }
              SaveItem(ColumnItem);
            }
          }
        
          if (NeedsAdvance ) {
            i++;
          } else {
            NeedsAdvance = true;
          }
        }    // end of while

        if (!(ownerItem == null)) {
          ownerItem.Expand();
        }
        this.InUpdate = false;

      } catch (Exception ex) {
        _ownerForm.LogMsg($"Parse Error: {ex.Message} {ex?.InnerException?.Message ?? ""}");
      }
      return ownerItem;
    }
      

  }
}
