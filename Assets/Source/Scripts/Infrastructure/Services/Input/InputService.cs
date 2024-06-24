using UnityEngine;

public abstract class InputService : IInputService
{
    protected const string Horizontal = nameof(Horizontal);
    protected const string Vertical = nameof(Vertical);
    protected const string Fire1 = nameof(Fire1);

    public abstract Vector2 Axis { get; }

    public abstract bool IsAttackButton();

    protected Vector2 SimpleInputAxis() => new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
    protected Vector2 UnityAxis() => new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));
}