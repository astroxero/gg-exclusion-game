using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GrenadeThrower : MonoBehaviour
{

    public float throwForce = 40f;
    public GameObject grenadePrefab;
    public float throwStopTime = 10f; 
    public float maxThrowStopTime = 10f;
    public TextMeshProUGUI countdownTimer;
    public TextMeshProUGUI grenadeButtonText;

    void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }

    // Start is called before the first frame update
    void Start()
    {
        throwStopTime = maxThrowStopTime;
    }

    // Update is called once per frame
    void Update()
    {
        countdownTimer.text = throwStopTime.ToString("0");
        if (throwStopTime == 0)
        {
            countdownTimer.text = null;
            grenadeButtonText.text = "E";
        } else 
        {
            grenadeButtonText.text = null;
        }

        if (throwStopTime > 0)
        {
            throwStopTime -= Time.deltaTime;
        }
        if (throwStopTime < 0)
        {
            throwStopTime = 0;
        }

        if (Input.GetKeyDown("e") && throwStopTime == 0)
        {
            ThrowGrenade();
            throwStopTime = maxThrowStopTime;
        }
    }
}
