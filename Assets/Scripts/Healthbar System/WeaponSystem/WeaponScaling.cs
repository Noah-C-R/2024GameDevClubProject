using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponScaling : MonoBehaviour
{
    private static WeaponScaling _instance;

    public static WeaponScaling Instance { get { return _instance; } }

    [SerializeField]
    private Transform weaponTransform;

    [SerializeField]
    private Vector3 originalScale, scaleVector;

    [SerializeField]
    private Slider debugHealthbar;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        weaponTransform = transform; //put this script on the weapon
        originalScale = transform.localScale;
        debugHealthbar.maxValue = originalScale.z;
    }

    public void ScaleWeapon(float factor)
    {
        var scaler = originalScale.z * factor;
        debugHealthbar.value = scaler;
        scaleVector = new Vector3(weaponTransform.localScale.x, weaponTransform.localScale.y, scaler);
        weaponTransform.localScale = scaleVector;
    }

    public void ResetWeapon()
    {
        weaponTransform.localScale = originalScale;
    }
}
