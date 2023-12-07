using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class GunSounds : MonoBehaviour
    {
        public AudioSource reloadSound;
        public AudioSource shootingSound;

        public void PlayShootSound()
        {
            shootingSound.Play();
        }
        public void PlayReloadSound()
        {
            reloadSound.Play();
        }
    }
}