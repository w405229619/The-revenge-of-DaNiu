using UnityEngine;
using System.Collections;

public class Boom : MonoBehaviour
{
    public Transform boom;

    AudioSource boom_AudioSource;

    void Start()
    {
        boom_AudioSource = GetComponent<AudioSource>();

    }

    void Update()
    {

    }

    void OnCollisionEnter(Collision collisionInfo)
    {
       
        if ("Fox" == collisionInfo.transform.tag)
        {
            boom_AudioSource.Play();

            Transform boom_Clone = Instantiate(boom) as Transform;

            boom_Clone.position = collisionInfo.transform.position + new Vector3(0, 0, 0);

            GameObject.Destroy(boom_Clone.gameObject, 1);


            NPC _NPC = collisionInfo.transform.GetComponent<NPC>();

            if (_NPC != null && _NPC.HP>0)
            {
                _NPC.HP -= 5;

                if (_NPC.HP == 0)
                {
                    Transform fox = collisionInfo.transform.parent;

                    fox.GetComponent<HuLi>()._HuliState = HuLi.HuliState.Dead;

                    fox.GetComponent<Animation>().Play("Dead");

                    GameObject.Destroy(fox.gameObject, 6);
                }
            }

        }
    }

}
