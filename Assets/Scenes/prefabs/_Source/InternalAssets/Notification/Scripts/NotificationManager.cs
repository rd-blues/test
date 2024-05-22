using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace VLRTK.Notification
{
    public class NotificationManager : MonoBehaviour
    {
        public static NotificationManager instacne;

        private void Awake()
        {
            instacne = this;
        }

        public void Show(MessageBox messageBox)
        {
            // load prefab
            GameObject mbox = Resources.Load("MessageBox") as GameObject;
            Sprite icon = Resources.Load<Sprite>("Icons/" + messageBox.type.ToString()) as Sprite;

            // Create on scenes
            GameObject box = Instantiate(mbox);

            // Set settings
            box.transform.GetChild(0).Find("MessageBox").transform.Find("textHeader").GetComponentInChildren<Text>().text = messageBox.header;
            box.transform.GetChild(0).Find("MessageBox").transform.Find("textBody").GetComponentInChildren<Text>().text = messageBox.body;

            if (messageBox.type != Notification.Type.None)
                box.transform.GetChild(0).Find("MessageBox").transform.Find("Icon").GetComponent<Image>().sprite = icon;
            else
                box.transform.GetChild(0).Find("MessageBox").transform.Find("Icon").gameObject.SetActive(false);

            // Add buttons
            for (int i = 0; i < messageBox.buttons.Count; i++)
            {
                // Load prefab
                GameObject buttonBox = Resources.Load("Buttons/" + messageBox.buttons[i].type.ToString()) as GameObject;

                // Create on scenes
                Button btn = Instantiate(buttonBox, box.transform.GetChild(0).GetChild(0).GetChild(2).transform).GetComponent<Button>();

                if (messageBox.buttons[i].onClicked != null)
                    btn.onClick.AddListener(messageBox.buttons[i].onClicked);

                btn.onClick.AddListener(DestroyMbox);
                btn.GetComponentInChildren<Text>().text = messageBox.buttons[i].buttonText;
            }
           
            // Destroy after click
            void DestroyMbox()
            {
                Destroy(box);
            }
        }
    }
}
