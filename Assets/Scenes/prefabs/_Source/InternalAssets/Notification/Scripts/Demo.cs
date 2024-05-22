using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VLRTK.Notification;

public class Demo : MonoBehaviour
{
    MessageBox mb;

    void Start()
    {
        mb = new MessageBox("Пример названия", "Пример вопроса", Notification.Type.Question);
        mb.AddButton(new ButtonMessageBox("Нет", CallNo, ButtonMessageBox.Type.Border));
        mb.AddButton(new ButtonMessageBox("Да", CallYes, ButtonMessageBox.Type.Fill));
        mb.Show();
    }

    void CallNo()
    {
        print("Click No");
    }

    void CallYes()
    {
        print("Click Yes");
    }

}
