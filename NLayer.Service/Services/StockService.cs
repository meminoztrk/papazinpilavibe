using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class StockService : Service<Stock>, IStockService
    {
        public StockService(IGenericRepository<Stock> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
