using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsController : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private Transform dashTransform;
    [SerializeField] private Transform rollTransform;
    [SerializeField] private Transform bombJumpTransform;
    [SerializeField] private Transform gravityTransform;
    [SerializeField] private Transform doubleJumpTransform;
    [SerializeField] private Transform landTransform;
    [SerializeField] private Transform jumpTransform;
    
    [SerializeField] private GameObject doubleJump;
    [SerializeField] private GameObject gravity;
    [SerializeField] private GameObject smokeJump;
    [SerializeField] private GameObject smokeLand;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetRoll(bool a)
    {
        if (a)
        {
            transform.position = rollTransform.position;
        }
        animator.SetBool("isRolling", a);
    }
    // Update is called once per frame
    public void StartDashEffect()
    {
        transform.position = dashTransform.position;
        animator.SetBool("isDashing", true);
    }
    public void StopDashEffect()
    {
        animator.SetBool("isDashing", false);
    }

    public void PickPower(int i)
    {
        switch (i)
        {
            case 1:
                animator.SetTrigger("PickBlue");
                break;
            case 2:
                animator.SetTrigger("PickRed");
                break;
            case 3:
                animator.SetTrigger("PickGreen");
                break;
            case 4:
                animator.SetTrigger("PickCian");
                break;
            case 5:
                animator.SetTrigger("PickPurple");
                break;
            case 6:
                break;
        }

    }
    public void StartBombJumpEffect()
    {
        transform.position = bombJumpTransform.position;
        animator.SetBool("isBombJumping", true);
    }
    public void StopBombJumpEffect()
    {
        animator.SetBool("isBombJumping", false);
    }
    public void StartRollEffect()
    {
        transform.position = rollTransform.position;
        animator.SetBool("isRolling", true);
    }
    public void StopRollEffect()
    {
        animator.SetBool("isRolling", false);
    }
    public void StartGravityEffect()
    {
        GameObject a = Instantiate(gravity, gravityTransform);
        a.transform.position = gravityTransform.position;
        a.transform.parent = null;
    }

    public void CreateDoubleJumpEffect()
    {
        GameObject a = Instantiate(doubleJump, doubleJumpTransform);
        a.transform.position = doubleJumpTransform.position;
        a.transform.parent = null;
    }
    
    public void CreateSmokeJumpEffect()
    {
        GameObject a = Instantiate(smokeJump, jumpTransform);
        a.transform.position = jumpTransform.position;
        a.transform.parent = null;
    }
    
    public void CreateSmokeLandEffect()
    {
        GameObject a = Instantiate(smokeLand, landTransform);
        a.transform.position = landTransform.position;
        a.transform.parent = null;
    }
}
