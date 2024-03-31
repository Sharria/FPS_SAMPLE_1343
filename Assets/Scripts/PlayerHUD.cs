using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField] TMP_Text currentAmmoText;
    [SerializeField] TMP_Text maxAmmoText;

    //Sharria Code
    [SerializeField] Gun gunRef;

    FPSController player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<FPSController>();
    }

    //Sharri'a Methods
    //------------------
    void Update(){
        //UpdateCurrentAmmo(gunRef.ammo);
    }
    void UpdateCurrentAmmo(int currentAmmo){
        currentAmmoText.text = currentAmmo.ToString();
    }
}



