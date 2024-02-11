using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using Weather.Services.CartApi.Dtos.LiqPay;

namespace Weather.Services.CartApi.Helpers;

public class LiqPayHelper
{
     private readonly string _private_key;
     private readonly string _public_key;

    public LiqPayHelper(IConfiguration configuration)
    {
        _public_key = configuration["LiqPay:PublicKey"]!;     // Public Key компанії, який можна знайти в особистому кабінеті на сайті liqpay.ua
        _private_key = configuration["LiqPay:PrivateKey"]!;    // Private Key компанії, який можна знайти в особистому кабінеті на сайті liqpay.ua
    }

    /// <summary>
    /// Сформувати дані для LiqPay (data, signature)
    /// </summary>
    /// <param name="order_id">Номер замовлення</param>
    /// <returns></returns>
     public LiqPayCheckoutFormModel GetLiqPayModel(string order_id, double amount)
    {
        // Заповнюю дані для їх передачі для LiqPay
        var signature_source = new LiqPayCheckout()
        {
            public_key = _public_key,
            version = 3,
            action = "pay",
            amount = (decimal)amount,
            currency = "UAH",
            description = "Оплата замовлення",
            order_id = order_id,

            result_url = "http://localhost:4200/pay/redirect",

        };
        var json_string = JsonConvert.SerializeObject(signature_source);
        var data_hash = Convert.ToBase64String(Encoding.UTF8.GetBytes(json_string));
        var signature_hash = GetLiqPaySignature(data_hash);

        // Данні для передачі у в'ю
        var model = new LiqPayCheckoutFormModel();
        model.Data = data_hash;
        model.Signature = signature_hash;
        return model;
    }

     public LiqPayCheckoutFormModel GetLiqPayModelStatus(string order_id)
    {
        // Заповнюю дані для їх передачі для LiqPay
        var signature_source = new LiqPayCheckout()
        {
            action = "status",
            version = 3,
            public_key = _public_key,
            order_id = order_id,

        };
        var json_string = JsonConvert.SerializeObject(signature_source);
        var data_hash = Convert.ToBase64String(Encoding.UTF8.GetBytes(json_string));
        var signature_hash = GetLiqPaySignature(data_hash);

        // Данні для передачі у в'ю
        var model = new LiqPayCheckoutFormModel();
        model.Data = data_hash;
        model.Signature = signature_hash;
        return model;
    }

    /// <summary>
    /// Формування сигнатури
    /// </summary>
    /// <param name="data">Json string з параметрами для LiqPay</param>
    /// <returns></returns>
     public string GetLiqPaySignature(string data)
    {
        return Convert.ToBase64String(SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(_private_key + data + _private_key)));
    }
}