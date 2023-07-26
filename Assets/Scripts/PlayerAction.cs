using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField]
    private GunSelector gunSelector;
    [SerializeField]
    private bool autoReload = true;
    [SerializeField]
    private Animator gunAnimator;
    private bool isReloading;

    private void Update()
    {
        if (gunSelector.activeGun != null && !isReloading)
        {
            gunSelector.activeGun.Tick(Input.GetMouseButtonDown(0)); 
        }
        if (ManualReload())
        {
            gunSelector.activeGun.StartReload();
            isReloading = true;
            gunAnimator.SetTrigger("Reload");
        }
    }

    private void EndReload()
    {
        gunSelector.activeGun.EndReload();
        isReloading = false;
    }

    private bool AutoReload()
    {
        return !isReloading && autoReload && gunSelector.activeGun.ammoConfigSO.currentClipAmmo == 0 && gunSelector.activeGun.CanReload();
    }

    private bool ManualReload()
    {
        return !isReloading && Input.GetKeyUp(KeyCode.R) && gunSelector.activeGun.CanReload();
    }
}
