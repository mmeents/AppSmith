using System;
using AppSmith.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.CompilerServices;
using System.IO;
using System.Configuration;
using PropertyGridEx;

namespace AppSmith {

  public partial class Form1 : Form {

    #region initialization
    private SettingsFile _settingsPack;
    private Settings _settings;
    private string _defaultSettings = "";
    private string _defaultDir = "";
    private bool _modelActive = false;

    private string _fileName;
    private string _folder;
    private Types _types;
    private FilePackage _filePackage;
    private ItemCaster _itemCaster;
    private Item _inEditItem = null;
    private bool _inReorder = false;
    public Form1() {
      InitializeComponent();
      LogMsg("Hello world");
      _types = new Types();
      _types.Load();
      _defaultDir = _defaultDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\PrompterFiles";
      if (!Directory.Exists(_defaultDir)) {
        Directory.CreateDirectory(_defaultDir);
      }      
      _defaultSettings = _defaultDir + "\\AppSmithSettings.sft";
      _settingsPack = new SettingsFile(_defaultSettings, this);
      tvBuilder.ContextMenuStrip = null;
      if (!props.Enabled) props.Enabled = false;
    }
    #endregion
    #region Logging and Progress 
    delegate void SetLogMsgCallback(string msg);
    public void LogMsg(string msg) {      
      if (this.edLogMsg.InvokeRequired) {
        SetLogMsgCallback d = new SetLogMsgCallback(LogMsg);
        this.BeginInvoke(d, new object[] { msg });
      } else {
        if (!edLogMsg.Visible) edLogMsg.Visible = true;
        this.edLogMsg.Text = msg + Environment.NewLine + edLogMsg.Text;
      }
    }
    public void SetInProgress(int ProgressPercent) {
      if (ProgressPercent == 0) {
        if (pbMain.Visible) pbMain.Visible = false;
        if (!tvBuilder.Enabled) tvBuilder.Enabled = true;
      } else {
        if (!pbMain.Visible) pbMain.Visible = true;
        if (!tvBuilder.Enabled) tvBuilder.Enabled = false;
      }
      System.Windows.Forms.Application.DoEvents();
      pbMain.Value = ProgressPercent;
      System.Windows.Forms.Application.DoEvents();
    }
    #endregion
    #region Location saving and app settings
    private void Form1_Resize(object sender, EventArgs e) {
      SaveLocationSettings();
    }
    private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
      SaveLocationSettings();
    }
    private void SetLocationSettings() {
      if (_settings.Contains("FormTop")) {
        this.Top = _settings["FormTop"].Value.AsInt();
      }
      if (_settings.Contains("FormLeft")) {
        this.Left = _settings["FormLeft"].Value.AsInt();
      }
      if (_settings.Contains("FormHeight")) {
        this.Height = _settings["FormHeight"].Value.AsInt();
      }
      if (_settings.Contains("FormWidth")) {
        this.Width = _settings["FormWidth"].Value.AsInt();
      }
      if (_settings.Contains("Split1")) {
        this.splitContainer1.SplitterDistance = _settings["Split1"].Value.AsInt();
      }
      if (_settings.Contains("Split2")) {
        this.splitContainer2.SplitterDistance = _settings["Split2"].Value.AsInt();
      }
    }

