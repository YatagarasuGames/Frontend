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
