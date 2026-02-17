using frame1.Models;

namespace frame1.Services
{
    public class TaskService : ITaskService
    {
        private readonly List<TaskItem> _items = new();
        private int _nextId = 1;
        private readonly object _lock = new();

        public IEnumerable<TaskItem> GetAll()
        {
            return _items;
        }

        public TaskItem? GetById(int id)
        {
            return _items.FirstOrDefault(x => x.Id == id);
        }

        public TaskItem Create(TaskItem item)
        {
            Validate(item);

            lock (_lock)
            {
                item.Id = _nextId++;
                _items.Add(item);
            }

            return item;
        }

        private void Validate(TaskItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Title))
                throw new ArgumentException("Заголовок не может быть пустым");

            if (item.Difficulty < 1 || item.Difficulty > 5)
                throw new ArgumentException("Сложность должна быть от 1 до 5");

            if (item.EstimatedHours < 0)
                throw new ArgumentException("Расчетные часы не могут быть отрицательными");
        }
    }
}
