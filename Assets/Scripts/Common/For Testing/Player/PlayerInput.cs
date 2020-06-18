using UnityEngine;

public class PlayerInput : IPlayerInput
{
    public float Vertical => Input.GetAxis("Vertical");
}
