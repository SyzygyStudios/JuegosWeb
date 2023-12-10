using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
            gameObject.GetComponentInChildren<TilemapCollider2D>().enabled = false;
            Destroy(this.gameObject);
        }
    }
}
