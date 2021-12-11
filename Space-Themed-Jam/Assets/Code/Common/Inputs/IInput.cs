using UnityEngine;

public interface IInput
{
    void Configure(ActionBindings actionBindings);
    Vector2 GetDirection();
}
