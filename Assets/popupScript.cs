using TMPro;
using UnityEngine;

public class XPTextPopup : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float floatSpeed = 1f;
    public float fadeDuration = 1f;

    private float timer = 0f;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetText(string message)
    {
        if (text != null)
        {
            text.text = message;
        }
    }

    private void Update()
    {
        // Text Drag
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

        // Text Fading
        timer += Time.deltaTime;
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1 - (timer / fadeDuration);
        }

        if (timer >= fadeDuration)
        {
            Destroy(transform.root.gameObject);

        }
    }
}
