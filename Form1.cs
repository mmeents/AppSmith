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
using Microsoft.OpenApi.Models;
using System.Security.AccessControl;

namespace AppSmith {

  public partial class Form1 : Form {

    #region initialization
    private SettingsFile _settingsPack;
    private Settings _settings;
    private string _defaultSettings = "";
    private string _defaultDir = "";
    private bool _modelActive = false;

    private string _fileName = "No File Open";
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
      _defaultSettings = _defaultDir + "\\AppSmith4Settings.sft";
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
      lbFocusedItem.Text = "Recent:";
      _settings = _settingsPack.Settings;
      SetLocationSettings();
      string smrul = _settings["MRUL"].Value;
      if (!String.IsNullOrEmpty(smrul)) { 
        comboBox1.Items.Clear();
        comboBox1.Items.AddRange(smrul.Parse(Environment.NewLine));
        comboBox1.SelectedIndex = 0;        
      }      
    }
    #endregion
    #region Main Menu opening and handlers
    private void fileToolStripMenuItem_DropDownOpening(object sender, EventArgs e) {
      openStripMenuItem.Enabled = !_modelActive;
      closeStripMenuItem.Enabled = _modelActive;
    }

    private void AddFileToMRUL(string fileName){       
      if (!_settings.Contains("MRUL")) {
        _settings["MRUL"] = new SettingProperty() { Key = "MRUL", Value = fileName };
      } else {
        var mrul = _settings["MRUL"].Value.Parse(Environment.NewLine);
        string newMRUL = (mrul.Length > 0 ? mrul[0] : "") 
          + (mrul.Length > 1 ? Environment.NewLine + mrul[1] : "")
          + (mrul.Length > 2 ? Environment.NewLine + mrul[2] : "");
        StringDict mruld = newMRUL.AsDict(Environment.NewLine);
        mruld.Add(fileName);
        _settings["MRUL"].Value = fileName + Environment.NewLine + mruld.AsString();
      }      
    }
    private void openStripMenuItem_Click(object sender, EventArgs e) {
      if (_fileName == "No File Open") {
        odMain.InitialDirectory = _defaultDir;
      } else {
        string s = _fileName.ParseLast("\\");
        odMain.InitialDirectory = _fileName.Substring(0, _fileName.Length - s.Length);
      }
      DialogResult res = odMain.ShowDialog();
      if (res == DialogResult.OK) {
        try { 
          _settings = _settingsPack.Settings;
          _fileName = odMain.FileName;
          AddFileToMRUL(_fileName);
          _settingsPack.Settings = _settings;
          _settingsPack.Save();
          this.Text = $"AppSmith {_fileName}";          
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
          btnOpenClose.Text = "Close";
          comboBox1.Visible = false;
        } catch(Exception ex) {
          LogMsg(ex.Message);
        }
        SetInProgress(0);
      }
    }
    private void closeStripMenuItem_Click(object sender, EventArgs e) {
      _fileName = "No File Open";
      _modelActive = false;
      tvBuilder.Nodes.Clear();
      tvBuilder.Enabled = false;
      tvBuilder.ContextMenuStrip = null;
      props.Item.Clear();
      props.Enabled = false;
      this.Text = "AppSmith ";
      btnOpenClose.Text = "Open";     
      comboBox1.Visible = true;
      lbFocusedItem.Text = "Recent:";
      edSQL.Text = "";
      edCSharp.Text = "";
      _inEditItem = null;
    }

    private void btnOpenClose_Click(object sender, EventArgs e) {
      if (btnOpenClose.Text != "Open") { 
        closeStripMenuItem_Click(sender, e);
      } else { 
        //openStripMenuItem_Click(sender, e);
        btnMruCombo_Click(sender, e);
      }
    }

