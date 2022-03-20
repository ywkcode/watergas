using NetCoreFrame.SocketConsole.Model;
using NetCoreFrame.SocketConsole.Repository;
using System.Threading.Tasks;

namespace NetCoreFrame.SocketConsole.Service
{
    public class Water_QualityService : Repository<Water_Quality>
    {
        public async Task<bool> AddData(Water_Quality model)
        {
            return (await this.InsertAsync(model));
        }
    }
}
