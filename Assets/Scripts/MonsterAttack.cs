using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    public GameObject CaveMonster;
    public GameObject Monster;
    public Animator AttackAnimator;
    public Animator HandsAnimator;
    public PlayerMove PlayerMove;
    public PlayerLook PlayerLook;
    public Camera PlayerCamera;

    public float DistanceToFigth = 3f;
    public float MinTime = 2.2f;
    public float MaxTime = 2.4f;

    private bool attacking = false;
    private bool canAttack = false;
    private float attackStartTime;
    
    // Start is called before the first frame update
    void Start()
    {
        Monster.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!attacking)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                // StartAttack();
            }

            if (Vector3.Distance(transform.position, CaveMonster.transform.position) < DistanceToFigth)
            {
                StartAttack();
            }
        }
        else
        {
            if (canAttack && Input.GetButtonDown("Fire1"))
            {
                canAttack = false;
                HandsAnimator.SetBool("Attacking", true);
                float timeOffset = Time.time - attackStartTime;
                Debug.Log("Time offset " + timeOffset);
                if (timeOffset > MinTime && timeOffset < MaxTime)
                {
                    Debug.Log("Hit!");
                    AttackAnimator.SetTrigger("Hit");
                }
            }
            else
            {
                HandsAnimator.SetBool("Attacking", false);
            }
        }
    }

    private void StartAttack()
    {
        attacking = true;
        canAttack = true;
        attackStartTime = Time.time;
        PlayerMove.enabled = false;
        PlayerLook.enabled = false;
        CaveMonster.SetActive(false);
        Monster.SetActive(true);
        // PlayerCamera.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        AttackAnimator.SetTrigger("Attack");
    }
}
