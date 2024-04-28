using MessagePack;
using System;
using System.IO;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AppSmith.Models {

  

  [MessagePackObject]
  public class WirePackage {
    [Key(0)]
    public string Name { get; set; }
    [Key(1)]
    public ICollection<SettingProperty> SettingsList { get; set; } = new List<SettingProperty>();

    [Key(2)]
    public ICollection<string> ItemChunks { get; set; } = new List<string>();

  }



  public class FilePackage {
    private readonly Form1 _owner;
    public FilePackage(string fileName, Form1 form1) {
      _owner = form1;
      FileName = fileName;
      Package = new WirePackage {
        Name = fileName
      };
      if (File.Exists(FileName)) {
        Load();
      }
    }

    public string FileName { get; set; }
    private bool _FileLoaded = false;
    public bool FileLoaded { get { return _FileLoaded; } }
    public WirePackage Package { get; set; }

    private Settings GetSettings() {
      Settings n = new Settings();
      foreach (var item in Package.SettingsList) {
        n[item.Key] = item;
      }
      return n;
    }
    private void SetSettings(Settings value) {
      Package.SettingsList = value.AsList;
    }

    private Items GetItems() {
      Items n = new Items();
      foreach (string chunk in Package.ItemChunks) {
        try { 
          var item = new Item().FromChunk(chunk);
          n[item.Id] = item;
        } catch { 

        }
      }
      return n;
    }

    private void SetItems(Items value) {
      List<string> itemChunks = new List<string>();
      foreach (Item item in value.Values) {
        itemChunks.Add(item.AsChunk());
      }
      Package.ItemChunks = itemChunks;
    }

    public Settings Settings { get { return GetSettings(); } set { SetSettings(value); } }
    public Items PackageItems { get { return GetItems(); } set { SetItems(value); } }

    public void Load() {
      Task.Run(async () => await this.LoadAsync().ConfigureAwait(false)).GetAwaiter().GetResult();
    }
    public async Task LoadAsync() {
      if (File.Exists(FileName)) {
        var mark = DateTime.Now;
        var encoded = await FileName.ReadAllTextAsync();
        var decoded = Convert.FromBase64String(encoded.Replace('?', '='));
        this.Package = MessagePackSerializer.Deserialize<WirePackage>(decoded);
        _FileLoaded = true;
        var finish = DateTime.Now;
        var diff = (finish - mark).TotalMilliseconds;
        _owner.LogMsg($"{DateTime.Now} {diff}ms loaded: {FileName}");
      }
    }

    public void Save() {
      Task.Run(async () => await this.SaveAsync().ConfigureAwait(false)).GetAwaiter().GetResult();
    }
    public async Task SaveAsync() {
      var mark = DateTime.Now;
      byte[] WirePacked = MessagePackSerializer.Serialize(this.Package);
      string encoded = Convert.ToBase64String(WirePacked);
      await encoded.WriteAllTextAsync(FileName);
      var finish = DateTime.Now;
      var diff = (finish - mark).TotalMilliseconds;
      _owner.LogMsg($"{DateTime.Now} {diff}ms Saved: {FileName}");
    }

  }

}
