using Newtonsoft.Json;
using static BonelabDevMode.Barcodes;
using static BonelabDevMode.Main;

namespace BonelabDevMode.JSON
{
    public class Pallet
    {
        [JsonProperty("version")]
        public int Version;

        [JsonIgnore]
        public required string DirectoryPath;

        [JsonIgnore]
        public required string FilePath;

        [JsonProperty("objects")]
        public required Dictionary<string, PalletObject> Objects;

        [JsonProperty("types", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, P3Type>? Types;
    }

    public class PalletObject
    {
        [JsonIgnore]
        public string Key;

        [JsonProperty("barcode")]
        public required string Barcode;

        [JsonProperty("author", NullValueHandling = NullValueHandling.Ignore)]
        public string? Author;

        [JsonIgnore]
        public Pallet? Pallet;

        [JsonIgnore]
        public PalletObject? Origin;

        [JsonIgnore]
        public BarcodeType Type;

        [JsonIgnore]
        public string? DisplayName;

        [JsonProperty("crates", NullValueHandling = NullValueHandling.Ignore)]
        public List<Crate>? Crates;

        [JsonProperty("dataCards", NullValueHandling = NullValueHandling.Ignore)]
        public List<Crate>? DataCards;

        [JsonProperty("unlockable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Unlockable;

        [JsonProperty("redacted", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Redacted;

        [JsonProperty("changelogs", NullValueHandling = NullValueHandling.Ignore)]
        public List<ChangeLog>? ChangeLogs;

        [JsonProperty("tags", NullValueHandling = NullValueHandling.Ignore)]
        public List<string>? Tags;

        [JsonIgnore]
        public required bool isBONELABContent = true;

        [JsonProperty("title")]
        public required string Title;

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string? Description;

        [JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
        public string? Version;

        [JsonProperty("sdkVersion", NullValueHandling = NullValueHandling.Ignore)]
        public string? SDKVersion;

        [JsonProperty("isa")]
        public ISA? ISA;

        [JsonProperty("packedAssets", NullValueHandling = NullValueHandling.Ignore)]
        public List<PacketAsset>? PacketAssets;

        [JsonProperty("mainAsset", NullValueHandling = NullValueHandling.Ignore)]
        public string? MainAsset;

        [JsonProperty("dataCardAsset", NullValueHandling = NullValueHandling.Ignore)]
        public string? DataCardAsset;

        [JsonProperty("multiscene", NullValueHandling = NullValueHandling.Ignore)]
        public bool? MultiScene;

        [JsonProperty("internal", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Internal;

        [JsonProperty("colliderBounds", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, ColliderBounds>? ColliderBounds;

        public PalletObject()
        {
        }

        public PalletObject(string barcode, BarcodeType type, string title, bool isBONELABContent = true, string? description = null, string? displayName = null, string? author = null, string? version = null, string? sDKVersion = null, ISA? ISA = null, Pallet? pallet = null, PalletObject? origin = null, List<Crate> crates = null, List<ChangeLog> changeLogs = null, List<string> tags = null)
        {
            Barcode = barcode;
            Pallet = pallet;
            Origin = origin;
            Type = type;
            DisplayName = displayName;
            Title = title;
            Description = description;
            Version = version;
            SDKVersion = sDKVersion;
            this.ISA = ISA;
            this.isBONELABContent = isBONELABContent;
            Author = author;
            Crates = crates;
            ChangeLogs = changeLogs;
            Tags = tags;

            if (string.IsNullOrWhiteSpace(displayName))
            {
                DisplayName = AC_HTMLRemove().Replace($"{barcode} (?_{title}_?)", string.Empty);
            }
        }
    }

    public class ISA
    {
        [JsonProperty("type")]
        public required string Type;
    }

    public class P3Type
    {
        [JsonProperty("type")]
        public required string Type;

        [JsonProperty("fullname")]
        public required string FullName;
    }

    public class Crate
    {
        [JsonProperty("ref")]
        public string Ref;

        [JsonProperty("type")]
        public string Type;
    }

    public class ChangeLog
    {
        [JsonProperty("version")]
        public required string Version;

        [JsonProperty("title")]
        public required string Title;

        [JsonProperty("text")]
        public required string Text;
    }

    public class PacketAsset
    {
        [JsonProperty("title")]
        public required string Title;

        [JsonProperty("subAssets", NullValueHandling = NullValueHandling.Ignore)]
        public List<string>? SubAssets;

        [JsonProperty("guid")]
        public string? GUID;
    }

    public class ColliderBounds
    {
        [JsonProperty("x")]
        public required float X;

        [JsonProperty("y")]
        public required float Y;

        [JsonProperty("z")]
        public required float Z;
    }
}