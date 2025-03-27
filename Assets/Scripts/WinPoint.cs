using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPoint : MonoBehaviour
{
    [SerializeField] private WinDialog winDialog;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<PlayerController>())
        {
            if (winDialog)
            {
                winDialog.Show(true);
                Time.timeScale = 0f;
            }
        }
    }
    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Scene0");
        if (winDialog)
        {
            winDialog.Show(false);
        }
    }
}
