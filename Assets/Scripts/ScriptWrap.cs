using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class ScriptWrap : MonoBehaviour
{
    private float leftSide, rightSide, topSide, botSide;
    private Rigidbody rb;
    private float botpos = -13f;
    private float topPos = 13f;
    private float rightPos = 23f;
    private float leftPos = -23f;
    void Start()
    {
        rb = GetComponent<Rigidbody> ();
        Debug.Log(rightSide);
        Debug.Log(leftSide);
        Debug.Log(topSide);
        Debug.Log(botSide);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        //float rightSide = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x;
        //float leftSide = Camera.main.ScreenToWorldPoint(new Vector2(0f, 0f)).x;
        //float topSide = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).z;
        //float botSide = Camera.main.ScreenToWorldPoint(new Vector2(0f, 0f)).z;

        if (screenPos.x <= 0 && rb.linearVelocity.x < 0)
        {
            transform.position = new Vector3(rightPos, 0f, transform.position.z);
        }
        else if (screenPos.x >= Screen.width && rb.linearVelocity.x > 0)
        {
            transform.position = new Vector3(leftPos, 0f, transform.position.z);
        }
        else if (gameObject.transform.position.z >= topPos && rb.linearVelocity.z > 0)
        {
            transform.position = new Vector3(transform.position.x, 0f, botpos);
        }
        else if (gameObject.transform.position.z <= botpos && rb.linearVelocity.z < 0)
        {
            transform.position = new Vector3(transform.position.x, 0f, topPos);
        }
    }
}
