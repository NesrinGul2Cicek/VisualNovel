using UnityEngine;
using UnityEngine.InputSystem;

// Mobil kontrollerin (joystick + interact butonu) ortak parent objesine
// bu scripti ekle. Touchscreen algýlanmayan cihazlarda (PC/Mac) kontrolleri
// otomatik gizler, touch cihazlarda otomatik gösterir.
public class MobileControlsVisibility : MonoBehaviour
{
    [Tooltip("Platform ne olursa olsun kontrolleri her zaman göster. Editor'de test etmek için kullanýþlý.")]
    public bool forceShow = false;

    private void Start()
    {
        bool hasTouch = Touchscreen.current != null;
        gameObject.SetActive(forceShow || hasTouch);
    }
}