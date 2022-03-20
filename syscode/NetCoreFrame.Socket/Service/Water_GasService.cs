using NetCoreFrame.SocketConsole.Model;
using NetCoreFrame.SocketConsole.Repository;
using System.Threading.Tasks;

namespace NetCoreFrame.SocketConsole.Service
{
    public class Water_GasService : Repository<Water_Gas>
    {
        public async Task<bool> AddData(Water_Gas model)
        {
            return (await this.InsertAsync(model));
        }
    }


}
