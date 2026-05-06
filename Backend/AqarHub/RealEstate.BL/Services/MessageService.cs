using RealEstate.DAL.Models;
using RealEstate.DAL.Repositories;

namespace RealEstate.BL.Services
{
    public class MessageService
    {
        private readonly MessageRepository _repo;

        public MessageService(MessageRepository repo)
        {
            _repo = repo;
        }

        public int Add(Message msg) => _repo.Add(msg);

        public List<Message> GetConversation(int u1, int u2)
            => _repo.GetConversation(u1, u2);
    }
}
