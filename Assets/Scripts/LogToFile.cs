using System;
using System.IO;
using UnityEngine;

public class LogToFile : MonoBehaviour
{
    private StreamWriter logFileWriter;
    private string logFilePath = "Log.txt";
    private DateTime startTime;

    void OnEnable()
    {
        startTime = DateTime.Now;
        DateTime dt = DateTime.Now;
        string formattedDate = dt.ToString("u").Replace(":", "-").Replace(" ", "_");
        logFilePath = Path.Combine(Application.persistentDataPath, formattedDate + "_" + logFilePath);
        logFileWriter = new StreamWriter(logFilePath, true);

        Log($"Программа запущена в {startTime}");
        Application.logMessageReceived += LogMessage;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= LogMessage;
        logFileWriter.Close();
    }

    void LogMessage(string message, string stackTrace, LogType type)
    {
        logFileWriter.WriteLine($"[{type}] {message}");
        logFileWriter.WriteLine(stackTrace);
        logFileWriter.Flush();
    }

    void Log(string message)
    {
        logFileWriter.WriteLine(message);
        logFileWriter.Flush();
    }

    void OnApplicationQuit()
    {
        Log("Программа завершена пользователем.");
    }

    void OnApplicationException(Exception ex)
    {
        Log($"Исключение: {ex.Message}. Причина завершения работы.");
    }
}
