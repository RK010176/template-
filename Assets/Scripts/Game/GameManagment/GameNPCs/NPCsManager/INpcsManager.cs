
using Common;

namespace Game
{
    public interface INpcsManager
    {
        Level _level { get; set; }
        void AddNpcs();
    }
}