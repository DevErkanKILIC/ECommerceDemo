using ECommerce.Application.ViewModels.Products;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Validators.Product
{
    public class CreateProductValidator : AbstractValidator<VMCreateProduct>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lütfen ürün adını boş bırakmayınız")
                .MaximumLength(150)
                .MinimumLength(5)
                .WithMessage("Ürün adının 5 ile 150 karakter arasında olması gerekmektedir");

            RuleFor(p => p.Stock)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lütfen stok bilgisini boş bırakmayınız")
                .Must(s => s >= 0)
                .WithMessage("Stok değeri 0 veya 0'dan büyük olmalıdır.");

            RuleFor(p => p.Price)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lütfen tutar bilgisini boş bırakmayınız")
                .Must(p => p > 0)
                .WithMessage("Tutar değeri 0'dan büyük olmalıdır.");
        }
    }
}
