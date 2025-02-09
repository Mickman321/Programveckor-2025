using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaCollider : MonoBehaviour
{
    public GameObject krystalExplody;
    LjusStamina staminaScript;
    private void Update()
    {

        staminaScript = GetComponentInParent<LjusStamina>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Ljus"))
        {
            staminaScript.Stamina = 100f;
            Instantiate(krystalExplody, collision.transform.position, Quaternion.identity);
            AudioManager.Instance.PlaySFX("Kristal");
            Destroy(collision.gameObject);
            Destroy(krystalExplody, 3f);

        }
        


    }
}
