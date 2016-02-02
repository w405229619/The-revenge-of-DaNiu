using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour
{


    void Start()
    {

    }



    int i = -30;

    void Update()
    {
        i++;
        if (i < 1)
            return;
        transform.localScale = new Vector3(1, 1 - i * 0.01f, 0);

        if (1 - i * 0.01f < 0)
        {
            GameObject.DestroyImmediate(gameObject);
        }

    }
}