    private void SaveLocationSettings() {
      if (_settings == null) return;
      if (!_settings.Contains("FormTop")) {
        _settings["FormTop"] = new SettingProperty() { Key = "FormTop", Value = this.Top.AsString() };
      } else {
        _settings["FormTop"].Value = this.Top.AsString();
      }

      if (!_settings.Contains("FormLeft")) {
        _settings["FormLeft"] = new SettingProperty() { Key = "FormLeft", Value = this.Left.AsString() };
      } else {
        _settings["FormLeft"].Value = this.Left.AsString();
      }

      if (!_settings.Contains("FormHeight")) {
        _settings["FormHeight"] = new SettingProperty() { Key = "FormHeight", Value = this.Height.AsString() };
      } else {
        _settings["FormHeight"].Value = this.Height.AsString();
      }

      if (!_settings.Contains("FormWidth")) {
        _settings["FormWidth"] = new SettingProperty() { Key = "FormWidth", Value = this.Width.AsString() };
      } else {
        _settings["FormWidth"].Value = this.Width.AsString();
      }

      if (!_settings.Contains("Split1")) {
        _settings["Split1"] = new SettingProperty() { Key = "Split1", Value = this.splitContainer1.SplitterDistance.AsString() };
      } else {
        _settings["Split1"].Value = this.splitContainer1.SplitterDistance.AsString();
      }

      if (!_settings.Contains("Split2")) {
        _settings["Split2"] = new SettingProperty() { Key = "Split2", Value = this.splitContainer2.SplitterDistance.AsString() };
      } else {
        _settings["Split2"].Value = this.splitContainer2.SplitterDistance.AsString();
      }
      _settingsPack.Settings = _settings;
      _settingsPack.Save();
    }

