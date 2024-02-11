namespace Weather.Services.CartApi.Dtos.LiqPay;

public class LiqPayCheckout
{
    // --- Обов'язкові параметри:
    public int version { get; set; } 
    public string public_key { get; set; } = default!;
    public string action { get; set; } = default!;
    public decimal amount { get; set; } = default!;
    public string currency { get; set; } = default!;
    public string description { get; set; } = default!;
    public string order_id { get; set; } = default!;

    // --- Не обов'язкові:


    public string card { get; set; } = default!;
    public string phone { get; set; } = default!;
    public string card_exp_month { get; set; } = default!;
    public string card_exp_year { get; set; } = default!;
    public string card_cvv { get; set; } = default!;


    public string expired_date { get; set; } = default!;
    public string language { get; set; } = default!;
    public string paytypes { get; set; } = default!;
    public string result_url { get; set; } = default!;
    public string server_url { get; set; } = default!;


}
