using Assets.Scripts.Weapons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGun : MonoBehaviour
{
    float timeSinceLastShot;
    [Header("References")]
    [SerializeField] private GunData gunData;
    GunSounds gunSound;
    [SerializeField] private Transform muzzle;
    [SerializeField] private GunSounds gunSounds;
    public GameObject m_shotPrefab;
    public Transform player;

   

    private void Start()
    {
        gunSound = GetComponent<GunSounds>();
    }

    private bool CanShoot() => !gunData.isReloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);


    public void Shoot()
    {
        transform.LookAt(player.transform.position);
        if (CanShoot() && Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hit, gunData.maxDistance) && hit.transform.tag!="Enemy")
        {
            GameObject laser = Instantiate(m_shotPrefab, muzzle.position, muzzle.rotation);
            gunSound.PlayShootSound();
            laser.GetComponent<ShotBehavior>().setTarget(hit.point);
            IDamageable damageable = hit.transform.GetComponent<IDamageable>();
            damageable?.Damage(gunData.damage);
            Destroy(laser, 2f);
            OnGunShot();
        }
        
    }
    private void OnGunShot()
    {
        timeSinceLastShot = 0;
        gunSounds.PlayShootSound();
    }

    private void Update() => timeSinceLastShot += Time.deltaTime;










}