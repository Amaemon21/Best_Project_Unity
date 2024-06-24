using UnityEngine;

public class MobileInputService : InputService
{
    public override Vector2 Axis => SimpleInputAxis();

    public override bool IsAttackButton()
    {
        return SimpleInput.GetButtonUp(Fire1);
    }
}