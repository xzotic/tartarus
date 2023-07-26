using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Pool;

[CreateAssetMenu(fileName = "Gun", menuName = "Guns/Gun", order = 0)]

public class GunsSO : ScriptableObject
{
    //public ImpactType impactType;
    public GameObject impact;
    public GunType type;
    public string Name;
    public GameObject ModelPrefab;
    public Vector3 SpawnPoint;
    public Vector3 SpawnRotation;
    public int shotsPerShot;
    public Recoil recoilComponent;

    public DamageConfigSO damageConfigSO;
    public ShootConfigSO shootConfigSO;
    public TrailConfigSO trailConfigSO;
    public AmmoConfigSO ammoConfigSO;
    public AudioConfigSO audioConfigSO;

    private Transform ori;

    private MonoBehaviour activeMonoBehaviour;
    private AudioSource shootingAudioSource;
    private GameObject model;
    private float lastShootTime;
    private float initialClickTime;
    private float stopShootingTime;
    private bool shootLastFrame;
    private ParticleSystem shootSystem;
    //private ObjectPool<TrailRenderer> trailPool;
    private TrailRenderer trail;

    public void Spawn(Transform parent, MonoBehaviour activeMonoBehaviour)   //spawn gun model
    {
        this.activeMonoBehaviour = activeMonoBehaviour;
        lastShootTime = 0;
        //trail = CreateTrail();

        model = Instantiate(ModelPrefab);
        model.transform.SetParent(parent, false);
        model.transform.localPosition = SpawnPoint;
        model.transform.localRotation = Quaternion.Euler(SpawnRotation);

        recoilComponent = model.transform.parent.GetComponentInParent<Recoil>();
        shootSystem = model.GetComponentInChildren<ParticleSystem>();
        shootingAudioSource = model.GetComponent<AudioSource>();
        ori = model.transform.parent;
    }

    public void Tick(bool wantsToShoot)
    {
        //Debug.Log("a");
        /*if (wantsToShoot)
        {
            shootLastFrame = true;
            Shoot();
        }
        else if (!wantsToShoot && shootLastFrame)
        {
            stopShootingTime = Time.time;
            shootLastFrame = false;
        }*/
        if (wantsToShoot && ammoConfigSO.currentClipAmmo > 0) Shoot();
        //Shoot();
    }

    private IEnumerator PlayTrail(Vector3 StartPoint, Vector3 EndPoint, RaycastHit Hit )
    {
        TrailRenderer instance = CreateTrail();
        instance.gameObject.SetActive(true);
        instance.transform.position = StartPoint;
        yield return null;
        instance.emitting = true;
        float distance = Vector3.Distance(StartPoint, EndPoint);
        float remainingDistance = distance;
        while(remainingDistance > 0)
        {
            instance.transform.position = Vector3.Lerp(
                StartPoint, EndPoint, Mathf.Clamp01(1 - (remainingDistance / distance)));
            remainingDistance -= trailConfigSO.SimSpeed * Time.deltaTime;
            yield return null;
        }
        instance.transform.position = EndPoint;

        if (Hit.collider != null)
        {
            //SurfaceManager.Instance.HandleImpact(Hit.transform.gameObject, EndPoint, Hit.normal, ImpactType, 0);
            GameObject go = Instantiate(impact, Hit.point, Quaternion.identity);
            //audioConfigSO.PlayImpactClip(go.GetComponent<AudioSource>());
            yield return new WaitForSeconds(0.5f);
            Destroy(go);
            if (Hit.collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(damageConfigSO.GetDamage(distance));
            }
        }

        yield return new WaitForSeconds(trailConfigSO.duration);
        yield return null;
        instance.emitting = false;
        instance.gameObject.SetActive(false);
        Destroy(instance.gameObject);
    }  //spawn trail

    private TrailRenderer CreateTrail()  //make trail
    {
        GameObject instance = new GameObject("Bullet Trail");
        TrailRenderer trail = instance.AddComponent<TrailRenderer>();
        trail.colorGradient = trailConfigSO.color;
        trail.material = trailConfigSO.material;
        trail.widthCurve = trailConfigSO.WidthCurve;
        trail.minVertexDistance = trailConfigSO.minVertexDistance;
        trail.emitting = false;
        trail.time = trailConfigSO.time;
        trail.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        return trail;
    }

    public bool CanReload()
    {
        return ammoConfigSO.CanReload();
    }
    public void StartReload()
    {
        audioConfigSO.PlayReloadClip(shootingAudioSource);
    }
    public void EndReload()
    {
        ammoConfigSO.Reload();
    }

    public void Shoot()
    {
        
        /*if (Time.time - lastShootTime - shootConfigSO.FireRate > Time.deltaTime)
        {
            float lastDuration = Mathf.Clamp(0, (stopShootingTime - initialClickTime), 1f);
            float lerpTime = (shootConfigSO.recoilRecovery - (Time.time - stopShootingTime)) / shootConfigSO.recoilRecovery;
            initialClickTime = Time.time - Mathf.Lerp(0,lastDuration, Mathf.Clamp01(lerpTime));
        }*/
        if (Time.time > shootConfigSO.FireRate + lastShootTime)
        {
            recoilComponent.StartRecoil(0.2f, 20f, 15f);
            lastShootTime = Time.time;
            if (ammoConfigSO.currentClipAmmo == 0)
            {
                audioConfigSO.PlayNoAmmoClip(shootingAudioSource);
                return;
            }
            shootSystem.Play();
            audioConfigSO.PlayShootingClip(shootingAudioSource);

            ammoConfigSO.currentClipAmmo --;

            for (int i=0; i < shotsPerShot; i++)
            {
                Vector3 shootDirection = ori.forward
                    + new Vector3(
                        Random.Range(
                            -shootConfigSO.Spread.x,
                            shootConfigSO.Spread.x
                        ),
                        Random.Range(
                            -shootConfigSO.Spread.y,
                            shootConfigSO.Spread.y
                        ),
                        Random.Range(
                            -shootConfigSO.Spread.z,
                            shootConfigSO.Spread.z
                        )
                    );
                shootDirection.Normalize();

                if (Physics.Raycast(
                        ori.position,
                        shootDirection,
                        out RaycastHit hit,
                        float.MaxValue,
                        shootConfigSO.Hitmask
                    ))
                {
                    activeMonoBehaviour.StartCoroutine(
                        PlayTrail(
                            shootSystem.transform.position,
                            hit.point,
                            hit
                        )
                    );
                }
                else
                {
                    activeMonoBehaviour.StartCoroutine(
                        PlayTrail(
                            shootSystem.transform.position,
                            shootSystem.transform.position + (shootDirection * trailConfigSO.MissDistance),
                            new RaycastHit()
                        )
                    );
                }
            }
        }
    }
}
