using System.Text;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.X509Store;

public class EPass3003AutoX64PinProvider : IPinProvider
{
    private byte[]? _pin = null;
    private string _storeLabel = "";
    public EPass3003AutoX64PinProvider(string pin, string storeLabel)
    {
        _pin = ConvertUtils.Utf8StringToBytes(pin);
        _storeLabel = storeLabel;
    }

    public GetPinResult GetKeyPin(Pkcs11X509StoreInfo storeInfo, Pkcs11SlotInfo slotInfo, Pkcs11TokenInfo tokenInfo, Pkcs11X509CertificateInfo certificateInfo)
    {
        if (tokenInfo.Label == _storeLabel)
            return new GetPinResult(false, _pin);
        else
            return new GetPinResult(true, null);
    }

    public GetPinResult GetTokenPin(Pkcs11X509StoreInfo storeInfo, Pkcs11SlotInfo slotInfo, Pkcs11TokenInfo tokenInfo)
    {
        if (tokenInfo.Label == _storeLabel)
            return new GetPinResult(false, _pin);
        else
            return new GetPinResult(true, null);
    }
}