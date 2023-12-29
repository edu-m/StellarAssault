using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class GunSounds : MonoBehaviour
    {
        public AudioSource reloadSound;
        public AudioSource shootingSound;
        public float soundRange = 25f;

        public void PlayShootSound()
        {
            shootingSound.Play();
            var shotSound = new Sound(transform.position, soundRange);
            Move.playerShoots = true;
            Sounds.MakeSound(shotSound);
        }
        public void PlayReloadSound()
        {
            reloadSound.Play();
        }
    }
}