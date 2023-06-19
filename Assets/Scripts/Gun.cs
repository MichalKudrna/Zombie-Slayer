using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public GameObject ammoText;
    public float damage;
    public float range;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public float fireRate;
    public float nextToFire = 0f;
    public int ammo;
    public int maxAmmo;
    public AudioSource zvuk;
    public int fired=0;
    public int killed=0;
    public string name="";
    public float hits=0;
    public float powerTime = 10f;
    void Update()
    {
        if (Input.GetButton("Fire1")&&Time.time>=nextToFire&&ammo>0&&!PauseGame.isPaused)
        {
            nextToFire = Time.time + 1f / fireRate;
            Shoot();
            fired++;
        }
        if (Player.powerUp)
        {
            powerTime -= Time.deltaTime;
            if (powerTime <= 0) Player.powerUp = false;
        }
    }
    void Shoot()
    {
        muzzleFlash.Play();
        zvuk.Play();
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Enemy enemy = hit.transform.root.GetComponent<Enemy>();
            hits++;
            if (enemy != null)
            {
                if (enemy.hp < damage)
                {
                    killed++;
                }
                if(Player.powerUp)
                    enemy.TakeDamage(damage*4);
                else enemy.TakeDamage(damage);
            }
        }
        ammo--;
        textChange();
    }
    public void AddAmmo()
    {
        ammo += maxAmmo/10;
        if (ammo > maxAmmo) ammo = maxAmmo;
    }
    public void textChange()
    {
        ammoText.GetComponent<Text>().text = "" + ammo;
    }
    public void novaHlasitost(float novy)
    {
        zvuk.volume = novy;
    }
}
