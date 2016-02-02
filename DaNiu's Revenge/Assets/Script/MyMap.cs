using UnityEngine;
using System.Collections;

public class MyMap : MonoBehaviour
{
    GUITexture mapGUITexture;

    public Texture me;

    public Texture you;

    void Start()
    {
        mapGUITexture = GetComponent<GUITexture>();

        mapGUITexture.pixelInset = new Rect((Screen.width - 200), (Screen.height - 100), 200, 100);
    }


    int i = 1;
    void Update()
    {
        i++;
        if (1 == 1000)
            i = 0;

        if (i % 20 == 0)
        { 
            
        }
    }

    void OnGUI()
    {
        GUIStyle _GUIStyle = new GUIStyle();

        GUI.Button(new Rect((Screen.width - 200), 100, 200, 100), "");

        GameObject[] GameObjects = GameObject.FindGameObjectsWithTag("Fox");

        foreach (var item in GameObjects)
        {
            Transform fox = item.transform;

            float x = 20 + (fox.position.x - 103) * 1.6f;

            float y = 50 - (fox.position.z - 107);

            GUI.Button(new Rect((Screen.width - 200) + x, 1 + y, 5, 5), you, _GUIStyle);
        }

        Transform player = GameObject.FindWithTag("Player").transform;

        float x1 = 20 + (player.position.x - 103) * 1.6f;

        float y1 = 50 - (player.position.z - 107);

        GUI.Button(new Rect((Screen.width - 200) + x1, 1 + y1, 5, 5), me, _GUIStyle);

    }
}
