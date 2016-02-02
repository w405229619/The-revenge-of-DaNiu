using UnityEngine;
using System.Collections;

public class GUIControl : MonoBehaviour {
	public GameObject background;
	public GameObject draggableAbout;
	public GameObject draggableHelp;
	public GameObject draggableMenu;
	public GameObject draggableSetting;
	public GameObject draggableTransition;
	
	public UILabel info;
	
	private string transInfo;
	
	public int pageNo;
	
	// Volume Control
	public UISlider soundVolume;
	public UISlider musicVolume;
	
	public gameScript game;

	// Use this for initialization
	void Start () {
	}
	
	public void GameStart() {
		pageNo = 1;
		gameObject.SetActive(false);
		Camera.main.depth = 10;
		Camera.main.GetComponent<gameScript>().gameStart();
	}
	
	public void GameQuit() {
		Application.Quit();
	}
	
	public void Transition() {
		pageNo = 1;
		int level = Camera.main.GetComponent<gameScript>().level;
		draggableMenu.SetActive(false);
		draggableTransition.SetActive(true);
		NextPage();
	}
	
	public void LoadLevel() {
		gameObject.SetActive(false);
		Camera.main.depth = 10;
		int level = Camera.main.GetComponent<gameScript>().level;
		Camera.main.GetComponent<gameScript>().LoadLevel();
	}
	
	public void NextPage() {
		int level = Camera.main.GetComponent<gameScript>().level;
		switch (level) {
		case 1:
			switch (pageNo) {
			case 1:
				transInfo = "DaNiu has passed the laser obstacle";
				break;
			case 2:
				transInfo = "DaNiu's health increased";
				break;
			case 3:
				LoadLevel();
				break;
			}
			break;
		case 2:
			switch (pageNo) {
			case 1:
				transInfo = "DaNiu has beated the guard and got the holy gun";
				break;
			case 2:
				LoadLevel();
				break;
			}
			break;
		case 3:
			switch (pageNo) {
			case 1:
				transInfo = "DaNiu has passed the fire maze and found three pills";
				break;
			case 2:
				transInfo = "DaNiu arrives enemy base";
				break;
			case 3:
				LoadLevel();
				break;
			}
			break;
		case 4:
			switch (pageNo) {
			case 1:
				transInfo = "DaNiu killed the first boss ";
				break;
			case 2:
				transInfo = "DaNiu's guns level up";
				break;
			case 3:
				LoadLevel();
				break;
			}
			break;
		case 5:
			switch (pageNo) {
			case 1:
				transInfo = "DaNiu killed the second boss ";
				break;
			case 2:
				transInfo = "DaNiu's guns level up";
				break;
			case 3:
				transInfo = "Da Niu sees the final boss actually has the same face as him, surprised and says: Are you my litter brother";
				//"大牛看到最终的boss竟然有着跟他一样的脸，诧异道：你是我的弟弟吗？"
				break;
			case 4:
				transInfo = "Boss: Go to hell!Let's Fight!";
				//"Boss：放屁，要打就来吧？"
				break;
				break;
			case 5:
				LoadLevel();
				break;
			}
			break;
		case 6:
			switch (pageNo) {
			case 1:
				transInfo = "DaNiu blocked lazer with his body";
				break;
			case 2:
				transInfo = "Boss:Why?Why do you do this?";
				//“Boss:为什么！你为什么要这么做”;
				break;
			case 3:
				transInfo = "Because you are my long lost twin litter brother Xiao Niu!";
				//"因为你是我从小就失散的双胞胎弟弟小牛啊"
				break;
			case 4:
				transInfo = "Xiao Niu:No!It is impossible!You lie to me!";
				//"小牛：不，不会的，你骗我！"
				break;
			case 5:
				transInfo = "Da Niu takes off the pendant on his neck, pieces together with Xiao Niu's, and says: it is said by mother " +
					"that shortly after we are born, once there happens a riot and you are captured away by the enemy and we are not met again.";
				//"大牛摘下自己脖子上的玉坠和小牛脖子上的玉坠拼在一起：听妈妈说我们出生不久发生一次暴乱，你被敌人抓走后就再也没找到了"				
				break;
			case 6:
				transInfo = "Da Niu : Can you call me brother?";
				//"大牛：你能叫我一声哥哥吗？"
				break;
			case 7:
				transInfo = "Xiao Niu : No!No!Brother, I don't allow you to die!";
				//"小牛：呜~~~~~~~~~~~哥哥，你不能死"				
				break;
			case 8:
				transInfo = "Da Niu closes his eyes happily.";//"大牛幸福的闭上了眼睛"
				break;
			case 9:
				transInfo = "Congratulations! You have passed all the tests and succeeded! I hope you enjoy it! Now press 'OK' to quit the game.";
				break;
			case 10:
				Application.Quit();
				break;
			}
			break;
		}
		pageNo++;
		info.text = transInfo;
		info.GetComponent<TypewriterEffect>().reset();
	}
	
	public void OnSoundSliderChange() {
		game.volSound = soundVolume.sliderValue;
	}
	
	public void OnMusicSliderChange() {
		game.volMusic = musicVolume.sliderValue;
		game.adjustVolMusic();
	}
}
