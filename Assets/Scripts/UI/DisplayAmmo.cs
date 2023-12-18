using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayAmmo : MonoBehaviour
{
    [SerializeField]
    private TMP_Text ammoText;

    private void Start()
    {
        //ammoText.text = "hello world!";
    }
    public void UpdateAmmo(int ammo, int maxAmmo)
    {
        ammoText.text = ammo + "/" + maxAmmo;
    }
}
