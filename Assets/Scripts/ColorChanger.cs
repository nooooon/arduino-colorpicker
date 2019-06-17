using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{

    public SerialHandler serialHandler;
    public GameObject cube;

    private Material _cubeMat;

    void Start()
    {
        _cubeMat = cube.GetComponent<Renderer>().material;
        serialHandler.OnDataReceived += OnDataReceived;
    }

    void OnDataReceived(string message) {
        var data = message.Split(new string[] { "," }, System.StringSplitOptions.None);
        if(data.Length < 3) return;

        try {
            Debug.Log(message);

            var r = System.Convert.ToByte(data[0]);
            var g = System.Convert.ToByte(data[1]);
            var b = System.Convert.ToByte(data[2]);

            _cubeMat.color = new Color32(r, g, b, 1);

        } catch(System.Exception e) {
            Debug.LogWarning(e.Message);
        }
    }

}
