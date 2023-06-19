using UnityEngine;

public class WeaponSwitch : MonoBehaviour, IDataPersistance
{
    public static int selected = 0;
    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
        int previousSelected = selected;
        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selected >= transform.childCount - 1)
                selected = 0;
            else
                selected++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selected <= 0)
                selected = transform.childCount - 1;
            else
                selected--;
        }
        if (previousSelected != selected)
            SelectWeapon();
    }
    public void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selected) {
                weapon.gameObject.GetComponent<Gun>().textChange();
                weapon.gameObject.SetActive(true); }
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
        if (selected == 0 || selected == 3)
        {
            PlayerMovement.speed = 10f;
        }
        if (selected == 1)
        {
            PlayerMovement.speed = 6f;
        }
        if (selected == 2)
        {
            PlayerMovement.speed = 13f;
        }
    }
    public void AddAmmo(int i)
    {
        transform.GetChild(i).gameObject.SetActive(true);
        transform.GetChild(i).gameObject.GetComponent<Gun>().AddAmmo();
        transform.GetChild(i).gameObject.SetActive(false);
        SelectWeapon();
    }

    public void LoadData(GameData data)
    {
        foreach(Transform weapon in transform)
        {
            weapon.gameObject.SetActive(true);
        }
        transform.GetChild(0).gameObject.GetComponent<Gun>().ammo = data.ammo1;
        transform.GetChild(1).gameObject.GetComponent<Gun>().ammo = data.ammo2;
        transform.GetChild(2).gameObject.GetComponent<Gun>().ammo = data.ammo3;
        transform.GetChild(3).gameObject.GetComponent<Gun>().ammo = data.ammo4;
        transform.GetChild(0).gameObject.GetComponent<Gun>().fired = data.fired1;
        transform.GetChild(1).gameObject.GetComponent<Gun>().fired = data.fired2;
        transform.GetChild(2).gameObject.GetComponent<Gun>().fired = data.fired3;
        transform.GetChild(3).gameObject.GetComponent<Gun>().fired = data.fired4;
        transform.GetChild(0).gameObject.GetComponent<Gun>().killed = data.killed1;
        transform.GetChild(1).gameObject.GetComponent<Gun>().killed = data.killed2;
        transform.GetChild(2).gameObject.GetComponent<Gun>().killed = data.killed3;
        transform.GetChild(3).gameObject.GetComponent<Gun>().killed = data.killed4;
        transform.GetChild(0).gameObject.GetComponent<Gun>().name = data.name1;
        transform.GetChild(1).gameObject.GetComponent<Gun>().name = data.name2;
        transform.GetChild(2).gameObject.GetComponent<Gun>().name = data.name3;
        transform.GetChild(3).gameObject.GetComponent<Gun>().name = data.name4;
        transform.GetChild(0).gameObject.GetComponent<Gun>().hits = data.hits1;
        transform.GetChild(1).gameObject.GetComponent<Gun>().hits = data.hits2;
        transform.GetChild(2).gameObject.GetComponent<Gun>().hits = data.hits3;
        transform.GetChild(3).gameObject.GetComponent<Gun>().hits = data.hits4;
        foreach (Transform weapon in transform)
        {
            weapon.gameObject.SetActive(false);
        }
        SelectWeapon();
    }

    public void SaveData(ref GameData data)
    {
        foreach (Transform weapon in transform)
        {
            weapon.gameObject.SetActive(true);
        }
        data.ammo1 = transform.GetChild(0).gameObject.GetComponent<Gun>().ammo;
        data.ammo2 = transform.GetChild(1).gameObject.GetComponent<Gun>().ammo;
        data.ammo3 = transform.GetChild(2).gameObject.GetComponent<Gun>().ammo;
        data.ammo4 = transform.GetChild(3).gameObject.GetComponent<Gun>().ammo;
        data.fired1 = transform.GetChild(0).gameObject.GetComponent<Gun>().fired;
        data.fired2 = transform.GetChild(1).gameObject.GetComponent<Gun>().fired;
        data.fired3 = transform.GetChild(2).gameObject.GetComponent<Gun>().fired;
        data.fired4 = transform.GetChild(3).gameObject.GetComponent<Gun>().fired;
        data.killed1 = transform.GetChild(0).gameObject.GetComponent<Gun>().killed;
        data.killed2 = transform.GetChild(1).gameObject.GetComponent<Gun>().killed;
        data.killed3 = transform.GetChild(2).gameObject.GetComponent<Gun>().killed;
        data.killed4 = transform.GetChild(3).gameObject.GetComponent<Gun>().killed;
        data.name1 = transform.GetChild(0).gameObject.GetComponent<Gun>().name;
        data.name2 = transform.GetChild(1).gameObject.GetComponent<Gun>().name;
        data.name3 = transform.GetChild(2).gameObject.GetComponent<Gun>().name;
        data.name4 = transform.GetChild(3).gameObject.GetComponent<Gun>().name;
        data.hits1 = transform.GetChild(0).gameObject.GetComponent<Gun>().hits;
        data.hits2 = transform.GetChild(1).gameObject.GetComponent<Gun>().hits;
        data.hits3 = transform.GetChild(2).gameObject.GetComponent<Gun>().hits;
        data.hits4 = transform.GetChild(3).gameObject.GetComponent<Gun>().hits;
        foreach (Transform weapon in transform)
        {
            weapon.gameObject.SetActive(false);
        }
        SelectWeapon();
    }
}
