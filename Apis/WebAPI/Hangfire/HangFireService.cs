﻿using Application.Interfaces;
using Application.Interfaces.Services;
using Application.ViewModels.Batchs;
using Domain.Entities;
using Domain.Enums;
using Hangfire;
using Microsoft.Extensions.Hosting;
using System.Net;
using static Application.Constants.BatchConstant;

namespace WebAPI.Hangfire
{
    public class HangFireService
    {
       
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

            var pendingOrders = orders.Where(x => x.Status == nameof(OrderStatus.Pending)).ToList();
            var nextPendingDriverSession = drivers.Where(x => !x.IsDeleted)
                                           .OrderBy(x => x.Batches.Any())
                                           .ThenBy(x => (x.Batches.FirstOrDefault()?.CreationDate))
                                           .ToList();
            await AddBatches(pendingOrders, drivers, nextPendingDriverSession, nameof(BatchType.Pickup));
            var washedOrders = orders.Where(x => x.Status == nameof(OrderStatus.Washed)).ToList();
            var nextWashedDriverSession = drivers.Where(x => !x.IsDeleted)
                                           .OrderBy(x => x.Batches.Any())
                                           .ThenBy(x => (x.Batches.FirstOrDefault()?.CreationDate))
                                           .ToList();
            await AddBatches(washedOrders, drivers, nextWashedDriverSession, nameof(BatchType.Return));

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
            //int count = pendingOrders.Count();            
            int count = pendingOrders.Count();
            int index = 0;
            int j = 0;
            Batch? batch = null;
            var numOfBatch = (count / 10) > BatchCount ? (count / 10) : BatchCount;
            for (index = 0; index < numOfBatch && nextDriverSession.Count > 0; index++)
            {

                var fromTime = _currentTime.GetCurrentTime().AddHours(2);
                var toTime = _currentTime.GetCurrentTime().AddHours(4);
                batch = new Batch()
                {
                    Type = batchType,
                    FromTime = fromTime,
                    ToTime = toTime,
                    Status = nameof(BatchStatus.Pending),
                    //DriverId = nextDriverSession.First().Id
                };
                //nextDriverSession.RemoveAt(0);
                //iterate from 0 to batch size
                while (j <= BatchSize * index)
                {
                    // if pending order exist then add to batch
                    if (pendingOrders.ElementAtOrDefault(j) != null)
                    {
                        OrderInBatch orderInBatch = new()
                        {
                            BatchId = batch.Id,
                            OrderId = pendingOrders[j].Id,
                            Status = nameof(OrderInBatchStatus.Waiting)
                        };
                        batch.OrderInBatches.Add(orderInBatch);// add order in batch
                    }
                    j++;
                }
                await _unitOfWork.BatchRepository.AddAsync(batch);
            }
            //af ter 3 batch added savechanges
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
