using Common;

namespace Game
{
    public interface IElement
    {
        LevelGameElement GameElementSpecs { get; set; }
        void ProcessSpecs();

    }
}
