using System.Security.Cryptography;
using Net.Pkcs11Interop.X509Store;

namespace TICS.Pks11.Implementions
{

    public class EPass3003AutoX64 : IPKS11DllImplemention
    {
        public string DllName => "ShuttleCsp11_3003.dll";

        public string DllPath => "dll//ShuttleCsp11_3003_X64.dll";

        public EPass3003AutoX64(TokenCertificateOptions _options)
        {
            Options = _options;
        }

        public TokenCertificateOptions Options { get; set; }

        private Pkcs11X509Certificate? GetCertificate(Pkcs11X509Store store, string tokenLabel, string certLabel)
        {
            Pkcs11Token? token = store?.Slots.FirstOrDefault(p => p.Token.Info.Label == tokenLabel)?.Token;
            return token?.Certificates.FirstOrDefault(p => p.Info.Label == certLabel);
        }

        public RSA? GetPrivateKey()
        {
            try
            {
                string key = string.Empty;
                IPKS11DllImplemention f = ImplementionFactory.Factory(Options);
                string pkcs11LibraryPath = $"{Options.RootDirectory}//{f.DllPath}";
                RSA? p11PrivKey = null;
                var store = new Pkcs11X509Store(pkcs11LibraryPath, new EPass3003AutoX64PinProvider(Options.TokenPinCode, Options.StoreTokenLabel));
                Pkcs11X509Certificate? cert1 = GetCertificate(store, Options.StoreTokenLabel, Options.TokenLabel);
                p11PrivKey = cert1?.GetRSAPrivateKey();
                return p11PrivKey;
            }
            catch (Exception e)
            {
                throw new Exception("در پیدا کردن کلید خصوصی در توکن سخت افزاری خطایی رخداده است", e);
            }
        }
    }
}