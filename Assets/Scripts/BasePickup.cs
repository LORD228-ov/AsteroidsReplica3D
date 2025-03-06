using UnityEngine;

public abstract class BasePickup : MonoBehaviour
{
    public Material colorMat;
    public SpaceShip player;
    void Start()
    {
        ApplyMaterial();
        player = FindFirstObjectByType<SpaceShip>();
    }

    protected void ApplyMaterial()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null && colorMat != null)
        {
            renderer.material = colorMat;
        }
    }

    public abstract void Abbility();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            Abbility();
            Destroy(gameObject);
        }
    }

}
