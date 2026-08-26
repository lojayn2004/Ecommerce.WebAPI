

namespace Ecommerce.Domain.Common
{
    public class BaseEntity<Tkey>
    {
        public Tkey Id { get; set; } = default!;

        public string Name { get; set; } = "";
    }
}
