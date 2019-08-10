using System;
using FluentValidation;
using DDD.Domain.Entities;

namespace DDD.Service.Validators
{
    public class AccountValidator : AbstractValidator<Account>
    {
        public AccountValidator()
        {
            RuleFor(c => c)
                    .NotNull()
                    .OnAnyFailure(x =>
                    {
                        throw new ArgumentNullException("Can't find the object.");
                    });

            RuleFor(c => c.UserId)
                .NotEmpty().WithMessage("Is necessary to inform client ID.")
                .NotNull().WithMessage("Is necessary to inform client ID.");

            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Is necessary to inform the account ID.")
                .NotNull().WithMessage("Is necessary to inform the account ID.");

            RuleFor(c => c.Balance)
                .NotEmpty().WithMessage("Is necessary to inform the account balance.")
                .NotNull().WithMessage("Is necessary to inform the account balance.");

            RuleFor(c => c.CreditLimit)
                .NotEmpty().WithMessage("Is necessary to inform the credit limit.")
                .NotNull().WithMessage("Is necessary to inform the credit limit.");
        }
    }
}