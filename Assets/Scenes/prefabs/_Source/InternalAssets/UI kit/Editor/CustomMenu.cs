using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CustomMenu : MonoBehaviour
{
    [MenuItem("GameObject/UI/VLRTK/Button/Fill", false, 0)]
    static void btnFill()
    {
        AddObjectOnScene(Application.dataPath + "/_Source/InternalAssets/UI kit/Prefabs/btnFill.prefab", "Button");
    }

    [MenuItem("GameObject/UI/VLRTK/Button/Icon", false, 0)]
    static void btnIcon()
    {
        AddObjectOnScene(Application.dataPath + "/_Source/InternalAssets/UI kit/Prefabs/btnIcon.prefab", "Button");
    }

    [MenuItem("GameObject/UI/VLRTK/Button/Border", false, 0)]
    static void btnBorder()
    {
        AddObjectOnScene(Application.dataPath + "/_Source/InternalAssets/UI kit/Prefabs/btnBorder.prefab", "Button");
    }

    [MenuItem("GameObject/UI/VLRTK/Slider", false, 0)]
    static void Slider()
    {
        AddObjectOnScene(Application.dataPath + "/_Source/InternalAssets/UI kit/Prefabs/Slider.prefab", "Slider");
    }

    [MenuItem("GameObject/UI/VLRTK/Dropdown", false, 0)]
    static void Dropdown()
    {
        AddObjectOnScene(Application.dataPath + "/_Source/InternalAssets/UI kit/Prefabs/Dropdown.prefab", "Dropdown");
    }

    [MenuItem("GameObject/UI/VLRTK/Toggle/Toggle", false, 0)]
    static void Toggle()
    {
        AddObjectOnScene(Application.dataPath + "/_Source/InternalAssets/UI kit/Prefabs/Toggle.prefab", "Toggle");
    }
    

    [MenuItem("GameObject/UI/VLRTK/Toggle/RadioButton", false, 0)]
    static void RadioButton()
    {
        AddObjectOnScene(Application.dataPath + "/_Source/InternalAssets/UI kit/Prefabs/RadioButton.prefab", "RadioButton");
    }

    [MenuItem("GameObject/UI/VLRTK/InputField", false, 0)]
    static void InputField()
    {
        AddObjectOnScene(Application.dataPath + "/_Source/InternalAssets/UI kit/Prefabs/InputField.prefab", "InputField");
    }

    [MenuItem("GameObject/UI/VLRTK/Scrollbar", false, 0)]
    static void Scrollbar()
    {
        AddObjectOnScene(Application.dataPath + "/_Source/InternalAssets/UI kit/Prefabs/InputField.prefab", "Scrollbar");
    }

    [MenuItem("GameObject/UI/VLRTK/Panel", false, 0)]
    static void Panel()
    {
        AddObjectOnScene(Application.dataPath + "/_Source/InternalAssets/UI kit/Prefabs/Background.prefab", "Background");
    }
    
        
    static void AddObjectOnScene(string path, string nameObj)
    {
        // Load from prefab
        GameObject prefab = PrefabUtility.LoadPrefabContents(path);

        // Create on scenes
        GameObject obj = Instantiate(prefab, Selection.activeGameObject.transform);

        // Set some settings
        obj.transform.localPosition = Vector3.zero;
        obj.name = nameObj;

        // Select created Objcet
        Selection.activeGameObject = obj;
    }
}
