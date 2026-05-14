using UnityEngine;

public class ChapterStone : MonoBehaviour
{
    public ChapterData chapter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && UIManager.Instance != null)
        {
            UIManager.Instance.OpenPanel(chapter);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && UIManager.Instance != null)
        {
            UIManager.Instance.ClosePanel();
        }
    }
}