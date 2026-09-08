using MassTransit;
using Common.Events;
using Modules.Products.Application.Repositories;
using System.Threading.Tasks;

namespace Modules.Products.Infrastructure.Consumers;

public class CategoryDeletedEventConsumer : IConsumer<CategoryDeletedEvent>
{
    private readonly IProductRepository _productRepository;

    public CategoryDeletedEventConsumer(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Consume(ConsumeContext<CategoryDeletedEvent> context)
    {
        // RabbitMQ-dan gələn mesajın içindəki ID-ni oxuyuruq
        int deletedCategoryId = context.Message.categoryId;

        // 1. O kateqoriyaya aid bütün məhsulları xüsusi metodla tapırıq
        var products = await _productRepository.GetProductsByCategoryAsync(deletedCategoryId);

        // 2. Tapılan məhsulları silirik (və ya sənin məntiqinlə Update edib IsDeleted = true edirik)
        if (products != null)
        {
            foreach (var product in products)
            {
                _productRepository.Remove(product);
            }

            // 3. Verilənlər bazasını yeniləyirik
            await _productRepository.SaveChangesAsync();
        }
    }
}