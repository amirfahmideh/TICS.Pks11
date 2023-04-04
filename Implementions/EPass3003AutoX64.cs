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

        private void TokensInfo(Pkcs11X509Store store)
        {
            if (store == null || store.Slots.Count() <= 0)
            {
                Console.WriteLine($"Hardware token not detected on servers");
                return;
            }
            foreach (var slot in store.Slots)
            {
                Console.WriteLine($"Number of certificate in slot: {slot.Token.Info}: {slot.Token.Certificates.Count()}");
                foreach (var cer in slot.Token.Certificates)
                {
                    Console.WriteLine($"Certificate label: {cer.Info.Label}, Id: {cer.Info.Id}");
                }
            }
        }

        private Pkcs11X509Certificate? GetCertificate(Pkcs11X509Store store, string tokenLabel, string certLabel)
        {
            TokensInfo(store);
            try
            {
                if (store == null || store.Slots.Count() <= 0)
                {
                    Console.WriteLine($"Hardware token not detected on servers");
                    return null;
                }
                Pkcs11Token? token = store?.Slots.FirstOrDefault(p => p.Token.Info.Label == tokenLabel)?.Token;
                if (token == null)
                {
                    Console.WriteLine($"unable to find hardware token with `{tokenLabel}` label");
                    return null;
                }
                var certificate = token?.Certificates.FirstOrDefault(p => p.Info.Label == certLabel || p.Info.Id == certLabel);
                if (certificate == null)
                {
                    Console.Write($"unable to find certificate with `{certLabel}` label");
                    return null;
                }
                return certificate;
            }
            catch (Exception e)
            {
                throw new Exception($"unable to find certificate in TokenLabel:`{tokenLabel}` slot, with `{certLabel}` certificate!", e);
            }
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
                throw new Exception($"unable to find private key in hardware token. Options Values: Factory Type: {Options.FactoryType}, Store Token Lable: {Options.StoreTokenLabel}, Token Lable: {Options.TokenLabel}, Token Pin Code:{Options.TokenPinCode}", e);
            }
        }
    }
}