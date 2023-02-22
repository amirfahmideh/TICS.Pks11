using TICS.Pks11;
using TICS.Pks11.Enum;
using TICS.Pks11.Implementions;
namespace TICS.Pks11;
public static class ImplementionFactory
{
    public static IPKS11DllImplemention Factory(TokenCertificateOptions options)
    {
        switch (options.FactoryType)
        {
            case FactoryType.PrivateKeyFile:
                return new PrivateKeyFile(options);
            case FactoryType.PrivateKeyString:
                return new PrivateKeyString(options);
            case FactoryType.EPass3003AutoX64:
                return new EPass3003AutoX64(options);
            default:
                throw new NotImplementedException();
        }
    }
}