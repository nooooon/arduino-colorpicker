using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;

public class SerialHandler : MonoBehaviour
{
    public delegate void SerialDataReceivedEventHandler(string message);
    public event SerialDataReceivedEventHandler OnDataReceived;

    public string portName = "/dev/tty.usbmodem14311";
    public int baudRate = 9600;

    private SerialPort _serialPort;
    private Thread _thread;
    private bool _isConnected = false;
    private string _message;
    private bool _isMessageReceived = false;

    void Start()
    {
        Connect();
    }

    void Update()
    {
        if(_isMessageReceived)
        {
            OnDataReceived(_message);
        }
        _isMessageReceived = false;
    }

    void OnDestroy()
    {
        _isConnected = false;


        Debug.Log("OnDestroy");

        if(_thread != null && _thread.IsAlive)
        {
            Debug.Log("Join <");
            _thread.Join();
            Debug.Log("> Join");
        }

        if(_serialPort != null & _serialPort.IsOpen)
        {

            Debug.Log("Close <");
            _serialPort.Close();
            Debug.Log("> Close");
            _serialPort.Dispose();
            Debug.Log("> Dispose");
        }
    }

    private void Connect()
    {
        try {
            _serialPort = new SerialPort(portName, baudRate);
            _serialPort.Open();
            _isConnected = true;
        }
        catch {
            _isConnected = false;
            Debug.LogError("Failed to connect to serial port " + portName);
        }

        if(_isConnected) {
            _thread = new Thread(Read);
            _thread.Start();
        }
    }

    private void Read()
    {
        while(_serialPort != null && _serialPort.IsOpen && _isConnected) {
            try {
                _message = _serialPort.ReadLine();
                _isMessageReceived = true;
                //Debug.Log(_message);
            } catch (System.Exception e) {
                Debug.LogWarning(e.Message);
            }
        }
    }

    public void Write(string message)
    {
        try {
            _serialPort.Write(message);
        } catch (System.Exception e) {
            Debug.LogWarning(e.Message);
        }
    }


}
