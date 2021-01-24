using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class NotificationUpdate : MonoBehaviour
{
    [Tooltip("Max number of notifications to store before deleting old ones")]
    public int maxNotifications = 100;

    private TextMeshPro textMesh = null;
    private List<string> notes = null;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = gameObject.GetComponent<TextMeshPro>();
        textMesh.text = "";

        notes = new List<string>() { "" };
        
        AddNote("Notification Panel starting", Color.yellow);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNote(string note, Color color)
    {
        //timestamp
        string timestamp = "<color=yellow>" + DateTime.Now.ToString() + ": </color>";

        string startColor = "<color=#" + ColorUtility.ToHtmlStringRGB(color) + ">";
        string endColor = "</color>";


        notes.Add(timestamp + startColor + note + endColor);

        if (notes.Count > maxNotifications)
        {
            notes.RemoveAt(0);
        }

        textMesh.text = string.Join(Environment.NewLine, notes);
    }

 
}
