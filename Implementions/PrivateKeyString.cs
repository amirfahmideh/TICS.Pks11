using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Net.Pkcs11Interop.X509Store;

namespace TICS.Pks11.Implementions
{

    public class PrivateKeyString : IPKS11DllImplemention
    {
        public PrivateKeyString(TokenCertificateOptions _Options)
        {
            Options = _Options;
        }
        public TokenCertificateOptions Options { get; set; }

        public string DllName => string.Empty;

        public string DllPath => string.Empty;

        public RSA? GetPrivateKey()
        {
            try
            {
                if (string.IsNullOrEmpty(Options.PrivateKey))
                    throw new Exception("کلید خصوصی به درستی مقدار دهی نشده است، جدول تنظیمات و نوع احراز هویت را بررسی کنید");
                var rsa = RSA.Create();
                rsa.ImportFromPem(this.Options.PrivateKey.ToCharArray());
                return rsa;
            }
            catch (Exception e)
            {
                throw new Exception("در پیدا کردن کلید خصوصی در رشته خطایی رخداده است", e);
            }
        }
    }
}