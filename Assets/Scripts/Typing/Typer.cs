using UnityEngine;

public class Typer : MonoBehaviour
{
    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.anyKeyDown)
        {
            string keyPressed = Input.inputString;

            if (keyPressed.Length == 1)
               ManagerTyping.instance.EnterTypeLetter(keyPressed);
        }
    }
}
