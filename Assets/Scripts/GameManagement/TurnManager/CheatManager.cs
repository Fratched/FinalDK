using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameCheats.EnemiesDisabled = !GameCheats.EnemiesDisabled;
            Debug.Log("EnemiesDisabled is now: " + GameCheats.EnemiesDisabled);
        }
    }
}
