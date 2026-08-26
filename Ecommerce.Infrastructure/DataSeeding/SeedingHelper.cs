

using Ecommerce.Domain.Common;
using Ecommerce.Infrastructure.Data;
using System.Reflection;
using System.Text.Json;

namespace Ecommerce.Infrastructure.DataSeeding
{
    internal static class SeedingHelper
    {
        public static async Task SeedFileFromEmbeddedResource<T, Tkey>(StoreDbContext _storeDbContext, string fileName) where T : BaseEntity<Tkey>
        {
            if (_storeDbContext.Set<T>().Any())
            {
                Console.WriteLine($"{typeof(T).Name} already has data, skipping...");
                return;
            }

            var assembly = Assembly.GetExecutingAssembly();

            var resourceName = $"Ecommerce.Infrastructure.DataSeeding.{fileName}";


            using var stream = assembly.GetManifestResourceStream(resourceName);

            if (stream == null)
            {
                return;
            }

            try
            {
                var result = await JsonSerializer.DeserializeAsync<List<T>>(stream);

                if (result != null && result.Any())
                {
                    await _storeDbContext.Set<T>().AddRangeAsync(result);
                    await _storeDbContext.SaveChangesAsync();
                    Console.WriteLine($"Added {result.Count} {typeof(T).Name} records from embedded resource");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading embedded resource {fileName}: {ex.Message}");
                throw;
            }
        }
    }
}
