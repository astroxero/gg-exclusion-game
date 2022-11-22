using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class GunScript : MonoBehaviour
{

    Rigidbody rb;
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public ParticleSystem muzzleFleesh;
    public float impactForce = 30f;
    public float gubbyNumber = 0f;
    public TextMeshProUGUI ammoDisplay;

    public Camera fpsCam;

    private float nextTimeToFire = 0f;

    public int maxAmmo = 10;
    public int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;
    public Animator animatr;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentAmmo = maxAmmo;

    }

    void OnEnable()
    {
        isReloading = false;
        animatr.SetBool("Reloading", false);
    }

    // Update is called once per frame
    void Update()
    {
        ammoDisplay.text = currentAmmo.ToString();
        if (currentAmmo == 0)
        {
            ammoDisplay.text = "Reloading";
        }
        if (isReloading)
        {
            return;
        }
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

    }

    IEnumerator Reload()
    {
        isReloading = true;
        if (gubbyNumber == 1)
        {
            animatr.SetBool("gub2", true);
        }
        animatr.SetBool("Reloading", true);
        Debug.Log("Reloading");

        yield return new WaitForSeconds(reloadTime);

        animatr.SetBool("Reloading", false);
        animatr.SetBool("gub2", false);
        currentAmmo = maxAmmo;
        isReloading = false;
        

    }

    void Shoot ()
    {
        animatr.SetBool("Shooting", true);
        currentAmmo--;
        muzzleFleesh.Play();
        RaycastHit hitInfo;
        {
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitInfo, range))
            {
                Debug.Log(hitInfo.transform.name);

                Target target = hitInfo.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.takeDamage(damage);
                }
                if (hitInfo.rigidbody != null)
                {
                    hitInfo.rigidbody.AddForce(-hitInfo.normal * impactForce);
                }
            }
        }
        animatr.SetBool("Shooting", false);
    }
}
