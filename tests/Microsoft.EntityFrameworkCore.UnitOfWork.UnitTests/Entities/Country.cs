using System.Collections.Generic;

namespace Hcqn.Infra.EntityFramework.UnitOfWork.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<City> Cities { get; set; }
    }
}
