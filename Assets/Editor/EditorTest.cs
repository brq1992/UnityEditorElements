using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor;
using UnityEngine;

public class EditorTest : EditorWindow
{
    enum EnumTest
    {
        net,
        insect,
        tes,
        est
    }

    static EditorTest window;

    [MenuItem("Tools/Test Eidtor Function")]
    static void Test()
    {
        window = (EditorTest) EditorWindow.GetWindow(typeof (EditorTest), false);
        window.Show();
    }

#region Properties

    int testInt = 0;
    float testFloat = 0;
    float floatSlider = 0;
    float maxValue = 20;
    float minValue = -50;
    string testStr = "Jeff";
    bool testBool = true;
    int toolbarOption = 0;
    string[] toolbarStr = new[] {"this", "is", "Toolbar"};
    EnumTest enumTest;
    EnumTest enumTest2;
    int enumInt = 0;
    int selectdSize = 1;
    string[] names = {"1", "2", "3", "4"};
    int[] sizes = {1, 2, 4};
    string tagStr = "";
    int layerInt = 0;
    int maskInt = 0;
    Vector3 testVector3;
    Color testColor;
    Rect testRect;
    GameObject gameobject;
    Texture texture;
    private bool isshowScrollView = false;
    private Vector2 scrollPosition;
    #endregion


    private void OnGUI()
    {
        this.Repaint();
        if (secondWindow || secondWindow2)
        {
            GUI.enabled = false;
        }

        GUILayout.Label(testStr);

        GUIStyle fontStyle = new GUIStyle();
        fontStyle.normal.background = null;
        fontStyle.normal.textColor = new Color(1, 0, 0);
        fontStyle.fontStyle = FontStyle.BoldAndItalic;
        fontStyle.fontSize = 18;

        GUILayout.Label(testStr, fontStyle);

        GUILayout.TextField(testStr);

        EditorGUILayout.LabelField("name: ", testStr);

        testStr = EditorGUILayout.TextField("name: ", testStr);

        testInt = EditorGUILayout.IntField("IntField: ", testInt);

        testFloat = EditorGUILayout.FloatField("FloatField: ", testFloat);

        testStr = EditorGUILayout.TextArea(testStr, GUILayout.Height(40));

        EditorGUILayout.SelectableLabel(testStr);

        testStr = GUILayout.PasswordField(testStr, "*"[0]);

        testStr = EditorGUILayout.PasswordField("Password: ", testStr);

        floatSlider = EditorGUILayout.Slider(floatSlider, 1, 100);

        EditorGUILayout.MinMaxSlider(ref minValue, ref maxValue, -100, 100);

        testBool = GUILayout.Toggle(testBool, "on");

        testBool = EditorGUILayout.Toggle("on: ", testBool);

        toolbarOption = GUILayout.Toolbar(toolbarOption, toolbarStr);
        switch (toolbarOption)
        {
            case 0:
                GUILayout.Label("111");
                break;
            case 1:
                GUILayout.Label("22222222");
                break;
            case 2:
                GUILayout.Label("3333");
                break;
        }

        EditorGUILayout.Space();

        enumTest = (EnumTest) EditorGUILayout.EnumPopup("Popup Type: ", enumTest);

        enumTest2 = (EnumTest)EditorGUILayout.EnumMaskField("Mult Popup Type: ", enumTest2);

        enumInt = EditorGUILayout.Popup("string popup type: ", enumInt, names);

        selectdSize = EditorGUILayout.IntPopup("Int popup type: ", selectdSize, names, sizes);

        tagStr = EditorGUILayout.TagField("choose tag: ", tagStr);

        layerInt = EditorGUILayout.LayerField("choose layer: ", layerInt);

        maskInt = EditorGUILayout.MaskField("multy arry: ", maskInt, names);

        testColor = EditorGUILayout.ColorField("color ", testColor);

        GUI.backgroundColor = Color.magenta;
        testVector3 = EditorGUILayout.Vector3Field("position ", testVector3);

        GUI.backgroundColor = Color.green;
        testRect = EditorGUILayout.RectField("Rect Size: ", testRect);
        GUI.backgroundColor = Color.gray*1.8f;

        gameobject = (GameObject)EditorGUILayout.ObjectField("any type ex. GameObject", gameobject, typeof (GameObject));

        texture = EditorGUILayout.ObjectField("Any Type ex. Texture", texture, typeof (Texture), true) as Texture;

        GUILayout.BeginHorizontal();
        GUILayout.Label("Horizontal auto asign show: ");
        testStr = GUILayout.PasswordField(testStr,"*"[0]);
        testBool = GUILayout.Toggle(testBool, "on");
        GUILayout.Button("button");
        GUILayout.EndHorizontal();

        if (GUILayout.Button("click button pup up message tips!"))
        {
            ShowNotification(new GUIContent("this is a message."));
        }

        if (GUILayout.Button("Click button show scroll view!"))
        {
            isshowScrollView = !isshowScrollView;
        }

        if (isshowScrollView)
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            for (int i = 0; i < 100; i++)
            {
                GUILayout.Label(i.ToString());
            }
            GUILayout.EndScrollView();
        }
        if (GUILayout.Button("Click button show erji window"))
        {
            secondWindow = !secondWindow;
        }
        if (secondWindow)
        {
            GUI.enabled = true;
            BeginWindows();
            secondWindowRect = GUILayout.Window(1, secondWindowRect, SecondWindow, "erji window");
            EndWindows();
        }

