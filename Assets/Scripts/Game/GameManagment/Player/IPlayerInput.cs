using System;

namespace Game
{
    public interface IPlayerInput
    {
        bool FireWeapons { get; set; }
        event Action OnFire;

        bool Forward { get; set; }
        bool Backword { get; set; }
        bool Right { get; set; }
        bool Left { get; set; }
        bool PanLeft { get; set; }
        bool PanRight { get; set; }
        bool Idle { get; set; }
        bool Up { get; set; }
        bool Down { get; set; }
        bool SpaceBar { get; set; }
    }
}