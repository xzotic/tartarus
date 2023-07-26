using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GunSelector : MonoBehaviour
{
    [SerializeField]
    private GunType Gun;
    [SerializeField]
    private Transform GunParent;
    [SerializeField]
    private List<GunsSO> Guns;
    //[SerializeField]
    //private PlayerIK

    [Space]
    [Header("Runtime Filled")]
    public GunsSO activeGun;

    private void Start()
    {
        GunsSO gun = Guns.Find(gun => gun.type == Gun);
        if (gun == null)
        {
            Debug.LogError($"No Gun SO found for Guntype:{gun}");
            return;
        }

        activeGun = gun;
        gun.Spawn(GunParent, this);
    }
}
