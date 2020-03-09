﻿using System;
using cqrs_Test.Application.Models.Query;
using FluentValidation;

namespace cqrs_Test.Application.UseCase.Product.Command.PostProduct
{
    public class PostProductCommandValidation : AbstractValidator<PostProductCommand>
    {
        public PostProductCommandValidation()
        {
            RuleFor(x => x.Dataa.Attributes.name).NotEmpty().WithMessage("name can't be empty");
            RuleFor(x => x.Dataa.Attributes.name).MaximumLength(50).WithMessage("max name lenght is 50");
            RuleFor(x => x.Dataa.Attributes.price).NotEmpty().WithMessage("price can't be empty");
            RuleFor(x => x.Dataa.Attributes.price).GreaterThan(1000).WithMessage("price must be greater than 1000");
        }
    }
}
