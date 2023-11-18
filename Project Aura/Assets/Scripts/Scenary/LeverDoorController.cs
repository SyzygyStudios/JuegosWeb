using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverDoorController : MonoBehaviour
{

    public void Open()
    {
        if (gameObject.CompareTag("Floor"))
        {
            gameObject.GetComponent<MovingPlatform>().SetMove(true);
        }

        if (gameObject.CompareTag("Door"))
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
