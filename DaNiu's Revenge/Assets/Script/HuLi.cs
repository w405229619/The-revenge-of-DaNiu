using UnityEngine;
using System.Collections;
using System;

public class HuLi : MonoBehaviour
{

    public enum HuliState
    {
        Run,
        BeAttak,
        Attak,
        Rest,
        Dead
    }

    Transform transform_Player;

    NavMeshAgent navmeshagent;

    Vector3 posOriginal;

    [HideInInspector]
    public  HuliState _HuliState;

    AudioSource miao;

    void Start()
    {
        _HuliState = HuliState.Rest;

        navmeshagent = gameObject.GetComponent<NavMeshAgent>();

        posOriginal = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        navmeshagent.speed = 2;

        miao = GetComponent<AudioSource>();

        transform_Player = GameObject.FindWithTag("Player").transform;
    }

    int i = 0;

    Vector3 transform_Player_fix = new Vector3();

    bool flag = true;

    void Update()
    {
        if (_HuliState == HuliState.Dead)
        {
            navmeshagent.Stop();
            return;
        }

        i++;
        if (i == 1000)
        {
            i = 0;
        }
        if (i % 20 == 0)
        {
            transform_Player_fix.x = transform_Player.position.x + 1.4f;
            transform_Player_fix.y = transform_Player.position.y;
            transform_Player_fix.z = transform_Player.position.z + 1.4f;
            float distance = Vector3.Distance(transform.position, transform_Player.position);

            if (distance < 15)
            {
                if (distance < 2.4)
                {
                    navmeshagent.Stop();
                    transform.LookAt(transform_Player);
                    _HuliState = HuliState.Attak;
                }
                else
                {
                    if (flag)
                    {
                        miao.Play();
                        flag = false;
                    }
                    navmeshagent.SetDestination(transform_Player_fix);
                    _HuliState = HuliState.Run;
                }
            }
            else
            {
                if (i % 100 == 0)
                    navmeshagent.SetDestination(posOriginal);
                if (i % 50 == 0 && Vector3.Distance(transform.position, posOriginal) < 1)
                    _HuliState = HuliState.Rest;
            }
        }



        switch (_HuliState)
        {
            case HuliState.Run:
                GetComponent<Animation>().Play("Run");
                break;
            case HuliState.BeAttak:
                GetComponent<Animation>().Play("");
                break;
            case HuliState.Attak:
                GetComponent<Animation>().Play("Attak");
                break;
            case HuliState.Rest:
                GetComponent<Animation>().Play("Rest");
                break;
            default:
                break;
        }

    }

}