        if (GUILayout.Button("click button show graphic"))
        {
            secondWindow2 = !secondWindow2;
        }
        if (secondWindow2)
        {
            GUI.backgroundColor = Color.red/2;
            GUI.enabled = true;
            BeginWindows();
            secondWindowRect2 = GUILayout.Window(2, secondWindowRect2, SecondWindow2, "show graphic");
            EndWindows();
        }

        GUI.backgroundColor = Color.gray*1.8f;
        GUI.enabled = false;
        GUILayout.Button("apply gray button");
        GUI.enabled = true;
    }

    Rect secondWindowRect = new Rect(0, 0, 400, 400);
    bool secondWindow = false;

    void SecondWindow(int unusedWindowID)
    {
        Application.targetFrameRate = EditorGUILayout.IntSlider("limit fps ", Application.targetFrameRate, 10, 300);
        Application.runInBackground = EditorGUILayout.Toggle("runtime in background", Application.runInBackground);
        gameobject = (GameObject)EditorGUILayout.ObjectField("selection gameobejct", Selection.activeGameObject,
            typeof (GameObject));
        EditorGUILayout.Vector3Field("the position of the mouse in the scene's view ", mousePosition);
        EditorGUILayout.Vector3Field("the position of the mouse in the second window' view", Event.current.mousePosition);
        hitGo = (GameObject) EditorGUILayout.ObjectField("the ray ", hitGo != null ? hitGo : null, typeof (GameObject));

        GUILayout.Label("UsedTextureCount " + UnityStats.usedTextureCount);
        GUILayout.Label("UsedTextureMemorySize " + UnityStats.usedTextureMemorySize/1000000f + "Mb");
        GUILayout.Label("RenderTextureCount " + UnityStats.renderTextureCount);

        if (GUILayout.Button("close erji window"))
        {
            secondWindow = false;
        }
        GUI.DragWindow();
    }

    Rect secondWindowRect2 = new Rect(0, 0, 400, 400);
    bool secondWindow2 = false;
    int capSize = -50;
    Vector3 capEular = new Vector3(200, 200, 200);

    void SecondWindow2(int unusedWindowID)
    {
        capSize = EditorGUILayout.IntField("Size ", capSize);
        capEular = EditorGUILayout.Vector3Field("testVector3 ", capEular);

        if (GUILayout.Button("close draw graphic"))
        {
            secondWindow2 = false;
        }

        Handles.color = Color.red;
        Handles.DrawLine(new Vector2(75, 100), new Vector3(150, 200));
        Handles.CircleCap(1, new Vector2(300,150), Quaternion.identity, capSize);
        Handles.color = Color.green;
        Handles.SphereHandleCap(2, new Vector2(100, 250), Quaternion.Euler(capEular), capSize,EventType.Ignore);
        Handles.CubeHandleCap(3,new Vector2(300,250),Quaternion.Euler(capEular),capSize,EventType.Ignore);

        Handles.color = Color.blue;
        Handles.CylinderHandleCap(4, new Vector2(100, 350), Quaternion.Euler(capEular), capSize, EventType.Ignore);
        Handles.ConeHandleCap(5, new Vector2(300, 350), Quaternion.Euler(capEular), capSize, EventType.Ignore);

        GUI.DragWindow();
    }

    private void OnEable()
    {
        SceneView.onSceneGUIDelegate += SceneGUI;
    }

    private void OnDisable()
    {
        SceneView.onSceneGUIDelegate -= SceneGUI;
    }

    GameObject hitGo;
    Vector3 mousePosition;

    void SceneGUI(SceneView sceneView)
    {
        if (Event.current.type == EventType.MouseMove)
        {
            mousePosition = Event.current.mousePosition;
            RaycastHit hit;
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                hitGo = hit.collider.gameObject;
            }
            else
            {
                hitGo = null;
            }
        }
    }
}