    private void Form1_Shown(object sender, EventArgs e) {
      _settings = _settingsPack.Settings;
      SetLocationSettings();
    }
    #endregion
    #region Main Menu opening and handlers
    private void fileToolStripMenuItem_DropDownOpening(object sender, EventArgs e) {
      openStripMenuItem.Enabled = !_modelActive;
      closeStripMenuItem.Enabled = _modelActive;
    }
    private void openStripMenuItem_Click(object sender, EventArgs e) {
      if (lbFileName.Text == "No File Open") {
        odMain.InitialDirectory = _defaultDir;
      } else {
        string s = lbFileName.Text.ParseLast("\\");
        odMain.InitialDirectory = lbFileName.Text.Substring(0, lbFileName.Text.Length - s.Length);
      }
      DialogResult res = odMain.ShowDialog();
      if (res == DialogResult.OK) {
        try { 
          _settings = _settingsPack.Settings;
          _fileName = odMain.FileName;
          if (!_settings.Contains("MRUL")) {
            _settings["MRUL"] = new SettingProperty() { Key = "MRUL", Value = _fileName };
          } else { 
            _settings["MRUL"].Value = _fileName + Environment.NewLine + _settings["MRUL"].Value;
          }
          _settingsPack.Settings = _settings;
          _settingsPack.Save();
          lbFileName.Text = _fileName;        
          _folder = _fileName.Substring(0, _fileName.Length - (_fileName.ParseLast("\\").Length + 1));
          if (!Directory.Exists(_folder)) {
            Directory.CreateDirectory(_folder);
          }
          SetInProgress(3000);
          _filePackage = new FilePackage(_fileName, this);
          SetInProgress(6000);
          _itemCaster = new ItemCaster(this, tvBuilder, _filePackage, _types);
          _itemCaster.LoadTreeviewItemsAsync(tvBuilder);
          _modelActive = true;
          tvBuilder.ContextMenuStrip = msBuilder;
          if (!props.Enabled) props.Enabled = true;
        } catch(Exception ex) {
          LogMsg(ex.Message);
        }
        SetInProgress(0);
      }
    }
    private void closeStripMenuItem_Click(object sender, EventArgs e) {
      lbFileName.Text = "No File Open";
      _modelActive = false;
      tvBuilder.Nodes.Clear();
      tvBuilder.Enabled = false;
      tvBuilder.ContextMenuStrip = null;
      props.Item.Clear();
      props.Enabled = false;
    }
    #endregion
    #region tree View right click menu and handlers 
    private void addTemplateToolStripMenuItem_DropDownOpening(object sender, EventArgs e) {      
      if (_inEditItem == null) {      
        
        if (!AddServerToolStripMenuItem.Enabled) AddServerToolStripMenuItem.Enabled = true;
        if (AddDatabaseToolStripMenuItem.Enabled) AddDatabaseToolStripMenuItem.Enabled = false;
        if (AddTableToolStripMenuItem.Enabled) AddTableToolStripMenuItem.Enabled = false;
        if (apiToolStripMenuItem.Enabled) apiToolStripMenuItem.Enabled = false;
        if (controllerToolStripMenuItem.Enabled) controllerToolStripMenuItem.Enabled = false;
        if (meToolStripMenuItem.Enabled) meToolStripMenuItem.Enabled = false;
        if (columnToolStripMenuItem.Enabled) columnToolStripMenuItem.Enabled = false;        
        if (deleteToolStripMenuItem.Enabled) deleteToolStripMenuItem.Enabled = false;
        
      } else {

        if (AddServerToolStripMenuItem.Enabled) AddServerToolStripMenuItem.Enabled = false;
        if (_inEditItem.TypeId == (int)TnType.Server) {
          if (!AddDatabaseToolStripMenuItem.Enabled) AddDatabaseToolStripMenuItem.Enabled = true;
          if (!apiToolStripMenuItem.Enabled) apiToolStripMenuItem.Enabled = true;
        } else {
          if (AddDatabaseToolStripMenuItem.Enabled) AddDatabaseToolStripMenuItem.Enabled = false;
          if (apiToolStripMenuItem.Enabled) apiToolStripMenuItem.Enabled = false;
        } 
        if (_inEditItem.TypeId == (int)TnType.Api) {
          if (!controllerToolStripMenuItem.Enabled) controllerToolStripMenuItem.Enabled = true;          
        } else {
          if (controllerToolStripMenuItem.Enabled) controllerToolStripMenuItem.Enabled = false;          
        }
        if(_inEditItem.TypeId == (int)TnType.Controller) {          
          if (!meToolStripMenuItem.Enabled) meToolStripMenuItem.Enabled = true;
        } else {          
          if (meToolStripMenuItem.Enabled) meToolStripMenuItem.Enabled = false;
        }

        if (_inEditItem.TypeId == (int)TnType.Tables) {
          if (!AddTableToolStripMenuItem.Enabled) AddTableToolStripMenuItem.Enabled = true;
        } else {
          if (AddTableToolStripMenuItem.Enabled) AddTableToolStripMenuItem.Enabled = false;
        }
        if (_inEditItem.TypeId == (int)TnType.Table) {
          if (!columnToolStripMenuItem.Enabled) columnToolStripMenuItem.Enabled = true;
        } else {
          if (columnToolStripMenuItem.Enabled) columnToolStripMenuItem.Enabled = false;
        }
        if (_inEditItem.CanSwitchUp()) {
          if(!moveUpToolStripMenuItem.Enabled) moveUpToolStripMenuItem.Enabled = true;
        } else {
          if (moveUpToolStripMenuItem.Enabled) moveUpToolStripMenuItem.Enabled = false;
        }
        if (_inEditItem.CanSwitchDown()) {
          if (!moveDownToolStripMenuItem.Enabled) moveDownToolStripMenuItem.Enabled = true;
        } else {
          if (moveDownToolStripMenuItem.Enabled) moveDownToolStripMenuItem.Enabled = false;
        }
        if (!deleteToolStripMenuItem.Enabled) deleteToolStripMenuItem.Enabled = true;
      }      
    }
    private void moveDownToolStripMenuItem_Click(object sender, EventArgs e) {
      if (_inEditItem == null) return;
      if (!_inEditItem.CanSwitchDown()) return;
      var otherItem = _inEditItem.GetSwitchDownItem();
      _inEditItem.SwitchRankDown();
      if (_inEditItem.Dirty) { _itemCaster.SaveItem(_inEditItem); }
      if (otherItem != null && otherItem.Dirty) _itemCaster.SaveItem(otherItem);
      var opItem = _inEditItem;
      _inReorder = true;
      try {
        var parentItem = (Item)_inEditItem.Parent;
        var inEditIndex = parentItem.Nodes.IndexOf(opItem);
        if (inEditIndex >= 0) {
          parentItem.Nodes.RemoveAt(inEditIndex);
          parentItem.Nodes.Insert(inEditIndex + 1, opItem);
        }
      } finally {
        _inReorder = false;
      }
      tvBuilder.SelectedNode = opItem;
    }
    private void moveUpToolStripMenuItem_Click(object sender, EventArgs e) {
      if (_inEditItem == null) return;
      if (!_inEditItem.CanSwitchUp()) return;
      var otherItem = _inEditItem.GetSwitchUpItem();
      _inEditItem.SwitchRankUp();
      if (_inEditItem.Dirty) { _itemCaster.SaveItem(_inEditItem); }
      if (otherItem != null && otherItem.Dirty) _itemCaster.SaveItem(otherItem);

      var opItem = _inEditItem;
      _inReorder = true;
      try {
        var parentItem = (Item)_inEditItem.Parent;
        var otherItemIndex = parentItem.Nodes.IndexOf(otherItem);
        if (otherItemIndex >= 0) {
          parentItem.Nodes.Remove(opItem);
          parentItem.Nodes.Insert(otherItemIndex, opItem);
        }
      } finally {
        _inReorder = false;
      }
      tvBuilder.SelectedNode = opItem;
    }
    private void AddServerToolStripMenuItem_Click(object sender, EventArgs e) {
      _ = _itemCaster.SaveNewChildItemsFromText(_inEditItem, _types[(int)TnType.Server], "Server");
    }
    private void AddDatabaseToolStripMenuItem_Click(object sender, EventArgs e) {
      var dbItem = _itemCaster.SaveNewChildItemsFromText(_inEditItem, _types[(int)TnType.Database], "Database");
      _itemCaster.SaveNewChildItemsFromText(dbItem, _types[(int)TnType.Tables], "Tables");
      _itemCaster.SaveNewChildItemsFromText(dbItem, _types[(int)TnType.Procedure], "Procedures");
    }

