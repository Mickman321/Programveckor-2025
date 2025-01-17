using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LjusStamina : MonoBehaviour
{
    public float Stamina = 100;
    public float MaxStamina = 100f;
    public bool staminaD = false;

    public void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            staminaD = true;

            print("Working");
        } if (Input.GetKeyUp(KeyCode.Space))
        {
            staminaD = false;

            print("Working");
        }
        if (staminaD == true)
        {
            Stamina = Stamina -= 1 * Time.deltaTime * 10;
        }

        if (Stamina < 1f)
        {
            staminaD = false;
        }

    }
}
