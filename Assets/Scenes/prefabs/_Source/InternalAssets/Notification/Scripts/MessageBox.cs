using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace VLRTK.Notification
{
    public class MessageBox
    {
        // fields
        public string header;
        public string body;
        public Notification.Type type;

        public List<ButtonMessageBox> buttons = new List<ButtonMessageBox>();

        // ctor
        public MessageBox(){}
        public MessageBox(string header, string body, Notification.Type type = Notification.Type.None)
        {
            this.header = header;
            this.body = body;
            this.type = type;
        }

        // Methods
        public void Show()
        {
            NotificationManager.instacne.Show(this);
        }

        public void AddButton(ButtonMessageBox buttonMessageBox)
        {
            buttons.Add(buttonMessageBox);
        }
    }

    public class ButtonMessageBox
    {
        // enum
        public enum Type 
        {
            Fill,
            Border
        }

        // fields
        public string buttonText;
        public Type type;
        public UnityEngine.Events.UnityAction onClicked;

        // ctor
        public ButtonMessageBox(string buttonText, UnityEngine.Events.UnityAction onClicked, Type type)
        {
            this.onClicked = onClicked;
            this.buttonText = buttonText;
            this.type = type;
        }
    }

}
