using System.ComponentModel;

namespace Zattini.Domain.Enums
{
    public enum AddressTypeAddressType
    {
        [Description("avenida")]
        Avenida,
        [Description("rua")]
        Rua,
        [Description("praça")]
        Praça,
        [Description("quadra")]
        Quadra,
        [Description("estrada")]
        Estrada,
        [Description("alameda")]
        Alameda,
        [Description("ladeira")]
        Ladeira,
        [Description("travessa")]
        Travessa,
        [Description("rodovia")]
        Rodovia,
        [Description("praia")]
        Praia,
        [Description("outros")]
        Outros
    }
}
