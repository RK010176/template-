namespace App
{
    public interface IPlatformInput
    {
        bool Up();
        bool Down();
        bool Forward();
        bool Backword();
        bool Right();
        bool Left();
        bool PanRight();
        bool PanLeft();
        bool FireWeapons();
        bool SpaceBar();
    }
}