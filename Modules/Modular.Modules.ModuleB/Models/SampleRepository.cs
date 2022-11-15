using Modular.Core.Domain;

namespace Modular.Modules.ModuleB.Models
{
    public class SampleRepository : IRepository<Sample>
    {
        public void Add(Sample entity)
        {
            //throw new NotImplementedException();
        }

        public IQueryable<Sample> Query()
        {
            return new List<Sample>().AsQueryable();
        }

        public void Remove(Sample entity)
        {
            //throw new NotImplementedException();
        }

        public void SaveChange()
        {
            //throw new NotImplementedException();
        }
    }
}
