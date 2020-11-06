using UnityEngine;
using UnityEngine.UI;

public class TextChanger : MonoBehaviour
{

    public Text text;
    public string string1;
    public string string2;
    public string string3;
    public string string4;
    public string string5;

    public void StringChanger(int i)
    {
        if (i == 0)
        {
            text.text = string1;
        }
        if (i == 1)
        {
            text.text = string2;
        }
        if (i == 2)
        {
            text.text = string3;
        }
        if (i == 3)
        {
            text.text = string4;
        }
        if (i == 4)
        {
            text.text = string5;
        }
    }
}
