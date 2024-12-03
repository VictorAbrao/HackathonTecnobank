using FluentValidation;
using Hackathon.AppService.Commands.Requests.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.AppService.Validators.Keywords
{
    internal class CreateKeywordCommandRequestValidator : AbstractValidator<CreateKeywordCommandRequest>
    {
    }

    public class DeleteByIdKeywordCommandRequestValidator: AbstractValidator<DeleteByIdKeywordCommandRequest> { }
    public class UpdateKeywordCommandRequestValidator : AbstractValidator<UpdateKeywordCommandRequest> { }
}
