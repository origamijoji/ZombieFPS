using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimation : MonoBehaviour
{
    public Animator ani;

    private void Awake() {
        ani = gameObject.GetComponent<Animator>();
    }

    public void Fire() {
        ani.SetTrigger("Fire");
    }

    public void Reload() {
        ani.SetTrigger("Reload");
    }

    public void IsEmpty(bool value) {
        ani.SetBool("Empty", value);
    }
}
