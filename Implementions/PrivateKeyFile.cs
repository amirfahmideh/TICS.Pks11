using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Net.Pkcs11Interop.X509Store;

namespace TICS.Pks11.Implementions
{

    public class PrivateKeyFile : IPKS11DllImplemention
    {
        public PrivateKeyFile(TokenCertificateOptions _Options)
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
                var rsa = RSA.Create();
                var path = Options.RootDirectory + "\\privateKey.pem";
                if (!System.IO.Path.Exists(path))
                {
                    throw new Exception("در این روش احراز هویت فایل کلید خصوصی با آدرس مشخص شده اجباری می باشد" + " FilePath:" + path);
                }
                var pem = System.IO.File.ReadAllText(path);
                rsa.ImportFromPem(pem.ToCharArray());
                return rsa;
            }
            catch (Exception e)
            {
                throw new Exception("در پیدا کردن کلید خصوصی در فایل خطایی رخداده است", e);
            }
        }
    }
}