namespace BonelabDevMode.JSON
{
    public class Pallet
    {
        public int version;
        public required Dictionary<string, PalletObject> objects;
        public Dictionary<string, Type> types;
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

    public class Type
    {
        public string type;
        public string fullname;
    }
}