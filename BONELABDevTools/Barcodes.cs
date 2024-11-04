using BonelabDevMode.JSON;

namespace BonelabDevMode
{
    public static class Barcodes
    {
#pragma warning disable IDE1006 // Naming Styles
        public static List<PalletObject> barcodes { get; internal set; } = [];
#pragma warning restore IDE1006 // Naming Styles

        public static void AddDefaults()
        {
            lock (barcodes)
                barcodes.AddRange(BONELABContentBarcodes.GetObjects());
        }

        public static void AddObject(PalletObject obj)
        {
            lock (barcodes)
                barcodes.Add(obj);
        }

        public static void AddObject(PalletObject obj, PalletObject origin)
        {
            obj.Origin = origin;
            obj.isBONELABContent = false;
            lock (barcodes)
                barcodes.Add(obj);
        }

        public static void AddObject(PalletObject obj, Pallet pallet, PalletObject origin)
        {
            obj.Origin = origin;
            obj.isBONELABContent = false;
            obj.Pallet = pallet;
            lock (barcodes)
                barcodes.Add(obj);
        }

        public static void AddObject(PalletObject obj, Pallet pallet, PalletObject origin, BarcodeType type)
        {
            obj.Origin = origin;
            obj.Type = type;
            obj.Pallet = pallet;
            obj.isBONELABContent = false;
            lock (barcodes)
                barcodes.Add(obj);
        }

        public static void AddObject(PalletObject obj, Pallet pallet, BarcodeType type)
        {
            obj.Pallet = pallet;
            obj.Type = type;
            obj.isBONELABContent = false;
            lock (barcodes)
                barcodes.Add(obj);
        }

        public static void AddObject(string barcode, string title)
        {
            lock (barcodes)
                barcodes.Add(new PalletObject() { Barcode = barcode, Title = title, isBONELABContent = false });
        }

        public static void AddObject(string barcode, string title, string displayName)
        {
            lock (barcodes)
                barcodes.Add(new PalletObject() { Barcode = barcode, Title = title, DisplayName = displayName, isBONELABContent = false });
        }

        public static PalletObject[] GetPOByType(BarcodeType type)
        {
            var _barcodes = barcodes.Where(x => x.Type == type);
            return _barcodes.ToArray();
        }

        public static PalletObject[] GetPOByBarcode(string barcode)
        {
            var _barcodes = barcodes.Where(x => x.Barcode == barcode);
            return _barcodes.ToArray();
        }

        public static PalletObject[] GetPOBySDKVersion(string sdkVersion)
        {
            var _barcodes = barcodes.Where(x => x.SDKVersion == sdkVersion);
            return _barcodes.ToArray();
        }

        public enum BarcodeType
        {
            LEVEL,
            SPAWNABLE,
            AVATAR,
            PALLET,
            MONODISC,
            VFX,
            BONETAG,
            SURFACEDATACARD
        }
    }
}