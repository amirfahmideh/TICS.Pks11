namespace TICS.Pks11;

using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.HighLevelAPI41;
using Net.Pkcs11Interop.X509Store;

public class Pks11
{
    private TokenCertificateOptions _options { get; set; }
    private string _privateKey { get; set; }
    /// <summary>
    /// سازنده کلاس Pks برای احراز هویت 
    /// </summary>
    /// <param name="options"></param>
    public Pks11(TokenCertificateOptions options)
    {
        _options = options;
    }

    public RSA? GetPrivateKey()
    {
        string key = string.Empty;
        IPKS11DllImplemention f = ImplementionFactory.Factory(_options);
        return f.GetPrivateKey();
    }
}
