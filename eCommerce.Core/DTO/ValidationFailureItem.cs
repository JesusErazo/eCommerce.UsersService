namespace eCommerce.Core.DTO;

public record ValidationFailureItem(
  string propertyName,
  string errorMessage
  );
