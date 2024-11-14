using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject hitBox;

    private bool isSlashing = false;

    private InputReader inputReader;

    // Start is called before the first frame update
    void Start()
    {
        inputReader = InputReader.Instance;
        hitBox = transform.Find("WeaponHitbox").gameObject;
        inputReader.OnAttackPerformed += StartSlash;
    }
    private void OnDisable()
    {
        inputReader.OnAttackPerformed -= StartSlash;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSlash()
    {
        if(!isSlashing)
        StartCoroutine(WeaponSlash());

    }

    private IEnumerator WeaponSlash()
    {
        // <--start playing animation on this line
        isSlashing = true;

        yield return new WaitForSeconds(PlayerCooldowns.SWING_WINDUP);

        hitBox.SetActive(true);

        yield return new WaitForSeconds(PlayerCooldowns.SWING_DURATION); //<-- animation's duration should match swing duration

        hitBox.SetActive(false);

        yield return new WaitForSeconds(PlayerCooldowns.SWING_COOLDOWN);
        isSlashing = false;
    }
}
