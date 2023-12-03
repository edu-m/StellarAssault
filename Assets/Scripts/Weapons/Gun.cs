using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform cam;
    [SerializeField] private DisplayAmmo displayAmmo;
    [SerializeField] public GameObject m_shotPrefab;

    float timeSinceLastShot;

    private void Start()
    {
        displayAmmo = GameObject.Find("Canvas").GetComponent<DisplayAmmo>();
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
        displayAmmo.UpdateAmmo(gunData.currentAmmo,gunData.magSize);
    }

    private void OnDisable() => gunData.isReloading = false;

    public void StartReload()
    {
        //Debug.Log("Reloading Weapon!");
        if (!gunData.isReloading && this.gameObject.activeSelf)
            StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        gunData.isReloading = true;
        yield return new WaitForSeconds(gunData.reloadTime);
        gunData.currentAmmo = gunData.magSize;
        displayAmmo.UpdateAmmo(gunData.currentAmmo, gunData.magSize);
        gunData.isReloading = false;
    }

    private bool CanShoot() => !gunData.isReloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    private void Shoot()
    {
        //Debug.Log("Shoot");
        if (gunData.currentAmmo > 0)
        {
            if (CanShoot())
            {
                if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, gunData.maxDistance))
                {
                    GameObject laser = Instantiate(m_shotPrefab, cam.position, cam.rotation);
                    laser.GetComponent<ShotBehavior>().setTarget(hit.point);
                    //TODO: random skew angle within crosshair
                    IDamageable damageable = hit.transform.GetComponent<IDamageable>();
                    damageable?.Damage(gunData.damage);
                    Destroy(laser, 2f);
                }

                gunData.currentAmmo--;
                timeSinceLastShot = 0;
                OnGunShot();
                //Debug.Log("Current ammo: "+gunData.currentAmmo);    
            }
        }
        else StartReload();
        displayAmmo.UpdateAmmo(gunData.currentAmmo,gunData.magSize);
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        //Debug.DrawRay(cam.position, cam.forward * gunData.maxDistance);
    }

    private void OnGunShot() { }
}