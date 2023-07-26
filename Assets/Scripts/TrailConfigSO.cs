using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Trail Config", menuName = "Guns/Trail Config", order = 4)]

public class TrailConfigSO : ScriptableObject
{
    public Material material;
    public AnimationCurve WidthCurve;
    public float duration = 0.5f;
    public float minVertexDistance = 0.1f;
    public float time = 0.1f;
    public Gradient color;

    public float MissDistance = 100f;
    public float SimSpeed = 100f;
}
