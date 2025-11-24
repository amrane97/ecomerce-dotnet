namespace Ordering.Application.Dtos;

public record PaymentDto(string CardName, string CardNumber, string Cvv, string DateExpire, int paymentMethod);
