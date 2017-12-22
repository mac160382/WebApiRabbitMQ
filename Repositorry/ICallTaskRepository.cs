using CallCenterModel;

namespace Repository
{
    public interface ICallTaskRepository
    {
        void Add(Task task);

        Task Update(Task task);
    }
}