# TICS.Pks11
**C# .net standard class library** for exporting private key from hardwareToken, privateKey.pem, privateKey as string
# Description 
Find private RSA key to sign with Hardware token, private Pem File and private key

# How to find private key string from hardware token?
When you work more on RSA algorithms, you need private key to sign data, but it's not possible to export private key as string 
and it's really correct, only token owner should sign documents
When you work with hardware token you can find private key as RSA KEY and sign data with it.


---
# Rsa.ir (ره آورد سامانه های امن)

این سازمان به عنوان ارایه دهنده توکن های سخت افزاری حجم زیادی از توکن ها را در اختیار صاحبان کسب و کار قرار داده است
نمونه کد های ارایه شدن به عنوان مشخصات فنی فاقد نمونه کد مناسب برای زبان سی شارپ می باشد 

# سامانه مودیان
از این کد در امضا درخواست ها برای سیستم ارایه شده توسط دارایی با عنوان سامانه مودیان استفاده شده است 

# Install

1. Add nuget packge 
```
dotnet add package TICS.Pks11
```

## Usage
### Dependency Injection Configuration (optional)
```
services.AddSingleton(new TokenCertificateOptions { FactoryType = config.FactoryType, RootDirectory = rootDirectory, StoreTokenLabel = config.StoreTokenLabel, TokenLabel = config.TokenLabel, TokenPinCode = config.TokenPinCode, PrivateKey = config.PrivateKey });
```

### Get RSA Private Key to sign 
```
TICS.Pks11.Pks11 pks = new TICS.Pks11.Pks11(_options);
var rsaPrivateKey = pks.GetPrivateKey();
 ```
 
### Sign sample (کلاس مورد نیاز برای سامانه مودیان)
```
public class CustomSign : ISignatory
{
    private TICS.Pks11.TokenCertificateOptions _options;
    public CustomSign(TICS.Pks11.TokenCertificateOptions options)
    {
        _options = options;
    }
    public string GetKeyId()
    {
        return null;
    }
    public string Sign(string data)
    {

        TICS.Pks11.Pks11 pks = new TICS.Pks11.Pks11(_options);
        var rsaPrivateKey = pks.GetPrivateKey();
        if (rsaPrivateKey != null)
        {
            var dataBytes = Encoding.UTF8.GetBytes(data);
            var signString = Convert.ToBase64String(rsaPrivateKey.SignData(dataBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));
            return signString;
        }
        else return string.Empty;
    }
}
```
