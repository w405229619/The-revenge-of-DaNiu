using UnityEngine;
using System.Collections;
using System;

public class Controller : MonoBehaviour
{
    public AudioSource workVoice;

    public MPJoystick moveJoystick;

    public float speed = 10.0f;

    CharacterController cc;

    float Quaternion_Y;

    float touchKey_x;

    float touchKey_y;

    bool isUse = false;

    Vector3 transformValue = new Vector3(0, -0.1f, 0);

    public void Awake()
    {
        cc = GetComponent<CharacterController>();

    }

    void Start()
    {

    }

    void Update()
    {

        if (GetComponent<Animation>().IsPlaying("SpellCastA"))
            return;
        if (GetComponent<Animation>().IsPlaying("MagicShotStraight"))
            return;
        //animation.Play("FireBallSpell");
        //return;
        touchKey_x = moveJoystick.position.x;

        touchKey_y = moveJoystick.position.y;

        if (moveJoystick.position.x == 0 && moveJoystick.position.y == 0)
        {
            touchKey_y = Input.GetAxis("Vertical");

            touchKey_x = Input.GetAxis("Horizontal");
        }

        if ((touchKey_x != 0 || touchKey_y != 0))
        {
            Quaternion_Y = float.Parse(Math.Atan(touchKey_y / touchKey_x) * 180 / Math.PI + "");

            if (touchKey_x > 0 && touchKey_y > 0)
            {

            }
            else if (touchKey_x < 0 && touchKey_y > 0)
            {
                Quaternion_Y = Quaternion_Y + 180;
            }
            else if (touchKey_x < 0 && touchKey_y < 0)
            {
                Quaternion_Y = Quaternion_Y + 180;
            }
            else if (touchKey_x > 0 && touchKey_y < 0)
            {
                Quaternion_Y = Quaternion_Y + 360;
            }

            transform.rotation = Quaternion.Euler(0.0f, -Quaternion_Y + 90, 0.0f);

            //如果 用键盘控制，会有这个问题哟
            if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") == 0)
            {
                transform.rotation = Quaternion.Euler(0.0f, -Quaternion_Y - 90, 0.0f);
            }

            //Debug.Log(Input.GetAxis("Horizontal") + ":" + Input.GetAxis("Vertical"));

            transformValue.x = touchKey_x * Time.deltaTime * 4;

            transformValue.z = touchKey_y * Time.deltaTime * 4;
        }
        else
        {

            transformValue.x = 0 * Time.deltaTime * 4;

            transformValue.z = 0 * Time.deltaTime * 4;
        }



        if (transformValue.x == 0 && transformValue.z == 0)
        {
            if (GetComponent<Animation>().IsPlaying("Run"))
                GetComponent<Animation>().Play();

            workVoice.Stop();
        }
        else
        {
            
            GetComponent<Animation>().Play("Run");
            if (!workVoice.isPlaying)
            {
                workVoice.Play();
            }
        }

        cc.Move(transformValue);

     


    }


    void OnCollisionEnter(Collision collisionInfo)
    {

        if ("Respawn" == collisionInfo.transform.tag)
        {
            Debug.Log("sdfsdfsdf");
        }
    }

}
