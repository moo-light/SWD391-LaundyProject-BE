using Application.Interfaces;
using Application.Interfaces.Services;
using Application.ViewModels.Batchs;
using Domain.Entities;
using Domain.Enums;
using Hangfire;
using Microsoft.Extensions.Hosting;

namespace WebAPI.Hangfire
{
    public class HangFireService
    {
        private const int BatchSize = 20;
        public IUnitOfWork _unitOfWork;
        private ICurrentTime _currentTime;

        public HangFireService(ICurrentTime currentTime, IUnitOfWork unitOfWork)
        {
            _currentTime = currentTime;
            _unitOfWork = unitOfWork;
        }

        public async Task AddBatchesForThisSession()
        {
            Console.WriteLine(_currentTime.GetCurrentTime());
            var orders = await _unitOfWork.OrderRepository.GetAllAsync(x => x.OrderInBatches);
            var drivers = await _unitOfWork.DriverRepository.GetAllAsync(x => x.Batches);

            var pendingOrders = orders.Where(x => x.OrderDetails.Any(x => x.Status == nameof(OrderDetailStatus.Pending))).ToList();
            var nextPendingDriverSession = drivers.Where(x => !x.IsDeleted)
                                           .OrderBy(x => x.Batches.Any())
                                           .ThenBy(x => (x.Batches.FirstOrDefault()?.CreationDate))
                                           .ToList();
            if (pendingOrders.Any())
                await AddBatches(pendingOrders, drivers, nextPendingDriverSession, nameof(BatchType.Pickup));
            else
            {
                await Console.Out.WriteLineAsync("There currently no pending orders");
            }
            var washedOrders = orders.Where(x => x.OrderDetails.Any(x => x.Status == nameof(OrderDetailStatus.Washed))).ToList();
            var nextWashedDriverSession = drivers.Where(x => !x.IsDeleted)
                                           .OrderBy(x => x.Batches.Any())
                                           .ThenBy(x => (x.Batches.FirstOrDefault()?.CreationDate))
                                           .ToList();
            if (washedOrders.Any())
                await AddBatches(washedOrders, drivers, nextWashedDriverSession, nameof(BatchType.Return));
            else
            {
                await Console.Out.WriteLineAsync("There currently no washed orders");
            }
            //var batchReturn = new BatchRequestDTO()
            //{
            //    Type = nameof(BatchType.Return),
            //    Status = nameof(BatchStatus.Pending),
            //    Date = _currentTime.GetCurrentTime()
            //};
            //var batchPickup = new BatchRequestDTO()
            //{
            //    Type = nameof(BatchType.Pickup),
            //    Status = nameof(BatchStatus.Pending),
            //    Date = _currentTime.GetCurrentTime()
            //};
            //await _batchService.AddAsync(batchReturn);
            //await _batchService.AddAsync(batchPickup);

        }

        private async Task AddBatches(List<LaundryOrder> pendingOrders, List<Driver> drivers, List<Driver> nextDriverSession, string batchType)
        {
            int count = pendingOrders.Count();
            int index = 0;
            Batch? batch = null;
            do
            {
                if (index % BatchSize == 0)
                {
                    batch = new Batch()
                    {
                        Type = batchType,
                        Status = nameof(BatchStatus.Pending),
                        DriverId = nextDriverSession.First().Id
                    };
                    nextDriverSession.RemoveAt(0);
                }
                OrderInBatch orderInBatch = new OrderInBatch()
                {
                    BatchId = batch.Id,
                    OrderId = pendingOrders[index].Id
                };
                batch.OrderInBatches.Add(orderInBatch);
                await _unitOfWork.BatchRepository.AddAsync(batch);
                index++;
            } while (index != count || drivers.Count > 0);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
