using UnityEngine;

public class Lamp : ScriptToUse
{
    Light lightComponent;
    public override void Toggle()
    {
        active = !active;

        if (lightComponent != null)
        {
            lightComponent.enabled = active;
        }
    }
    void Start()
    {
        lightComponent = GetComponent<Light>();
        if (lightComponent == null)
        {
            lightComponent = gameObject.AddComponent<Light>();
        }
        // Инициализируем состояние
        lightComponent.enabled = active;
    }
    // Изменение цвета света
    public void ChangeColor(Color newColor)
    {
        if (lightComponent != null)
        {
            lightComponent.color = newColor;
        }

    }
    // Метод для внешнего использования (например, из других скриптов)
    public void UseAnywhere(Color color = default)
    {
        TurnOn();
        if (color != default)
        {
            ChangeColor(color);
        }
    }
    // Включение света (можно вызвать извне)
    public void TurnOn()
    {
        active = true;
        if (lightComponent != null)
        {
            lightComponent.enabled = true;
        }
    }
    // Выключение света (можно вызвать извне)
    public void TurnOff()
    {
        active = false;
        if (lightComponent != null)
        {
            lightComponent.enabled = false;
        }
    }
}
