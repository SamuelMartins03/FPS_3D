using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] float firePower = 10f;
    [SerializeField] float range = 100f;

    [SerializeField] Camera fpsCam;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] AudioSource shootSound;

    [SerializeField] GameObject impactEffect;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            TriggerShoot();
        }
    }
    private void TriggerShoot()
    {
        Shoot();
    }
    private void Shoot()
    {
        muzzleFlash.Play();
        shootSound.Play();

        RaycastHit hit;
        //If the raycast hits something in a x range
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            //If the collider is a enemy make damage
           Enemy target = hit.transform.GetComponent<Enemy>();
            if (target != null)
            {
                target.TakeDamage(firePower);
            }
            GameObject impactGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGo, 1.5f);
        }
    }
}
