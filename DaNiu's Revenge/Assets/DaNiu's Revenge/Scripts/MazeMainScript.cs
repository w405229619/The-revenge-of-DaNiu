using UnityEngine;
using System.Collections;

public class MazeMainScript : MonoBehaviour {
	public int objNowi;
	public int objNowj;
	public bool publicNow = false;
	public MazeScript[] mazeScripts;
	public int randomNumber=1;
	public int[,] mazeMap = {{8,8,9,10,8,13,7,14,13,14,12,8,4,12,12,13,6,9,6,12,5,2,4,1,6}
							,{9,10,9,10,8,12,5,6,13,14,12,8,9,6,12,12,12,12,8,12,5,6,4,5,6}						
							,{8,9,2,9,2,13,15,3,7,2,12,13,3,11,2,4,12,9,15,2,1,6,4,5,2}
							,{8,9,3,2,8,13,15,10,9,6,4,12,5,7,10,9,15,3,2,12,4,4,1,3,6}
							,{1,11,3,3,10,1,14,9,3,14,1,14,5,10,12,9,14,9,6,12,4,4,4,1,6}};

	// Use this for initialization
	void Start () {
		mazeScripts = GetComponentsInChildren< MazeScript >();
		randomNumber = Random.Range(0,5);
		foreach (MazeScript ms in mazeScripts) {
			if (ms.i == 0 && ms.j == 0)
				ms.emit = false;
			else
				ms.emit = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		/*bool inMaze = false;
		foreach (MazeScript ms in mazeScripts) {
			if (ms.playerIsHere)
			{
				inMaze = true;
				break;
			}
		}
		if (!inMaze)
		{
			foreach (MazeScript ms in mazeScripts) {
				if (ms.i == 0 && ms.j == 0)
					ms.emit = false;
				else
					ms.emit = true;
			}
			randomNumber = Random.Range(0,5);
		}*/
		if(publicNow)
		{			
			int mazeMapPos = objNowi*5+objNowj;
			int mazeMapValue = mazeMap[randomNumber,mazeMapPos];
			bool up = (mazeMapValue&8)==0?false:true;
			bool down = (mazeMapValue&4)==0?false:true;
			bool left = (mazeMapValue&2)==0?false:true;
			bool right = (mazeMapValue&1)==0?false:true;
			publicNow = false;
			foreach (MazeScript ms in mazeScripts) {
				if (ms.i == objNowi+1 && ms.j == objNowj)
				{
					if (up)
						ms.emit = false;
					else
						ms.emit = true;
				}
				else if (ms.i == objNowi-1 && ms.j == objNowj)
				{
					if (down)
						ms.emit = false;
					else
						ms.emit = true;
				}
				else if (ms.i == objNowi && ms.j == objNowj+1)
				{
					if (right)
						ms.emit = false;
					else
						ms.emit = true;		
				}
				else if (ms.i == objNowi && ms.j == objNowj-1)
				{
					if (left)
						ms.emit = false;
					else
						ms.emit = true;	
				}
				else if (ms.i == objNowi && ms.j == objNowj)
				{
					ms.emit = false;
				}
				else ms.RandomChange = true;
			}
		}
	}
}
