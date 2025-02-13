using UnityEngine;
using UnityEngine.UI;

public class SliderEng1_08 : MonoBehaviour
{

    [SerializeField]
    private Text mass;
    [SerializeField]
    private Slider sli
    [SerializeField]
    private Text lenght;
    [SerializeField]
    private Slider sliderLenght;
    [SerializeField]
    private Text APS;
    [SerializeField]
    private Slider sliderAPS;

    public static float ToSingle(double value)  //функция конвертации типа данных из double в float
    {
        return (float)value;
    }
    
    void SliderSettings()                                                           //функция изменения начальных условий установки
    {
        mass.text = (sliderMass.value * 0.1).ToString();
        lenght.text = (sliderLenght.value * 0.05).ToString();
        APS.text = sliderAPS.value.ToString("0.0000");
    }
    
    private void Start()
    {
        SliderSettings();
    }
    void Update()
    {
        if (/*valueSlider != sliderHight.value*/ true)      //для экономии ресурсов, расчёты новых координат объектов происходит только в случае изменения настроек (новое значение не равно старому значению)
                                                            //значение true вставил для заглушки. Убрать при добавлении своих параметров
        {
            SliderSettings();
        }
    }
}
