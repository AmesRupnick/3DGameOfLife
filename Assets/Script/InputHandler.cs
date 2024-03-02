using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    public TMP_InputField Size; // References to the TMP Input Field
    public TMP_InputField speed;
    public TMP_InputField design;

    public int GetSize() //same as getSizeX above
    {
        string strsize = Size.text;
        if (int.TryParse(strsize, out int result))
        {
            int size = System.Int32.Parse(strsize);
            if (size < 8)
            {
                return 8;
            }
            else if (size > 15)
            {
                return 15;
            }
            else
            {
                return size;
            }
        }
        else
        {
            return 10;
        }
    }

    public float GetSpeed() //getting speed form text
    {
        string speedy = speed.text;
        if (float.TryParse(speedy, out float result))
        {
            float pause = float.Parse(speedy);
            if (pause > 5) //no larger than 5 second pause
            {
                return 5f;
            }
            else if (pause < .2) //no smaller than 0.2 second pause
            {
                return 0.2f;
            }
            else
            {
                return pause;
            }
            
        }
        else
        {
            return 1f; //default of 1
        }
    }

    public string GetDesign() //getting design from the design textbox
    {
        return design.text;
    }
}
