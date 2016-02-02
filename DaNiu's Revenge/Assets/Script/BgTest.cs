using UnityEngine;
using System.Collections;

public class BgTest : MonoBehaviour
{
    public AudioSource youLinHuoQiuVoice;

    public AudioSource bu;

    public Texture btnTextureBlueAndRed;

    public Texture btnFuck;

    public Texture btnFuck1;

    public Texture btnFuck2;

    public Texture btnFuck3;

    public Texture btnFuck4;




 
    //记录上一次手机触摸位置判断用户是在左放大还是缩小手势
    Vector2 oldPosition1;

    Vector2 oldPosition2;

    float distance = 7;

    void Start()
    {

    }


    void Update()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + distance-1, transform.position.z - distance);

        //判断触摸数量为多点触摸
        if (Input.touchCount > 1)
        {
            //前两只手指触摸类型都为移动触摸
            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                //计算出当前两点触摸点的位置
                var tempPosition1 = Input.GetTouch(0).position;
                var tempPosition2 = Input.GetTouch(1).position;
                //函数返回真为放大，返回假为缩小
                if (isEnlarge(oldPosition1, oldPosition2, tempPosition1, tempPosition2))
                {
                    if (distance > 4)
                        distance -= 0.1f;

                    Camera.main.transform.Translate(Vector3.forward * -0.1f);

                }
                else
                {
                    if (distance < 7)
                        distance += 0.1f;
                   
                }
                //备份上一次触摸点的位置，用于对比
                oldPosition1 = tempPosition1;
                oldPosition2 = tempPosition2;
            }
        }

    }

   

    //函数返回真为放大，返回假为缩小
    bool isEnlarge(Vector2 oP1, Vector2 oP2, Vector2 nP1, Vector2 nP2)
    {
        //函数传入上一次触摸两点的位置与本次触摸两点的位置计算出用户的手势
        var leng1 = Mathf.Sqrt((oP1.x - oP2.x) * (oP1.x - oP2.x) + (oP1.y - oP2.y) * (oP1.y - oP2.y));
        var leng2 = Mathf.Sqrt((nP1.x - nP2.x) * (nP1.x - nP2.x) + (nP1.y - nP2.y) * (nP1.y - nP2.y));
        if (leng1 < leng2)
        {
            //放大手势
            return true;
        }
        else
        {
            //缩小手势
            return false;
        }
    }

    void OnGUI()
    {
        GUIStyle _GUIStyle = new GUIStyle();

        if (GUI.Button(new Rect(6, 6, 135, 135), btnTextureBlueAndRed, _GUIStyle))
        {
            //人物  红蓝级数基本属性
        }

        if (GUI.Button(new Rect(Screen.width - 100, (Screen.height - 100), 91, 91), btnFuck, _GUIStyle))
        {
            if (GetComponent<Animation>().IsPlaying("MagicShotStraight"))
                return;

            Transform WavesShow = Instantiate(transform.FindChild("WavesShow")) as Transform;

            WavesShow.GetComponent<Animation>().Play();

            WavesShow.parent = transform;

            WavesShow.position = new Vector3(0, 2, 0);

            //Electricity.position = transform.position + new Vector3(0, 1.8f, 0);

            //Electricity.rotation = transform.rotation * Quaternion.Euler(0, 90, 90);

            //Electricity.rotation = Quaternion.Euler(0, 0, 90);

            Destroy(WavesShow.gameObject, 4);

            GetComponent<Animation>().Play("MagicShotStraight");

            bu.Play();
        }

        if (GUI.Button(new Rect(Screen.width - 180, (Screen.height - 60), 61, 61), btnFuck1, _GUIStyle))
        {
            if (GetComponent<Animation>().IsPlaying("SpellCastA"))
                return;

            youLinHuoQiuVoice.Play();

            if (GetComponent<Animation>().isPlaying)
                GetComponent<Animation>().Stop();
            GetComponent<Animation>().Play("SpellCastA");

            Transform Glow = Instantiate(transform.FindChild("Glow")) as Transform;

            Transform Waves = Glow.FindChild("Waves") as Transform;

            Glow.parent = transform;

            Glow.position = new Vector3(0, 2, 0);

            Glow.GetComponent<Animation>().Play();

            Destroy(Glow.gameObject, 4);
        }

        if (GUI.Button(new Rect(Screen.width - 60, (Screen.height - 180), 61, 61), btnFuck2, _GUIStyle))
        {

        }

        if (GUI.Button(new Rect(Screen.width - 140, (Screen.height - 140), 61, 61), btnFuck3, _GUIStyle))
        {

        }


       




    }
}
