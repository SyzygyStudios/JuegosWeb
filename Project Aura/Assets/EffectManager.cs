using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float lifespan;
    void Start()
    {
        StartCoroutine(Die());
    }

    // Update is called once per frame
    private IEnumerator Die()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(this.gameObject);
    }
}
