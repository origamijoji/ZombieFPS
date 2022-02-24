using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    [System.Serializable]
    public class WeaponModel {
        public string name;
        public GameObject model;
        public WeaponAnimation wepAni;
    }
    public WeaponAnimation currentAnimator;

    public List<WeaponModel> weapons;

    public void SetActiveWeapon(string name) {
        DisableAllWeapons();
        foreach (WeaponModel weapon in weapons) {
            if(weapon.name == name) {
                weapon.model.SetActive(true);
                currentAnimator = weapon.wepAni;
            }
        }
    }

    public WeaponAnimation CurrentWeaponAnimator() {
        return currentAnimator;
    }

    public IEnumerator SwitchWeapons(string newWeapon, float switchTime) {
        DisableAllWeapons();
        yield return new WaitForSeconds(switchTime);
        SetActiveWeapon(newWeapon);
        yield break;
    }

    private void DisableAllWeapons() {
        foreach(WeaponModel weapon in weapons) {
            weapon.model.SetActive(false);
        }
    }
}
