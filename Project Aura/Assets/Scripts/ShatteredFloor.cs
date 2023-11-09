using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatteredFloor : MonoBehaviour
{

    [SerializeField] private float destroyTime;
    
    public void Break()
    {
        StartCoroutine(BreakHandler());
    }
    
    private IEnumerator BreakHandler()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(this.gameObject);
    }
}
