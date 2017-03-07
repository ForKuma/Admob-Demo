using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class AppLogView : MonoBehaviour {
	public Text ConsoleLogText;
	public Button IapPreLogButton;
	public Button IapNextLogButton;

	int logId = 0;
	void Awake() {
		Application.logMessageReceived += (string condition, string stackTrace, LogType type) => {
			Log(string.Format("{0:D3}",(++logId)) + ":" +   condition);
		};
	}

	int messageIdx = 0;
	List<string> Messages;
	void Log(string str) {
		if(Messages == null) {
			Messages = new List<string> ();
		}

		Messages.Add (str);
		messageIdx = Messages.Count - 1;
		ConsoleLogText.text = str;
		IapPreLogButton.interactable = true;

		while(--messageIdx >= 0) {
			string temp = ConsoleLogText.text;
			ConsoleLogText.text = Messages [messageIdx] + "\n" + ConsoleLogText.text;
			if(ConsoleLogText.preferredHeight > ConsoleLogText.transform.parent.GetComponent<RectTransform>().rect.size.y-10) {
				ConsoleLogText.text = temp;
				messageIdx++;
				break;
			}
		}
	}

	public void OnIapPreLogButtonClick() {
		if(--messageIdx <= 0) {
			messageIdx = 0;
			IapPreLogButton.interactable = false;
		}
		IapNextLogButton.interactable = true;

		ConsoleLogText.text = "";
		for(int i=messageIdx; i<Messages.Count; i++) {
			string temp = ConsoleLogText.text;
			ConsoleLogText.text = ConsoleLogText.text + Messages [i];
			if(ConsoleLogText.preferredHeight > ConsoleLogText.transform.parent.GetComponent<RectTransform>().rect.size.y-10) {
				ConsoleLogText.text = temp;
				break;
			}

			if(i<Messages.Count) {
				ConsoleLogText.text = ConsoleLogText.text + "\n";
			}
		}
	}

	public void OnIapNextLogButtonClick() {
		if(++messageIdx >= Messages.Count-1) {
			messageIdx = Messages.Count - 1;
			IapNextLogButton.interactable = false;
		}
		IapPreLogButton.interactable = true;

		ConsoleLogText.text = "";
		for(int i=messageIdx; i<Messages.Count; i++) {
			string temp = ConsoleLogText.text;
			ConsoleLogText.text = ConsoleLogText.text + Messages [i];
			if(ConsoleLogText.preferredHeight > ConsoleLogText.transform.parent.GetComponent<RectTransform>().rect.size.y-10) {
				ConsoleLogText.text = temp;
				break;
			}
			if(i<Messages.Count) {
				ConsoleLogText.text = ConsoleLogText.text + "\n";
			}
		}
	}
}
