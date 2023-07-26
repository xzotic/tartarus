using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Damage Config", menuName = "Guns/Ammo Config", order = 1)]

public class AmmoConfigSO : ScriptableObject
{
    public int maxAmmo = 120;
    public int clipSize = 6;
    public int currentAmmo = 120;
    public int currentClipAmmo = 6;

    public void Reload()
    {
        /*int maxReloadAmount = Mathf.Min(clipSize, currentAmmo);
        int availableBulletsInCurrentClip = clipSize - currentClipAmmo;
        int reloadAmount = Mathf.Min(maxReloadAmount, availableBulletsInCurrentClip);       //ammo conserving
        currentClipAmmo = currentClipAmmo + reloadAmount;
        currentAmmo -= reloadAmount;*/

        int reloadAmount = Mathf.Min(clipSize, currentAmmo);
        currentClipAmmo = reloadAmount;
        currentAmmo -= reloadAmount; 
    }

    public bool CanReload()
    {
        return currentClipAmmo < clipSize && currentAmmo > 0;
    }
}
