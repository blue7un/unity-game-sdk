
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

public static class PenaltyEditorTools
{

    static public GUIStyle CreateLabelInfoGUI()
    {
        GUIStyle myFoldoutStyle = new GUIStyle(EditorStyles.whiteLabel);
        Color myStyleColor = new Color(190f/255,240f/255,143f/255);
        myFoldoutStyle.fontStyle = FontStyle.Normal;
        myFoldoutStyle.normal.textColor = myStyleColor;
        myFoldoutStyle.onNormal.textColor = Color.white;
        myFoldoutStyle.hover.textColor = Color.white;
        myFoldoutStyle.onHover.textColor = Color.white;
        myFoldoutStyle.focused.textColor = Color.white;
        myFoldoutStyle.onFocused.textColor = Color.white;
        myFoldoutStyle.active.textColor = Color.white;
        myFoldoutStyle.onActive.textColor = Color.white;

        return myFoldoutStyle;
    }
    static public GUIStyle StyleButtonRemove()
    {
        GUIStyle myFoldoutStyle = new GUIStyle();
        Color myStyleColor = new Color(190f / 255, 240f / 255, 143f / 255);
        myFoldoutStyle.fontStyle = FontStyle.Normal;
        myFoldoutStyle.normal.textColor = myStyleColor;
        myFoldoutStyle.onNormal.textColor = Color.white;
        myFoldoutStyle.hover.textColor = Color.white;
        myFoldoutStyle.onHover.textColor = Color.white;
        myFoldoutStyle.focused.textColor = Color.white;
        myFoldoutStyle.onFocused.textColor = Color.white;
        myFoldoutStyle.active.textColor = Color.white;
        myFoldoutStyle.onActive.textColor = Color.white;

        return myFoldoutStyle;
    }

    /// <summary>
    /// Draw a distinctly different looking header label
    /// </summary>
    /// 
    static public void AlignLeft(float pixel)
    {
        EditorGUILayout.BeginVertical();
        GUILayout.Space(pixel);
        EditorGUILayout.EndVertical();
    }
    static public bool DrawMinimalisticHeader(string text) { return DrawHeader(text, text, false, true); }

    /// <summary>
    /// Draw a distinctly different looking header label
    /// </summary>

    static public bool DrawHeader(string text) { return DrawHeader(text, text, false,false); }

    /// <summary>
    /// Draw a distinctly different looking header label
    /// </summary>

    static public bool DrawHeader(string text, string key) { return DrawHeader(text, key, false, false); }

    /// <summary>
    /// Draw a distinctly different looking header label
    /// </summary>

    static public bool DrawHeader(string text, bool detailed) { return DrawHeader(text, text, detailed, !detailed); }

    /// <summary>
    /// Draw a distinctly different looking header label
    /// </summary>

    static public bool DrawHeader(string text, string key, bool forceOn, bool minimalistic)
    {
        bool state = EditorPrefs.GetBool(key, true);

        if (!minimalistic) GUILayout.Space(3f);
        if (!forceOn && !state) GUI.backgroundColor = new Color(0.8f, 0.8f, 0.8f);
        GUILayout.BeginHorizontal();
        GUI.changed = false;

        if (minimalistic)
        {
            if (state) text = "\u25BC" + (char)0x200a + text;
            else text = "\u25BA" + (char)0x200a + text;

            GUILayout.BeginHorizontal();
            GUI.contentColor = EditorGUIUtility.isProSkin ? new Color(1f, 1f, 1f, 0.7f) : new Color(0f, 0f, 0f, 0.7f);
            if (!GUILayout.Toggle(true, text, "PreToolbar2", GUILayout.MinWidth(100f), GUILayout.Height(20))) state = !state;
            GUI.contentColor = Color.white;
            
            GUILayout.EndHorizontal();
        }
        else
        {
            text = "<b><size=11>" + text + "</size></b>";
            if (state) text = "\u25BC " + text;
            else text = "\u25BA " + text;
            if (!GUILayout.Toggle(true, text, "dragtab", GUILayout.MinWidth(100f),GUILayout.Height(20)))state = !state;

        }

        if (GUI.changed) EditorPrefs.SetBool(key, state);

        if (!minimalistic) GUILayout.Space(2f);
        GUILayout.EndHorizontal();
        GUI.backgroundColor = Color.white;
        if (!forceOn && !state) GUILayout.Space(3f);
        return state;
    }


    static public bool DrawHeaderWithRemoveItem(string text)
    {
        bool state = EditorPrefs.GetBool(text, true);

        GUILayout.BeginHorizontal();
        GUI.changed = false;

        text = "<b><size=11>" + text + "</size></b>";
        if (state) text = "\u25BC " + text;
        else text = "\u25BA " + text;
        if (!GUILayout.Toggle(true, text, "dragtab", GUILayout.MinWidth(20f), GUILayout.Height(20))) state = !state;
        
        GUILayout.BeginVertical();
        GUILayout.Space(-1);
        GUILayout.Button("-", GUILayout.MaxWidth(30));
        GUILayout.EndVertical();

        if (GUI.changed) EditorPrefs.SetBool(text, state);

        GUILayout.EndHorizontal();
        GUI.backgroundColor = Color.white;
        return state;
    }



}

