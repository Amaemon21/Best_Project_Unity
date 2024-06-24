using UnityEngine;

public class StandaloneInputService : InputService
{
    public override Vector2 Axis
    {
        get
        {
            Vector2 axis = SimpleInputAxis();

            if (axis == Vector2.zero)
                axis = UnityAxis();

            return axis;
        }
    }

    public override bool IsAttackButton()
    {
        return Input.GetButtonUp(Fire1);
    }
}