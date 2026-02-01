using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponControl : MonoBehaviour
{
    public enum WeaponType
    {
        Shotgun,
        Pistol,
        Rifle,

        Count
    }

    public GameObject ShotgunImage;
    public GameObject PistolImage;
    public GameObject RifleImage;

    private GameObject[] WeaponAssetLook;
    
    public TextMeshProUGUI TotalAmmoCounter;
    public GameObject[] AmmoInGunObjects;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        WeaponAssetLook = new GameObject[(int)WeaponType.Count];
        WeaponAssetLook[(int)WeaponType.Pistol] = PistolImage;
        WeaponAssetLook[(int)WeaponType.Shotgun] = ShotgunImage;
        WeaponAssetLook[(int)WeaponType.Rifle] = RifleImage;

        SetActiveWeapon(WeaponType.Shotgun);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateWeaponAmmoUI(int shotsAvailable, int ammoOverall)
    {
        for (int i = 0; i < AmmoInGunObjects.Length; ++i)
        {
            bool enabledShotCounter = i < shotsAvailable; //Shot counting is one based but array index is zero based. 1 shot, zero enabled, 2 shots, zero + one enabled
            AmmoInGunObjects[i].SetActive(enabledShotCounter);
        }

        TotalAmmoCounter.text = $"{ammoOverall}";
    }

    void SetActiveWeapon(WeaponType weapon)
    {
        //Iterate over enum indexed lookup, disable all except the one with the matching enum
        for(int i = 0; i < WeaponAssetLook.Length; ++i)
        {
            GameObject imageObject = WeaponAssetLook[i]; //be safe this is not expected to run frequently
            if (imageObject)
            {
                imageObject.SetActive((int)weapon == i);
            }
        }
    }
    
}
