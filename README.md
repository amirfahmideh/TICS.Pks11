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

```dotnet
dotnet add package TICS.Pks11
```

## Usage

### Dependency Injection Configuration (optional)

```c#
services.AddSingleton(new TokenCertificateOptions { FactoryType = config.FactoryType, RootDirectory = rootDirectory, StoreTokenLabel = config.StoreTokenLabel, TokenLabel = config.TokenLabel, TokenPinCode = config.TokenPinCode, PrivateKey = config.PrivateKey });
```

### Get RSA Private Key to sign

```c#
TICS.Pks11.Pks11 pks = new TICS.Pks11.Pks11(_options);
var rsaPrivateKey = pks.GetPrivateKey();
```

### Sign sample (کلاس مورد نیاز برای سامانه مودیان)

```c#
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

### TokenCertificateOptions

```c#
public class TokenCertificateOptions
{
    public FactoryType FactoryType { get; set; }
    public string RootDirectory { get; set; } = default!;
    public string StoreTokenLabel { get; set; } = default!;
    public string TokenLabel { get; set; } = default!;
    public string TokenPinCode { get; set; } = default!;
    public string PrivateKey { get; set; } = default!;
}
```
1. RootDirectory: path to your dll file
2. StoreTokenLabel: When factory type is 1 or 2 Name of token label (sample: ePass3003Auto), you can find this name on root of your hardware token
3. TokenLabel: When factory type is 1 or 2 Name of token label (sample: ePass3003Auto), lable of token in Hardware token, each token can contains more than one token (sample: Amir Fahmideh [stamp])
4. TokenLabel: When factory type is 1 or 2 pin code of token
5. PrivateKey: only fill this property when use factory type 3

### Factory Type
```c#
   public enum FactoryType
    {
        PrivateKeyFile = 0,
        EPass3003AutoX64 = 1,
        EPass3003AutoX86 = 2,
        PrivateKeyString = 3
    }
```
1. PrivateKeyFile: Use this type when you have privateKey.pem next to your root direcotry
2. EPass3003AutoX64: Use this type when you want to have sign with private key in x64 
3. EPass3003AutoX86: Use this type when you want to have sign with private key in x86
4. PrivateKeyString: Use this type when private key field is not empty in your injected configurations setting 

---

می تونی برای اینکه تونستی راحت تر و سریع تر به نتیجه دلخواهت برسی من رو یک قهواه مهمون کنی

<a href="https://www.coffeebede.com/amirfahmideh"><img class="img-fluid" src="https://coffeebede.ir/DashboardTemplateV2/app-assets/images/banner/default-yellow.svg" /></a>
