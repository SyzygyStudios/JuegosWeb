using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SelectNick : MonoBehaviour
{
    public InputField inputText;
    public Text Nick;
    public Image Light;
    public GameObject acceptbutton;


    private void Awake()
    {
        Light.color = Color.red;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if(Nick.text.Length <= 3)
        {
            Light.color = Color.red;
            acceptbutton.SetActive(false);
        }

        if (Nick.text.Length > 3)
        {
            Light.color = Color.green;
            acceptbutton.SetActive(true);
        }
    }

    public void accept()
    {
        PlayerPrefs.SetString("name",inputText.text);
        SceneManager.LoadScene("Main");
    }
}