    private void apiToolStripMenuItem_Click(object sender, EventArgs e) {
      var dbItem = _itemCaster.SaveNewChildItemsFromText(_inEditItem, _types[(int)TnType.Api], "Api");
      _itemCaster.SaveNewChildItemsFromText(dbItem, _types[(int)TnType.Controller], "Controller1");
    }


    private void AddTableToolStripMenuItem_Click(object sender, EventArgs e) {
      var tblItem = _itemCaster.SaveNewChildItemsFromText(_inEditItem, _types[(int)TnType.Table], "Table");
      _ = _itemCaster.SaveNewChildItemsFromText(tblItem, _types[(int)TnType.TableColumn], "Id");
    }
    private void columnToolStripMenuItem_Click(object sender, EventArgs e) {
      if (_inEditItem == null) return;
      if (_inEditItem.TypeId == (int)TnType.Table) { 
        _ = _itemCaster.SaveNewChildItemsFromText(_inEditItem, _types[(int)TnType.TableColumn], "Column");
      } else if (_inEditItem.TypeId == (int)TnType.View) {
        _ = _itemCaster.SaveNewChildItemsFromText(_inEditItem, _types[(int)TnType.ViewColumn], "Column");
      } 
    }

    private void deleteToolStripMenuItem_Click(object sender, EventArgs e) {
      if (_inEditItem == null) return;
      try {
        Item item = _inEditItem;
        Item ParentItem = (Item)item.Parent;
        SetInProgress(9500);
        if (ParentItem != null) {
          ParentItem.Nodes.Remove(item);
          tvBuilder.SelectedNode = ParentItem;
        }
        _itemCaster.RemoveItem(item);
      } finally {
        SetInProgress(0);
      }
    }

    private void controllerToolStripMenuItem_Click(object sender, EventArgs e) {
      _itemCaster.SaveNewChildItemsFromText(_inEditItem, _types[(int)TnType.Controller], "Controller");
    }

    private void meToolStripMenuItem_Click(object sender, EventArgs e) {
      _itemCaster.SaveNewChildItemsFromText(_inEditItem, _types[(int)TnType.Method], "Method");
    }

