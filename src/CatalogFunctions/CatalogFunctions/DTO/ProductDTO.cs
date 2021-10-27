using FluentValidation;
using FluentValidation.Results;
using System;

namespace CatalogFunctions.DTO
{
    public class ProductDTO
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public bool Validate()
        {
            ValidationResult validationResult = new ProductDTOValidation().Validate(this);
            return validationResult.IsValid;
        }

        public class ProductDTOValidation : AbstractValidator<ProductDTO>
        {
            public ProductDTOValidation()
            {
                RuleFor(c => c.Price)
                    .NotEmpty()
                    .WithMessage("O preço do produto foi informado.");
            }
        }

    }
}