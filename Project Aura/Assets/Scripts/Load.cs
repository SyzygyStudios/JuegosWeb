using UnityEngine;
using UnityEngine.UI;

public class Load : MonoBehaviour
{
    public Text Nick;

    // Start is called before the first frame update
    void Start()
    {
        Nick.text = PlayerPrefs.GetString("name");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
