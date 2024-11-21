using ErrorOr;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.SharedKernel.Validations
{
    public static class ErrorOrFluentValidationExtensions
    {
        public static List<Error> ToFailure(this List<ValidationFailure> errors)
        {
            return errors.ToCustom(ErrorType.Failure);
        }

        public static List<Error> ToUnexpected(this List<ValidationFailure> errors)
        {
            return errors.ToCustom(ErrorType.Unexpected);
        }

        public static List<Error> ToValidation(this List<ValidationFailure> errors)
        {
            return errors.ToCustom(ErrorType.Validation);
        }

        public static List<Error> ToConflict(this List<ValidationFailure> errors)
        {
            return errors.ToCustom(ErrorType.Conflict);
        }

        public static List<Error> ToNotFound(this List<ValidationFailure> errors)
        {
            return errors.ToCustom(ErrorType.NotFound);
        }

        public static List<Error> ToCustom(this List<ValidationFailure> errors, ErrorType errorType)
        {
            return errors.ToCustom((int)errorType);
        }

        public static List<Error> ToCustom(this List<ValidationFailure> errors, int errorType)
        {
            return errors?.ConvertAll((ValidationFailure error) => Error.Custom(errorType, error.PropertyName ?? error.ErrorCode, error.ErrorMessage)) ?? Enumerable.Empty<Error>().ToList();
        }

        public static Error ToCustom(this ValidationFailure error, int errorType)
        {
            return Error.Custom(errorType, error.PropertyName ?? error.ErrorCode, error.ErrorMessage);
        }
    }
}
