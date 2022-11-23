using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    
    public float delay = 3f;
    public float radius = 5f;
    public float force = 700f;
    public float damageTooken = 20f;

    float countdown;
    bool hasExploded = false;

    public GameObject explosionEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0 && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode ()
    {
        Instantiate(explosionEffect, transform.position, transform. rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null) 
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
            Target targ = nearbyObject.GetComponent<Target>();
            TargetPlayer targP = nearbyObject.GetComponent<TargetPlayer>();
            if (targ != null)
            {
                targ.takeDamage(damageTooken);
            } 
            if (targP != null)
            {
                targP.takeDamage(damageTooken);
            }
        }
        
        
        Destroy(gameObject);
    }
}