    private void btnMruCombo_Click(object sender, EventArgs e) {
      try {
        _settings = _settingsPack.Settings;
        if (comboBox1.SelectedItem != null) { 
          _fileName = comboBox1.SelectedItem.ToString();                
        } else { 
          _fileName = comboBox1.Text;
        }
        this.Text = $"AppSmith {_fileName}";
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
        AddFileToMRUL(_fileName);
        tvBuilder.ContextMenuStrip = msBuilder;
        if (!props.Enabled) props.Enabled = true;
        btnOpenClose.Text = "Close";        
        comboBox1.Visible = false;
      } catch (Exception ex) {
        LogMsg(ex.Message);
      }
      SetInProgress(0);
    }
    #endregion
    #region tree View right click menu and handlers 
    private void msBuilder_Opening(object sender, CancelEventArgs e) {    
    
      if (_inEditItem == null) {      
        
        if (deleteToolStripMenuItem.Enabled) deleteToolStripMenuItem.Enabled = false;
        if (importAPIToolStripMenuItem.Enabled) importAPIToolStripMenuItem.Enabled = false;
        if (moveUpToolStripMenuItem.Enabled) moveUpToolStripMenuItem.Enabled = false;
        if (moveDownToolStripMenuItem.Enabled) moveDownToolStripMenuItem.Enabled = false;

      } else {

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

        if (_inEditItem.TypeId == (int)TnType.Server) { 
          if (_inEditItem.Url != "") {
            if( !importAPIToolStripMenuItem.Enabled) importAPIToolStripMenuItem.Enabled = true;
          } else {
            if(importAPIToolStripMenuItem.Enabled) importAPIToolStripMenuItem.Enabled = false;
          }
        } else {
          if (importAPIToolStripMenuItem.Enabled) importAPIToolStripMenuItem.Enabled = false;
        }

      }      
    }
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
        if (AddMethodParamMenuItem.Enabled) AddMethodParamMenuItem.Enabled = false;
        if (AddClassMenuItem.Enabled) AddClassMenuItem.Enabled = false;

      } else {

        if (!AddServerToolStripMenuItem.Enabled) AddServerToolStripMenuItem.Enabled = true;

        if (_inEditItem.TypeId == (int)TnType.Server) {
          if (!AddDatabaseToolStripMenuItem.Enabled) AddDatabaseToolStripMenuItem.Enabled = true;
          if (!apiToolStripMenuItem.Enabled) apiToolStripMenuItem.Enabled = true;
        } else {
          if (AddDatabaseToolStripMenuItem.Enabled) AddDatabaseToolStripMenuItem.Enabled = false;
          if (apiToolStripMenuItem.Enabled) apiToolStripMenuItem.Enabled = false;
        }

        if (_inEditItem.TypeId == (int)TnType.Api) {
          if (!controllerToolStripMenuItem.Enabled) controllerToolStripMenuItem.Enabled = true;
          if (!AddClassMenuItem.Enabled) AddClassMenuItem.Enabled = true;
        } else {
          if (controllerToolStripMenuItem.Enabled) controllerToolStripMenuItem.Enabled = false;
          if (AddClassMenuItem.Enabled) AddClassMenuItem.Enabled = false;
        }

        if ((_inEditItem.TypeId == (int)TnType.Controller) || (_inEditItem.TypeId == (int)TnType.Class)) {
          if (!meToolStripMenuItem.Enabled) meToolStripMenuItem.Enabled = true;
          if (!AddPropertyMenuItem.Enabled) AddPropertyMenuItem.Enabled = true;
        } else {
          if (meToolStripMenuItem.Enabled) meToolStripMenuItem.Enabled = false;
          if (AddPropertyMenuItem.Enabled) AddPropertyMenuItem.Enabled = false;
        }

        if (_inEditItem.TypeId == (int)TnType.Method) {
          if (!AddMethodParamMenuItem.Enabled) AddMethodParamMenuItem.Enabled = true;
        } else {
          if (AddMethodParamMenuItem.Enabled) AddMethodParamMenuItem.Enabled = false;
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
      _ = _itemCaster.SaveNewChildItemsFromText(null, _types[(int)TnType.Server], "Server");
    }
    private void AddDatabaseToolStripMenuItem_Click(object sender, EventArgs e) {
      var dbItem = _itemCaster.SaveNewChildItemsFromText(_inEditItem, _types[(int)TnType.Database], "Database");
      _itemCaster.SaveNewChildItemsFromText(dbItem, _types[(int)TnType.Tables], "Tables");
      _itemCaster.SaveNewChildItemsFromText(dbItem, _types[(int)TnType.Procedures], "Procedures");
    }

    private void apiToolStripMenuItem_Click(object sender, EventArgs e) {
      var dbItem = _itemCaster.SaveNewChildItemsFromText(_inEditItem, _types[(int)TnType.Api], "Api");
      _itemCaster.SaveNewChildItemsFromText(dbItem, _types[(int)TnType.Controller], "Controller1");
    }


    private void AddTableToolStripMenuItem_Click(object sender, EventArgs e) {
      var tblItem = _itemCaster.SaveNewChildItemsFromText(_inEditItem, _types[(int)TnType.Table], "Table");
      var ColIndx = _itemCaster.SaveNewChildItemsFromText(tblItem, _types[(int)TnType.TableColumn], "Id");
      ColIndx.SQLTypeId = 30;
      ColIndx.Code = "IDENTITY(1,1)";
    }

    private void AddMethodParamMenuItem_Click(object sender, EventArgs e) {
      var mp = _itemCaster.SaveNewChildItemsFromText(_inEditItem, _types[(int)TnType.MethodParam], "Param");
      mp.SQLTypeId = 30;
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
      _inReorder = true;
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
        _inReorder = false;
      }
    }

    private void controllerToolStripMenuItem_Click(object sender, EventArgs e) {
      var cont = _itemCaster.SaveNewChildItemsFromText(_inEditItem, _types[(int)TnType.Controller], "Controller");
      cont.Version = "1.0"; // version default 
    }
    private void AddClassMenuItem_Click(object sender, EventArgs e) {
      var cont = _itemCaster.SaveNewChildItemsFromText(_inEditItem, _types[(int)TnType.Class], "Class");      
    }
    private void meToolStripMenuItem_Click(object sender, EventArgs e) {
      var meth = _itemCaster.SaveNewChildItemsFromText(_inEditItem, _types[(int)TnType.Method], "Method");
      meth.MethodTypeId = 66;
    }
    

    private void AddPropertyMenuItem_Click(object sender, EventArgs e) {
      var cont = _itemCaster.SaveNewChildItemsFromText(_inEditItem, _types[(int)TnType.Property], "Prop");
    }

    private int GetMethodTypeFor(string httpMethodType) {
      int ret = 66;
      var al = _types.GetChildrenItems(65).ToArray<ItemType>();
      var match = al.First<ItemType>(x => x.Name == httpMethodType);
      if (match != null) { 
        ret = match.TypeId;
      }
      return ret;
    }

    private int GetParamTypefor(string interfaceType) { 
      int ret = 85;
      if (interfaceType != "integer") {      
        var al = _types.GetChildrenItems(79).ToArray<ItemType>();
        var tm = al.FirstOrDefault<ItemType>(x => x.Name == interfaceType);
        if (tm == null) {
          ret = 80;
        } else {
          ret = tm.TypeId;
        }
      }
      return ret;
    }
    private string ConvertToCSharpType(string text) {       
      if (text == null) return "Object";
      if (text == "object") return "Object";      
      if (text == "integer") return "int";
      if (text == "int32") return "int";
      return text;
    }
    private async void importAPIToolStripMenuItem_ClickAsync(object sender, EventArgs e) {
      _inReorder = true;
      try {
        if (_inEditItem.TypeId == (int)TnType.Server && !string.IsNullOrEmpty(_inEditItem.Url)) {
          var OAPIr = await ApiExt.GetOpenApiDocFromSite(_inEditItem.Url);
          if (OAPIr.Status != OpenApiDocStatus.Error) { 
            foreach(var warning in OAPIr.Diagnostic.Warnings) { 
              LogMsg(warning.Message);
            }
            var OAPI = OAPIr.Document; 
            string lastTagName = "";
            Item cont = _inEditItem;
            Item iapi = _inEditItem;          

            iapi = _itemCaster.SaveNewChildItemsFromText(_inEditItem, _types[(int)TnType.Api], $"{OAPI.Info.Title}.API"); 
            iapi.Code = OAPIr.RawJson;

            foreach(var compName in OAPI.Components.Schemas.Keys) { 
              try {
                var className = compName;                
                if ((compName.Length > 2) && (compName[0] == 'I') && (compName.Substring(1, 1).ToLower() != compName.Substring(1, 1))){ 
                  className = compName.Substring(1);                  
                }
                var ClassItem = _itemCaster.SaveNewChildItemsFromText(iapi, _types[(int)TnType.Class], className);
                ClassItem.IsArrayTypes = "false,false,false,false,false";
                ClassItem.BaseClass = "I" + className.AsUpperCaseFirstLetter(); 
                ClassItem.AccessTypeId = 74;
            
                var CompVal = OAPI.Components.Schemas[compName];
                if (CompVal != null) { 
                  foreach(string propName in CompVal.Properties.Keys) {                    
                    var propNameItem = _itemCaster.SaveNewChildItemsFromText(ClassItem, _types[(int)TnType.Property], propName);                                      
                    propNameItem.BaseClass = ConvertToCSharpType(CompVal.Properties[propName].Type);
                    propNameItem.AccessTypeId = 74;
                    propNameItem.IsArrayTypes = "false,false,false,false,false";                    
                  }
                }
              } catch (Exception ee) { 
                LogMsg(ee.Message);
              }
            } // end for each component class

            foreach (string pathKey in OAPI.Paths.Keys) {            //  for each path
              try {                
                var ops = OAPI.Paths[pathKey].Operations;
                foreach (var opKey in ops.Keys) {

                  try {
                    Item meth = _inEditItem;
                    var tag = ops[opKey].Tags[0];                
                    
                    if ((tag != null && !string.IsNullOrEmpty(tag.Name)) && lastTagName != tag.Name.AsUpperCaseFirstLetter() + "Controller") {
                      lastTagName = tag.Name.AsUpperCaseFirstLetter() + "Controller";
                      var sr = iapi.Nodes.Find(lastTagName, false);
                      if (sr.Length > 0) {
                        cont = sr[0] as Item;
                      } else {
                        cont = _itemCaster.SaveNewChildItemsFromText(iapi, _types[(int)TnType.Controller], lastTagName);                        
                        cont.AccessTypeId = 74;
                        cont.IsArrayTypes = "false,false,false,false,false";
                        cont.BaseClass = $"I{lastTagName}";
                        cont.Route = pathKey;
                        cont.Version = OAPI.Info?.Version ?? "1.0";
                      }                               
                    }

                    
                    try {
                      string[] methParts = ops[opKey].ParseOpenApiOpForMethod(opKey,_types).Parse(",");
                      string MethReturnType = methParts[0];
                      string MethodName = methParts[1];                      
                      meth = _itemCaster.SaveNewChildItemsFromText(cont, _types[(int)TnType.Method], MethodName);
                      meth.MethodTypeId = methParts[2].AsInt();
                      meth.ReturnType = MethReturnType;
                      meth.Route = pathKey;
                      meth.IsArrayTypes = "false,false,false,false,false";
                      meth.AccessTypeId = 74;                        
                      
                      var pms = ops[opKey].Parameters;
                      foreach (var p in pms) {  // foreach Parameter in Parameters
                        var pr = p.ParseParameter(_types);
                        if ((pr != null) && (pr.MethodParams.Count>0)){ 
                          foreach(var p2 in pr.MethodParams) {
                            var mpm = p2.Parse(",");
                            var mpr = _itemCaster.SaveNewChildItemsFromText(meth, _types[(int)TnType.MethodParam], $"{mpm[0]}");                            
                            mpr.CSharpTypeId = mpm[1].AsInt();                            
                            mpr.BaseClass = mpm[2];                            
                          }
                        }                        
                      }  // for each op param
                    } catch(Exception ew) { 
                      LogMsg(ew.Message);
                    }
                
                    var aRB = ops[opKey].RequestBody;   // process body specifics.
                    if (aRB != null && (meth != null)) { 
                      var aRBr = aRB.ParseRequestBody(_types);
                      foreach(var p in aRBr.MethodParams) {
                        var mpm = p.Parse(",");
                        var mp = _itemCaster.SaveNewChildItemsFromText(meth, _types[(int)TnType.MethodParam], $"[FromBody] {mpm[0]}");                        
                        mp.CSharpTypeId = mpm[1].AsInt();
                        mp.BaseClass = mpm[2];                        
                      }
                    }                

                  } catch (Exception ex) { 
                    LogMsg(ex.Message);
                  }
                } // for each op
              } catch (Exception ex1) {
                LogMsg(ex1.Message);
              }
            }  // for each path
          } else { // was not a success
            LogMsg(OAPIr.ErrorMessage);
          }
        }
      } catch (Exception ex1) { 
        LogMsg(ex1.Message);
      }
      _inReorder = false;
    }

    #endregion

    private void tvBuilder_BeforeExpand(object sender, TreeViewCancelEventArgs e) {
      Item tn = (Item)e.Node;
      if (tn != null) {
        var items = _itemCaster.GetOwnersItemsAsync(tn);
        e.Cancel = false;
      }
    }

    private void tvBuilder_AfterLabelEdit(object sender, NodeLabelEditEventArgs e) {
      try {
        if (_inEditItem != (Item)e.Node) {
          _inEditItem = (Item)e.Node;
        }
        if (_inEditItem == null || e.Label == null) {
          e.CancelEdit = true;
          return;
        }
        if (_inEditItem.Name != e.Label) { _inEditItem.Name = e.Label; }
        if (_inEditItem.Dirty) _itemCaster.SaveItem(_inEditItem);
        SetInProgress(9500);
        ResetPropEditors(_inEditItem);
        PopulateEditors(_inEditItem);
      } finally {
        SetInProgress(0);
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

        ItemType it = _types[item.TypeId];
        ItemType itCat = _types[it.CatagoryTypeId];

        var cp = new PropertyGridEx.CustomProperty("Name", item.Name, it.Readonly, itCat.Text, it.Desc, it.Visible);
        props.Item.Add(cp);

        if (item.TypeId == (int)TnType.Server) {
          var cp1 = new PropertyGridEx.CustomProperty("OpenAPIJsonURL", item.Url, false, itCat.Text, "", it.Visible);
          props.Item.Add(cp1);
        }

        if (item.TypeId == (int)TnType.Controller ||
            item.TypeId == (int)TnType.Property || 
            item.TypeId == (int)TnType.Class || 
            item.TypeId == (int)TnType.Method
          ) {
          var sa = item.IsArrayTypes.Parse(",");
          if ((sa == null)||(sa.Length!=5)) { 
            sa = "false,false,false,false,false".Parse(",");
          }
          bool isAsync = bool.Parse(sa[0]);
          bool isVirtual = bool.Parse(sa[1]);
          bool isStatic = bool.Parse(sa[2]);
          bool isAbstract = bool.Parse(sa[3]);
          bool isSealed = bool.Parse(sa[4]);          
          string returnType = item.ReturnType == "" ? "void " : item.ReturnType;

          int access = item.AccessTypeId;
          var al = _types.GetChildrenItems(73).ToArray<ItemType>();          
          cp = new PropertyGridEx.CustomProperty() {
            Name = "Access", Category = "Accessibility", Description = it.Desc, Value = _types[access].Name,
            DisplayMember = "Name", ValueMember = "TypeId", Datasource = al, Visible = true, IsReadOnly = false, IsDropdownResizable = true
          };
          props.Item.Add(cp);

          var cp0 = new PropertyGridEx.CustomProperty("Async", isAsync, false, "Async", "Include async", true);
          props.Item.Add(cp0);
          var cp1 = new PropertyGridEx.CustomProperty("Virtual", isVirtual, false, "Virtual", "Include virtual", true);
          props.Item.Add(cp1);
          var cp2 = new PropertyGridEx.CustomProperty("Static", isStatic, false, "Static", "Include static", true);
          props.Item.Add(cp2);
          var cp3 = new PropertyGridEx.CustomProperty("Abstract", isAbstract, false, "Abstract", "Include abstract", true);
          props.Item.Add(cp3);
          var cp4 = new PropertyGridEx.CustomProperty("Sealed", isSealed, false, "Sealed", "Include sealed", true);
          props.Item.Add(cp4);

          if (item.TypeId == (int)TnType.Method) {
            var cp5 = new PropertyGridEx.CustomProperty("ReturnType", returnType, false, "Return Type", "Returns", true);
            props.Item.Add(cp5);
          } else {
            var cp5 = new PropertyGridEx.CustomProperty("Base Class", item.BaseClass, false, "Base Class", "Type class is derived", true);            
            props.Item.Add(cp5);
          }         
        }

        if (item.TypeId == (int)TnType.MethodParam) {

          var al = _types.GetChildrenItems(79).ToArray<ItemType>();          
          var customProperty = new PropertyGridEx.CustomProperty() {
            Name = "Type", Category = itCat.Text, Description = it.Desc, Value = _types[item.CSharpTypeId].Name,
            DisplayMember = "Name", ValueMember = "TypeId", Datasource = al, Visible = true, IsReadOnly = false, IsDropdownResizable = true
          };
          props.Item.Add(customProperty);

          int[] ClassStructTypes = { 80, 81 };
          if( Array.IndexOf(ClassStructTypes, item.CSharpTypeId) >= 0) {
            var cp1 = new PropertyGridEx.CustomProperty("Base Class", item.BaseClass, false, itCat.Text, "", it.Visible);
            props.Item.Add(cp1);
          }

        }

        if (
            item.TypeId == (int)TnType.TableColumn ||
            item.TypeId == (int)TnType.ViewColumn ||
            item.TypeId == (int)TnType.ProcedureParam ||
            item.TypeId == (int)TnType.FunctionParam 
            
        ) {
          var al = _types.GetChildrenItems(29).ToArray<ItemType>();          
          var customProperty = new PropertyGridEx.CustomProperty() {
            Name = "SQLType", Category = itCat.Text, Description = it.Desc, Value = _types[item.SQLTypeId].Name,
            DisplayMember = "Name", ValueMember = "TypeId", Datasource = al, Visible = true, IsReadOnly = false, IsDropdownResizable = true
          };
          props.Item.Add(customProperty);
          
          cp = new PropertyGridEx.CustomProperty("Size", item.SQLTypeSize, false, itCat.Text, "column size example (max)", it.Visible);
          props.Item.Add(cp);
          
          var cp1 = new PropertyGridEx.CustomProperty("Code", item.Code, false, itCat.Text, "", it.Visible);
          props.Item.Add(cp1);
          
        }

        if (item.TypeId == (int)TnType.Controller) {
          cp = new PropertyGridEx.CustomProperty("Version", item.Version, false, "API Version", "Controller Version", true);
          props.Item.Add(cp);
        }
        Item parent = (Item)item.Parent;
        if (item.TypeId == (int)TnType.Method && (parent.TypeId == (int)TnType.Controller)) {
          var al = _types.GetChildrenItems(65).ToArray<ItemType>();
          cp = new PropertyGridEx.CustomProperty() {
            Name = "Method Type", Category = itCat.Text, Description = it.Desc, Value = _types[item.MethodTypeId].Name,
            DisplayMember = "Name", ValueMember = "TypeId", Datasource = al, Visible = true, IsReadOnly = false, IsDropdownResizable = true
          };
          props.Item.Add(cp);
        }


      }

      props.Refresh();
      props.MoveSplitterTo(Convert.ToInt32(props.Width * 0.3333));

    }

    private void props_PropertyValueChanged(object s, PropertyValueChangedEventArgs e) {
      if (_inEditItem != null) {

        var sa = "false,false,false,false,false".Parse(",");
        if (_inEditItem.TypeId == (int)TnType.Class ||
            _inEditItem.TypeId == (int)TnType.Controller ||
            _inEditItem.TypeId == (int)TnType.Property || 
            _inEditItem.TypeId == (int)TnType.Method) { 
          sa = _inEditItem.IsArrayTypes.Parse(",");
          if ((sa == null) || (sa.Length != 5)) {
            sa = "false,false,false,false,false".Parse(",");
          }
        }        
        bool isAsync = bool.Parse(sa[0]);
        bool isVirtual = bool.Parse(sa[1]);
        bool isStatic = bool.Parse(sa[2]);
        bool isAbstract = bool.Parse(sa[3]);
        bool isSealed = bool.Parse(sa[4]);
        string returnType = _inEditItem.ReturnType == "" ? "void " : _inEditItem.ReturnType;        

        foreach (CustomProperty y in props.Item) {
          if ((y.Name == "Name")&&(!String.IsNullOrEmpty( y.Name)) ){
            if (Convert.ToString(y.Value) != _inEditItem.Name) {
              _inEditItem.Name = Convert.ToString(y.Value);
            }          
          } else if (y.Name == "Access") {
            if ((y.SelectedValue != null) && (Convert.ToInt32(y.SelectedValue) != _inEditItem.AccessTypeId)) {
              _inEditItem.AccessTypeId = Convert.ToInt32(y.SelectedValue);
            }
          } else if (y.Name == "SQLType") {
            var selVal = Convert.ToInt32(y.SelectedValue);
            if ((selVal != _inEditItem.SQLTypeId) && (y.SelectedValue != null)) {
              _inEditItem.SQLTypeId = selVal;
            }
          } else if (y.Name == "Type") {
            var selVal = Convert.ToInt32(y.SelectedValue);
            if ((selVal != _inEditItem.CSharpTypeId) && (y.SelectedValue != null)) {
              _inEditItem.CSharpTypeId = selVal;
            }
          } else if (y.Name == "Method Type") {
            var selVal = Convert.ToInt32(y.SelectedValue);
            if ((selVal != _inEditItem.MethodTypeId) && (y.SelectedValue != null)){
              _inEditItem.MethodTypeId = selVal;              
            }                    
          } else if (y.Name == "Async") {
            if ((y.Value != null) && (Convert.ToBoolean(y.Value) != isAsync)) {
              isAsync = Convert.ToBoolean(y.Value);
            }
          } else if (y.Name == "Virtual") {
            if ((y.Value != null) && (Convert.ToBoolean(y.Value) != isVirtual)) {
              isVirtual = Convert.ToBoolean(y.Value);              
            }
          } else if (y.Name == "Static") {
            if ((y.Value != null) && (Convert.ToBoolean(y.Value) != isStatic)) {
              isStatic = Convert.ToBoolean(y.Value);
            }
          } else if (y.Name == "Abstract") {
            if ((y.Value != null) && (Convert.ToBoolean(y.Value) != isAbstract)) {
              isAbstract = Convert.ToBoolean(y.Value);
            }
          } else if (y.Name == "Sealed") {
            if ((y.Value != null) && (Convert.ToBoolean(y.Value) != isSealed)) {
              isSealed = Convert.ToBoolean(y.Value);
            }          
          } else if (y.Name == "Base Class") {
            if ((y.Value != null) && (y.Value.AsString() != _inEditItem.BaseClass)) {
              _inEditItem.BaseClass = y.Value.AsString();
            }
          } else if (y.Name == "Code") {
            if ((y.Value != null) && (y.Value.AsString() != _inEditItem.Code)) {
              _inEditItem.Code = y.Value.AsString();
            }
          } else if (y.Name == "ReturnType") {
            if ((y.Value != null) && (y.Value.AsString() != _inEditItem.ReturnType)) {
              _inEditItem.ReturnType = y.Value.AsString();
            }
          } else if (y.Name == "Route") {
            if ((y.Value != null) && (y.Value.AsString() != _inEditItem.Route)) {
              _inEditItem.Route = y.Value.AsString();
            }
          } else if (y.Name == "Size") {
            if ((y.Value != null) && (y.Value.AsString() != _inEditItem.SQLTypeSize)) {
              _inEditItem.SQLTypeSize = y.Value.AsString();
            }
          } else if (y.Name == "OpenAPIJsonURL") {
            if ((y.Value != null) && (y.Value.AsString() != _inEditItem.Url)) {
              _inEditItem.Url = y.Value.AsString();
            }
          } else if (y.Name == "Version") {
            if ((y.Value != null) && (y.Value.AsString() != _inEditItem.Version)) {
              _inEditItem.Version = y.Value.AsString();
            }
          }
        }
        if (_inEditItem.TypeId == (int)TnType.Class ||
            _inEditItem.TypeId == (int)TnType.Controller ||
            _inEditItem.TypeId == (int)TnType.Property || 
            _inEditItem.TypeId == (int)TnType.Method) {
          string newcode = $"{isAsync},{isVirtual},{isStatic},{isAbstract},{isSealed}";
          if (_inEditItem.IsArrayTypes != newcode) { 
            _inEditItem.IsArrayTypes = newcode;
          }
        }
        if (_inEditItem.Dirty) {
          try {
            SetInProgress(9500);
            _itemCaster.SaveItem(_inEditItem);
            ResetPropEditors(_inEditItem);
            PopulateEditors(_inEditItem);
          } finally {
            SetInProgress(0);
          }
        }
      }
    }

    public void PopulateEditors(Item it) {
      var lit = it;
      if (lit != null) {
        if (lit.TypeId >= (int)TnType.TableColumn && lit.TypeId <= (int)TnType.FunctionParam) {
          lit = (Item)_inEditItem.Parent;        
        }
      }
      Item parent = (Item)it?.Parent ?? null;
      Item gp = (Item)parent?.Parent ?? parent;

      if (it == null) {
        lbFocusedItem.Text = string.Empty;
         return;
      }
      lbFocusedItem.Text = it.Name;
      switch (lit.TypeId) {
        case (int)TnType.Tables: PrepareListTables(lit); break;
        case (int)TnType.Table: PrepareTableType(lit); break;
        case (int)TnType.Procedure: PrepareProcedureType(lit); break;
        case (int)TnType.Api: PrepareApi(lit) ; break;
        case (int)TnType.Controller: PrepareController(lit); break;
        case (int)TnType.Method: 
          if(parent.TypeId == (int)TnType.Controller) {
            PrepareController(parent);
          } else {
            PrepareClass(parent);
          } break;
        case (int)TnType.MethodParam:
          if (gp.TypeId == (int)TnType.Controller) {
            PrepareController(gp);
          } else {
            PrepareClass(gp);
          }
          break;
        case (int)TnType.Class: PrepareClass(lit); break;
        case (int)TnType.Property:
          if (parent.TypeId == (int)TnType.Controller) {
            PrepareController(parent);
          } else {
            PrepareClass(parent);
          } break;
        //  case (int)TnType.Function: PrepareFunctionType(it); break;
        default: PrepareNullType(lit); break;
      }
    }
    public void PrepareApi(Item it) { 
      if (it == null) return;
      edSQL.Text = $"-- {it.Name} Sql not implemented yet.";
      edCSharp.Text = it.GenerateApi(_types);
      edJSONOut.Text = it.Code;
    }
    public void PrepareController(Item it) {
      if (it == null) return;
      Item parentItem = (it.Parent as Item);
      edSQL.Text = $"-- {it.Name} Sql not implemented yet.";
      edCSharp.Text = it.GenerateController(_types, true);
      if ((parentItem != null) && (parentItem.TypeId == (int)TnType.Api) && ( !String.IsNullOrEmpty(parentItem.Code) )) {
        edJSONOut.Text = parentItem.Code;
      } else { 
        edJSONOut.Text = $"{it.Name} JSON not implemented yet.";
      }
    }
    public void PrepareClass(Item it) {
      if (it == null) return;
      Item parentItem = (it.Parent as Item);
      edSQL.Text = $"-- {it.Name} Sql not implemented yet.";
      edCSharp.Text = it.GenerateClass(_types, true);
      if ((parentItem != null) &&(parentItem.TypeId==(int)TnType.Api) &&(!String.IsNullOrEmpty(parentItem.Code))) {
        edJSONOut.Text = parentItem.Code;
      } else {
        edJSONOut.Text = $"{it.Name} JSON not implemented yet.";
      }
    }

    public void PrepareNullType(Item it) {
      if (it == null) return;
      edSQL.Text = $"-- {it.Name} Sql not implemented yet.";
      edCSharp.Text = $"// {it.Name} C not implemented ";
      edJSONOut.Text = $"{it.Name} JSON not implemented yet.";
    }

    public void PrepareListTables(Item it) { 
      StringBuilder res = new StringBuilder();
      foreach(var childTable in it.Nodes) { 
        Item thisItem = (Item)childTable;
        res.AppendLine(thisItem.GenerateSqlCreateTable(_types)+Cs.nl);
      }
      edSQL.Text = res.ToString();
      edJSONOut.Text = $"{it.Name} JSON not implemented yet.";
    }
    public void PrepareTableType(Item it) {
      if (it == null) return;
      edSQL.Text = " " + Cs.nl + it.GenerateSqlCreateTable(_types) + Cs.nl + Cs.nl + it.GenerateSQLAddUpdateStoredProc(_types) + it.GetSQLCursor(_types);
      edCSharp.Text = Cs.nl + it.GenerateCSharpRepoLikeClassFromTable(_types, true);
      edJSONOut.Text = $"{it.Name} JSON not implemented yet.";
    }

    public void PrepareProcedureType(Item tnProcedure) {
      edSQL.Text = tnProcedure.GenerateSQLStoredProc(_types);
      edCSharp.Text = tnProcedure.GenerateCSharpExecStoredProc(_types);
      edJSONOut.Text = $"{tnProcedure.Name} JSON not implemented yet.";
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
      _itemCaster.ParseSqlContentIntoItems(_inEditItem, sInput);     
    }

    private void CopyOutputMenuItem_Click(object sender, EventArgs e) {
      if (tabControl1.SelectedTab == tpSqlOut) {
        if (edSQL.SelectedText.Length == 0) edSQL.SelectAll();
        Clipboard.SetText(edSQL.SelectedText);
      } else if (tabControl1.SelectedTab == tpCOut) {
        if (edCSharp.SelectedText.Length == 0) edCSharp.SelectAll();
        Clipboard.SetText(edCSharp.SelectedText);
      }
    }


  }
}
