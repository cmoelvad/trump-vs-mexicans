using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallColiderScript : MonoBehaviour
{
    private WallController parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = gameObject.GetComponentInParent<WallController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("colided!");
        if (collision.transform.name.ToLower().Contains("floor"))
        {
            parent.isGrounded = true;
        }
    }

}
