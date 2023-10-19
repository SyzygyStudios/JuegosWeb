using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{


    public GameObject mainMenu;
    public GameObject nick;
    public GameObject acceptBottom;


    //[SerializeField] float movementQuantity;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
        nick.SetActive(false);
        acceptBottom.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
      
        
    }

    public void Play() 
    {
        mainMenu.SetActive(false);
        nick.SetActive(true);
        acceptBottom.SetActive(true);

    }

    public void Exit()
    {
        Application.Quit();
    }



}
