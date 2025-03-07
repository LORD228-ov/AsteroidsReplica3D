using UnityEngine;

public class SpeedPickup : BasePickup
{
    void Start()
    {
        //Initializing objects
        player = FindFirstObjectByType<SpaceShip>();
        colorMat = Resources.Load<Material>("Materials/SpeedPickup");
        ApplyMaterial();
    }
    public override void Abbility()
    {
        Debug.Log("SPEED");
        player.Speedup();
    }
}
