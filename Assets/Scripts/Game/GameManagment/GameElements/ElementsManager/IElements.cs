using Common;

namespace Game
{
    public interface IElements
    {
        Level _level { get; set; }
        void AddElements();
        void RemoveElements();
    }
}
