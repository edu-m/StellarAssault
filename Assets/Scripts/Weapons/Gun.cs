using Assets.Scripts.Weapons;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public static Gun _instance;
    [Header("References")]
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform muzzle;
    [SerializeField] private Transform cameraPosMuzzle;
    [SerializeField] private GunSounds gunSounds;
    public GameObject m_shotPrefab;
    private DisplayAmmo displayAmmo;
    private Slider reloadTimerSlider;
    private GameObject player;
    

    readonly Vector3 UIElementReloadScale = new Vector3(1f, 1f, 0f);

    float timeSinceLastShot;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        gunData.currentAmmo = gunData.magSize;
        displayAmmo = GameObject.Find("CrosshairCanvas").GetComponent<DisplayAmmo>();
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += ShowUIElementReload;
        PlayerShoot.reloadInput += StartReload;
        displayAmmo.UpdateAmmo(gunData.currentAmmo,gunData.magSize);
        reloadTimerSlider = GameObject.Find("ReloadTimerSlider").GetComponent<Slider>();
    }

    private void OnDisable() => gunData.isReloading = false;

    private void HideUIElementReload()
    {
        reloadTimerSlider.transform.localScale = new Vector3(0f, 0f, 0f);
    }

    private IEnumerator UpdateUIElementReload()
    {
        while ((reloadTimerSlider.value += 1f) < 75)
            yield return new WaitForSeconds(gunData.reloadTime / 75);
        Invoke("HideUIElementReload",0f);
        gunData.isReloading = false;
    }
   
    public void ShowUIElementReload()
    {
        if (gunData.currentAmmo == gunData.magSize)
            return;
        if (reloadTimerSlider.IsUnityNull())
            reloadTimerSlider = GameObject.Find("ReloadTimerSlider").GetComponent<Slider>();
        reloadTimerSlider.transform.localScale = UIElementReloadScale;
        reloadTimerSlider.value = 0;
        StartCoroutine(UpdateUIElementReload());
    }

    public void StartReload()
    {
        //Debug.Log("Reloading Weapon!");
        if (!gunData.isReloading && this.gameObject.activeSelf && gunData.currentAmmo < gunData.magSize)
        {
            gunSounds.PlayReloadSound();
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        gunData.isReloading = true;
        yield return new WaitForSeconds(gunData.reloadTime);
        gunData.currentAmmo = gunData.magSize;
        displayAmmo.UpdateAmmo(gunData.currentAmmo, gunData.magSize);
        
    }

    private bool CanShoot() => !gunData.isReloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f) && gunData.currentAmmo > 0;

    public void Shoot()
    {
        if (PlayerShoot.shootInput == null)
        {
            PlayerShoot.shootInput += Shoot;
            PlayerShoot.reloadInput += ShowUIElementReload;
            PlayerShoot.reloadInput += StartReload;
        }
        if (cameraPosMuzzle.IsUnityNull())
            cameraPosMuzzle = GameObject.Find("PlayerCam").transform;
        if (muzzle.IsUnityNull())
            muzzle = GameObject.Find("Muzzle").transform;
        if (CanShoot() && Physics.Raycast(cameraPosMuzzle.position, cameraPosMuzzle.forward, out RaycastHit hit, gunData.maxDistance))
        {
            GameObject laser = Instantiate(m_shotPrefab, muzzle.position, muzzle.rotation);
            laser.GetComponent<ShotBehavior>().setTarget(hit.point);
            //TODO: random skew angle within crosshair
            IDamageable damageable = hit.transform.GetComponent<IDamageable>();
            damageable?.Damage(gunData.damage);
            Destroy(laser, 2f);
            OnGunShot();
        }
        if(gunData.currentAmmo == 0) PlayerShoot.reloadInput?.Invoke();
        displayAmmo.UpdateAmmo(gunData.currentAmmo,gunData.magSize);
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        //Debug.DrawRay(muzzle.position, muzzle.forward * gunData.maxDistance);
    }

    private void OnGunShot() {
        gunData.currentAmmo--;
        timeSinceLastShot = 0;
        gunSounds.PlayShootSound();
    }

    private void OnDestroy()
    {
        StopCoroutine(Reload());
    }
}