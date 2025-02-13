using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Download108 : MonoBehaviour
{
    private Dictionary<int, string[][]> exp = new Dictionary<int, string[][]>();

    public Download108()
    {
        for (int i = 1; i < 6; i++)
        {
            exp[i] = new string[5][];
        }
    }

    public void add(int key, double len, double result)
    {
        string[][] info;
        if (exp.ContainsKey(key))
        {
            info = exp[key];
        }
        else
        {
            info = new string[5][];
        }
        info[(5 - ((int)(len * 100) / 5))] = new string[] { len.ToString("0.00"), result.ToString("0.0000") };
        exp[key] = info;
    }

    public string[][] get(int key) { return exp[key]; }

    public void DownloadFile()
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (int k in exp.Keys)
        {
            stringBuilder.Append("m = 0." + k + " кг");
            stringBuilder.Append("\n");
            foreach (string[] s in exp[k])
            {
                if (s == null || s.Length != 2)
                {
                    stringBuilder.Append("Нет измерений!");
                }
                else
                {
                    stringBuilder.Append("l = " + s[0] + "\t" + "T = " + s[1] + "\n");
                }
                stringBuilder.Append("\n");
            }
        }
        WebGLFileSaver.SaveFile(stringBuilder.ToString(), "Results");
    }
}
