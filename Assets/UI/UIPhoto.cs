using UnityEngine;
using UnityEngine.UI;

public class UIPhoto : MonoBehaviour
{
    private Image photoImage;

    public void Init(Sprite photo)
    {
        // Находим дочерний объект "Photo" и получаем его компонент Image
        photoImage = transform.Find("Photo").GetComponent<Image>();

        // Устанавливаем спрайт фотографии
        if (photoImage != null)
        {
            photoImage.sprite = photo;
        }
        else
        {
            Debug.LogError("Не найден Image с именем 'Photo' внутри Palaroid!");
        }
    }
}
