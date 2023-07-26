using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[DisallowMultipleComponent]
[RequireComponent(typeof(TextMeshProUGUI))]
public class AmmoDisplay : MonoBehaviour
{
    [SerializeField]
    private GunSelector gs;
    [SerializeField] private TextMeshProUGUI text;

    private void Awake()
    {
        if (gs == null) gs = GameObject.FindGameObjectWithTag("Player").GetComponent<GunSelector>();
        text = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        text.SetText(
            $"{gs.activeGun.ammoConfigSO.currentClipAmmo} / " +
            $"{gs.activeGun.ammoConfigSO.currentAmmo}"
            );
    }
}
