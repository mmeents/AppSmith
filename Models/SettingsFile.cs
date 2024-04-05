using MessagePack;
using System;
using System.IO;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSmith.Models {

  [MessagePackObject]
  public class SettingProperty {
    [IgnoreMember]
    public string Key { get; set; }
    [IgnoreMember]
    public string Value { get; set; }

    [Key(0)]
    public byte[] AsBytes {
      get {
        return $"{Key.AsBase64Encoded()} {Value.AsBase64Encoded()}".AsBytes();
      }
      set {
        string bytesAsString = value.AsString();
        Key = bytesAsString.ParseFirst(" ").AsBase64Decoded();
        Value = bytesAsString.ParseLast(" ").AsBase64Decoded();
      }
    }
  }

  [MessagePackObject]
  public class SettingsPackage {
    [Key(0)]
    public string Name { get; set; }
    [Key(1)]
    public ICollection<SettingProperty> SettingsList { get; set; } = new List<SettingProperty>();    
  }

  public class Settings : ConcurrentDictionary<string, SettingProperty> {
    public Settings() : base() { }

    public Settings(ICollection<SettingProperty> asList) : base() {
      AsList = asList;
    }
    public virtual Boolean Contains(String key) {
      try {
        return (!(base[key] is null));
      } catch {
        return false;
      }
    }

    public virtual new SettingProperty this[string key] {
      get {
        if (!Contains(key)) base[key] = new SettingProperty() { Key = key, Value = "" };
        return base[key];
      }
      set { if (value != null) { base[key] = value; } else { Remove(key); } }
    }
    public virtual void Remove(string key) {
      if (Contains(key)) { _ = base.TryRemove(key, out _); }
    }

    public ICollection<SettingProperty> AsList {
      get { return base.Values; }
      set {
        base.Clear();
        foreach (var x in value) {
          this[x.Key] = x;
        }
      }
    }

    public Settings Clone() {
      var clone = new Settings {
        AsList = AsList
      };
      return clone;
    }
  }


  public class SettingsFile {
    private readonly Form1 _form1;

    public SettingsFile(string fileName, Form1 form1) {
      _form1 = form1;
      FileName = fileName;

      Package = new SettingsPackage {
        Name = FileName
      };
      if (File.Exists(FileName)) {
        Load();
      }
    }

    public string FileName { get; set; }
    private bool _FileLoaded = false;
    public bool FileLoaded { get { return _FileLoaded; } }
    public SettingsPackage Package { get; set; }

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

    public Settings Settings { get { return GetSettings(); } set { SetSettings(value); } }

    public void Load() {
      Task.Run(async () => await this.LoadAsync().ConfigureAwait(false)).GetAwaiter().GetResult();
    }
    public async Task LoadAsync() {
      if (File.Exists(FileName)) {
        var mark = DateTime.Now;
        var encoded = await FileName.ReadAllTextAsync();
        var decoded = Convert.FromBase64String(encoded.Replace('?', '='));
        this.Package = MessagePackSerializer.Deserialize<SettingsPackage>(decoded);
        _FileLoaded = true;
        var finish = DateTime.Now;
        var diff = (finish - mark).TotalMilliseconds;
        _form1.LogMsg($"{DateTime.Now} {diff}ms loaded: {FileName}");
      } else {
        _form1.LogMsg($"{DateTime.Now} ms skipped(no file): {FileName}");
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
      _form1.LogMsg($"{DateTime.Now} {diff}ms Saved: {FileName}");
    }


  }
}
