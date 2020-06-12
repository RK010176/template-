using Common;

namespace Game
{
    public interface IPlayer
    {
        PlayerSpecs PlayerSpecs { get; set; }
        void GetLavelData();

    }
}