    #endregion

    private void tvBuilder_BeforeExpand(object sender, TreeViewCancelEventArgs e) {
      Item tn = (Item)e.Node;
      if (tn != null) {
        var items = _itemCaster.GetOwnersItemsAsync(tn);
        e.Cancel = false;
      }
    }

    private void tvBuilder_AfterSelect(object sender, TreeViewEventArgs e) {
      if (!_inReorder) {
        try {
          if (e?.Node == null) return;
          _inEditItem = (Item)e.Node;
          if (_inEditItem == null) return;
          SetInProgress(9500);
          ResetPropEditors(_inEditItem);
          PopulateEditors(_inEditItem);
        } finally {
          SetInProgress(0);
        }
      }
    }

    private void ResetPropEditors(Item item) {
      props.SelectedObject = item;
      props.ShowCustomProperties = true;

      props.Item.Clear();
      if (item != null) {

        if (
           item.TypeId == (int)TnType.Server ||
           item.TypeId == (int)TnType.Database ||
           item.TypeId == (int)TnType.Api ||
           item.TypeId == (int)TnType.Controller ||
           item.TypeId == (int)TnType.Table ||
           item.TypeId == (int)TnType.View ||
           item.TypeId == (int)TnType.Procedure ||
           item.TypeId == (int)TnType.Function ||
           item.TypeId == (int)TnType.Method ||
           item.TypeId == (int)TnType.TableColumn ||
           item.TypeId == (int)TnType.ViewColumn ||
           item.TypeId == (int)TnType.ProcedureParam ||
           item.TypeId == (int)TnType.FunctionParam
        ) {
           ItemType it = _types[item.TypeId];
           ItemType itCat = _types[it.CatagoryTypeId];
           var cp = new PropertyGridEx.CustomProperty("Name", item.Text, it.Readonly, itCat.Text, it.Desc, it.Visible);
           props.Item.Add(cp);
        }

        if (
            item.TypeId == (int)TnType.TableColumn ||
            item.TypeId == (int)TnType.ViewColumn ||
            item.TypeId == (int)TnType.ProcedureParam ||
            item.TypeId == (int)TnType.FunctionParam
        ) {
          var al = _types.GetChildrenItems(29).ToArray<ItemType>();
          ItemType it = _types[item.TypeId];
          ItemType itCat = _types[it.CatagoryTypeId];
          ItemType itEditor = _types[it.EditorTypeId];
          var customProperty = new PropertyGridEx.CustomProperty() {
            Name = "Type", Category = itCat.Text, Description = it.Desc, Value = _types[item.ValueTypeId].Name,
            DisplayMember = "Name", ValueMember = "TypeId", Datasource = al, Visible = true, IsReadOnly = false, IsDropdownResizable = true
          };
          props.Item.Add(customProperty);

          var cp = new PropertyGridEx.CustomProperty("Size", item.ValueTypeSize, false, itCat.Text, "column size example (max)", it.Visible);
          props.Item.Add(cp);

          var cp1 = new PropertyGridEx.CustomProperty("Code", item.Code, false, itCat.Text, "", it.Visible);
          props.Item.Add(cp1);

        }

      }

      props.Refresh();
      props.MoveSplitterTo(Convert.ToInt32(props.Width * 0.2));

    }

