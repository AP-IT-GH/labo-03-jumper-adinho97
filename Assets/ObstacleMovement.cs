using NUnit.Framework.Internal;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{

    public float Movespeed = 5.5f;
    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.back * Movespeed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
