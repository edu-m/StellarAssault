using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class GunSounds : MonoBehaviour
    {
        public AudioSource reloadSound;
        public AudioSource shootingSound;
        public float soundRange = 10f;

        public void PlayShootSound()
        {
            shootingSound.Play();
            var shotSound = new Sound(transform.position, soundRange);
            Sounds.MakeSound(shotSound);
        }
        public void PlayReloadSound()
        {
            reloadSound.Play();
        }
    }
}