    private void props_PropertyValueChanged(object s, PropertyValueChangedEventArgs e) {
      if (_inEditItem != null) {
        foreach (CustomProperty y in props.Item) {
          if (y.Name == "Name") {
            if (Convert.ToString(y.Value) != _inEditItem.Name) {
              _inEditItem.Text = Convert.ToString(y.Value);
            }
          } else if (y.Name == "Type") {
            if (Convert.ToInt32(y.SelectedValue) != _inEditItem.ValueTypeId) {
              _inEditItem.ValueTypeId = Convert.ToInt32(y.SelectedValue);

            }
          } else if (y.Name == "Size") { 
            if (y.Value.AsString() != _inEditItem.ValueTypeSize) {
              _inEditItem.ValueTypeSize = y.Value.AsString();
            }
          } else if (y.Name == "Code") {
            if (y.Value.AsString() != _inEditItem.Code) {
              _inEditItem.Code = y.Value.AsString();
            }
          }
        }
        if (_inEditItem.Dirty) {
          try {
            SetInProgress(9500);
            _itemCaster.SaveItem(_inEditItem);
            PopulateEditors(_inEditItem);
          } finally {
            SetInProgress(0);
          }
        }
      }
    }

    public void PopulateEditors(Item it) {
      if (it == null) {
        lbFocusedItem.Text = string.Empty;
         return;
      }
      lbFocusedItem.Text = it.Text;
      switch (it.TypeId) {
        case (int)TnType.Table: PrepareTableType(it); break;
      //  case (int)TnType.Procedure: PrepareProcedureType(it); break;
      //  case (int)TnType.Function: PrepareFunctionType(it); break;
        default: PrepareNullType(it); break;
      }
    }

    public void PrepareNullType(Item it) {
      if (it == null) return;
      edSQL.Text = $"-- {it.Text} Sql not implemented yet.";
      edCSharp.Text = $"// {it.Text} C not implemented ";
    }
    public void PrepareTableType(Item it) {
      if (it == null) return;
      edSQL.Text = " " + Cs.nl + it.GenerateSqlCreateTable(_types) + Cs.nl + Cs.nl + it.GenerateSQLAddUpdateStoredProc(_types) + it.GetSQLCursor(_types);
      edCSharp.Text = Cs.nl + it.GenerateCSharpRepoLikeClassFromTable(_types, true);
    }

    public void PrepareProcedureType(Item tnProcedure) {
    //  edSQL.Text = tnProcedure.GenerateSQLStoredProc(_types);
    //  edCSharp.Text = tnProcedure.GenerateCSharpExecStoredProc(_types);
    }

    public void PrepareFunctionType(Item tnFunction) {
    //  edSQL.Text = tnFunction.GenerateSQLFunction(_types);
    //  edCSharp.Text = "// C not impemented";
    }

    private void inputClearToolStripMenuItem_Click(object sender, EventArgs e) {
      edInput.Text = "";
    }

    private void inputCopyToolStripMenuItem_Click(object sender, EventArgs e) {
      if (edInput.SelectedText.Length == 0) edInput.SelectAll();
      Clipboard.SetText(edInput.SelectedText);
    }

    private void inputPasteToolStripMenuItem_Click(object sender, EventArgs e) {
      string x = Clipboard.GetText();
      string[] lines = x.Parse(Environment.NewLine);
      StringBuilder sb = new StringBuilder();
      foreach (string line in lines) {
        if (line.Length > 0) sb.AppendLine(line);
      }
      if (edInput.SelectedText.Length > 0) {
        edInput.SelectedText = sb.ToString();
      } else edInput.Text = edInput.Text + sb.ToString();
    }
    private void inputParseToolStripMenuItem_Click(object sender, EventArgs e) {
      string sInput = edInput.Text;
      _itemCaster.SaveNewChildItemsFromSqlTblCreate(_inEditItem, sInput);     
    }


    private void msOutput_Click(object sender, EventArgs e) {
    }

    private void toolStripMenuItem3_Click(object sender, EventArgs e) {
      if (tabControl1.SelectedTab == tpSqlOut ) {
        if (edSQL.SelectedText.Length == 0) edSQL.SelectAll();
        Clipboard.SetText(edSQL.SelectedText);
      } else if (tabControl1.SelectedTab == tpCOut) {
        if (edCSharp.SelectedText.Length == 0) edCSharp.SelectAll();
        Clipboard.SetText(edCSharp.SelectedText);
      }

    }

    private void msBuilder_Opening(object sender, CancelEventArgs e) {

    }


  }

}
