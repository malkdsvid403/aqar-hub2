using RealEstate.DAL.Models;
using RealEstate.DAL.Repositories;

namespace RealEstate.BL.Services
{
    public class PaymentService
    {
        private readonly PaymentRepository _repo;

        public PaymentService(PaymentRepository repo)
        {
            _repo = repo;
        }

        public List<Payment> GetAll()
        {
            return _repo.GetAll();
        }

        public List<Payment> GetByUser(int userId)
        {
            return _repo.GetByUser(userId);
        }


        public int Add(Payment payment)
        {
            return _repo.Add(payment);
        }
    }
}
