using TICS.Pks11.Enum;
namespace TICS.Pks11;

public class TokenCertificateOptions
{
    public FactoryType FactoryType { get; set; }
    public string RootDirectory { get; set; } = default!;
    public string StoreTokenLabel { get; set; } = default!;
    public string TokenLabel { get; set; } = default!;
    public string TokenPinCode { get; set; } = default!;
    public string PrivateKey { get; set; } = default!;
}
