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

    public List<WeaponModel> weapons;

    void Update() {

    }
    private void Start() {
        foreach(WeaponModel weapon in weapons) {
            weapon.wepAni = gameObject.GetComponent<WeaponAnimation>();
        }
    }

    public void SetActiveWeapon(string name) {

        switch (name) {
            case "M1911":

                break;
        }
    }

    //public WeaponAnimation currentWeaponAnimator() {
        
    //}

    private void DisableAllWeapons() {
    }
}
