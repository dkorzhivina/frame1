using frame1.Models;

namespace frame1.Services
{
    public interface ITaskService
    {
        IEnumerable<TaskItem> GetAll();
        TaskItem? GetById(int id);
        TaskItem Create(TaskItem item);
    }
}
