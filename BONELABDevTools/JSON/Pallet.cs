namespace BonelabDevMode.JSON
{
    public class Pallet
    {
        public int version;
        public required Dictionary<string, PalletObject> objects;
    }

    public class PalletObject
    {
        public required string barcode;

        public required string title;

        public required ISA isa;
    }

    public class ISA
    {
        public required string type;
    }
}