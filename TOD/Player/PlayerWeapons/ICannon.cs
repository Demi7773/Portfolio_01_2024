using UnityEngine;

public interface ICannon
{
     public void ActivateCannon(Vector3 velocity, Transform shootPosition);

    public float Cooldown();

    public void InitializeCannon(ItemCannon stats);
}

