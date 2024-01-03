using Assets.Scripts.Weapons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGun : MonoBehaviour
{
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

    public void Shoot()
    {
        transform.LookAt(player.transform.position);
        if (Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hit, gunData.maxDistance) && hit.transform.tag!="Enemy")
        {
            GameObject laser = Instantiate(m_shotPrefab, muzzle.position, muzzle.rotation);
            gunSound.PlayShootSound();
            laser.GetComponent<ShotBehavior>().setTarget(hit.point);
            //TODO: random skew angle within crosshair
            IDamageable damageable = hit.transform.GetComponent<IDamageable>();
            damageable?.Damage(gunData.damage);
            Destroy(laser, 2f);
            //OnGunShot();
        }
        
    }










}