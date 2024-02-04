﻿namespace Store.Services.CartApi.Dto;

public class CartAddDetailsDto
{
   
    public string? CartDetailsId { get; set; } = default!;
    public string? CartHeaderId { get; set; } = default!;
    public string ProductId { get; set; } = default!;
    public int Count { get; set; }
}
