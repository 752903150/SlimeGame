using UnityEngine;
public interface InjuredInterface
{
    // Start is called before the first frame update
    void SetPlayer(Rigidbody2D rigidbody2D);
    void NormalInjured(InjuredModel injured,InjuredModel attackModel);
}
