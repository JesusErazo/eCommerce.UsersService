namespace eCommerce.Core.DTO;

public record ValidationFailureResponse(
  List<ValidationFailureItem> errors
  );
