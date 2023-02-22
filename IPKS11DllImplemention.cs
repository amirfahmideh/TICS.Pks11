using System.Security.Cryptography;
using TICS.Pks11;

public interface IPKS11DllImplemention
{
    TokenCertificateOptions Options { get; set; }
    public string DllName { get; }
    public string DllPath { get; }
    public RSA? GetPrivateKey();
}