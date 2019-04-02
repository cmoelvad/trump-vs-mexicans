using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrusher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.transform.name.ToLower().Contains("trump") && !collision.transform.name.ToLower().Contains("wall"))
        {
            var toBeCrushed = collision.transform.GetComponent<IDamageable>();
            if(toBeCrushed != null)
            {
                toBeCrushed.AddDamage(toBeCrushed.GetHealth());
            }
        }
    }
}
