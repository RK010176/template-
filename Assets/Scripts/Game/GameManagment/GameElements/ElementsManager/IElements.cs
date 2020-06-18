using Common;
using System.Collections.Generic;

namespace Game
{
    public interface IElements
    {
        List<LevelGameElement> Elements { get; set; }
        void AddElements();
        void RemoveElements();
    }
}
