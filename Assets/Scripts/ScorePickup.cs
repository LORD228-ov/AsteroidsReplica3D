using UnityEngine;

public class ScorePickup : BasePickup
{
    public GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        colorMat = Resources.Load<Material>("Materials/ScorePickup");
        ApplyMaterial();
    }
    public override void Abbility()
    {
        gameManager.ScoreUp(100);

    }
}
