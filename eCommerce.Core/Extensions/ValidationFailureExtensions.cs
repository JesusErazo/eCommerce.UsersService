﻿using eCommerce.Core.DTO;
using FluentValidation.Results;


namespace eCommerce.Core.Extensions
{
  public static class ValidationFailureExtensions
  {    
    public static ValidationFailureResponse ToBasicFormat(this IEnumerable<ValidationFailure> failures)
    {
      List<ValidationFailureItem> errors = failures.Select(f => new ValidationFailureItem(f.PropertyName, f.ErrorMessage)).ToList();
      return new ValidationFailureResponse(errors);
    }
  }
}
