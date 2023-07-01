using Application.Interfaces;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;//check lai register
        private readonly IBaseUserRepository _baseUserRepository;
        private readonly IDriverRepository _driverRepository;//check lai register
        private readonly ISessionRepository _sessionRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IBatchRepository _batchRepository;
        private readonly IBuildingRepository _buildingRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IOrderInBatchRepository _orderInBatchRepository;

        public UnitOfWork(AppDbContext dbContext,
                          IOrderRepository orderRepository,
                          ICustomerRepository userRepository,
                          ISessionRepository timeSlotRepository,
                          IStoreRepository storeRepository,
                          IServiceRepository serviceRepository,
                          IBatchRepository batchRepository,
                          IBuildingRepository buildingRepository,
                          IPaymentRepository paymentRepository,
                          IOrderDetailRepository orderDetailRepository,
                          IDriverRepository driverRepository,
                          IBaseUserRepository baseUserRepository
,
                          IOrderInBatchRepository orderInBatchRepository
,
                          ICustomerRepository customerRepository)

        {
            _dbContext = dbContext;
            _orderRepository = orderRepository;
            _customerRepository = userRepository;
            _sessionRepository = timeSlotRepository;
            _storeRepository = storeRepository;
            _serviceRepository = serviceRepository;
            _batchRepository = batchRepository;
            _buildingRepository = buildingRepository;
            _paymentRepository = paymentRepository;
            _orderDetailRepository = orderDetailRepository;
            _orderInBatchRepository = orderInBatchRepository;
            _baseUserRepository = baseUserRepository;
            _customerRepository = customerRepository;
            _driverRepository = driverRepository;
            _baseUserRepository = baseUserRepository;
            _orderInBatchRepository = orderInBatchRepository;
            _customerRepository = customerRepository;
        }

        public ICustomerRepository CustomerRepository => _customerRepository;
        public IOrderRepository OrderRepository => _orderRepository;

        public IStoreRepository StoreRepository => _storeRepository;

        public IServiceRepository ServiceRepository => _serviceRepository;

        public IBatchRepository BatchRepository => _batchRepository;

        public IBuildingRepository BuildingRepository => _buildingRepository;

        public IDriverRepository DriverRepository => _driverRepository;

        public ISessionRepository SessionRepository => _sessionRepository;

        public IPaymentRepository PaymentRepository => _paymentRepository;

        public IOrderDetailRepository OrderDetailRepository => _orderDetailRepository;

        public IBaseUserRepository UserRepository => _baseUserRepository;

        public IOrderInBatchRepository OrderInBatchRepository => _orderInBatchRepository;


        public int SaveChange()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
