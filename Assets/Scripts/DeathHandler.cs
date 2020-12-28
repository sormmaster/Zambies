using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] Text notify;
    private void Start()
    {
        gameOverCanvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public void die()
    {
        gameOverCanvas.enabled = true;
        Time.timeScale = 0;
        FindObjectOfType<WeaponManager>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        FindObjectOfType<AudioController>().Play("gameLoss");
    }

    public void notifyUser(string message)
    {
        notify.text = message;
    }

    public IEnumerator notifyUser(string message, float elapse)
    {
        notify.text = message;
        yield return new WaitForSeconds(elapse);
        notify.text = "";
    }
}
