using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Lab08Engine : MonoBehaviour
{
    public GameObject line;
    public GameObject sphere;
    public GameObject parent;
    public GameObject down;
    public Slider sliderLenght;
    public Slider sliderMass;
    public Slider sliderAPS;
    public MenuExp menuExpScript;           //объект скрипта меню эксперимента, откуда будет получена ссылка на триггер кнопки перезагрузки
    public TextMeshPro periodSurface;
    public TextMeshProUGUI periodUI;

    public Text numOfExp;           //объект текста из меню, показывающего число проведённых экспериментов
    int trig = 0;                   //триггер
    public double A = 5;
    [SerializeField]
    public double result = 0;
    private Download108 data = new Download108();

    [SerializeField]
    double lenght;
    double mass;
    double hight;
    [SerializeField]
    double angV;                    //угловая скорость 
    double ang;
    double T;
    double g = 9.80665;             //ускорение свободного падения
    double time = 0;
    double dz;

    //double[,,] results = new double[5, 4, 2];  //массив для данных для будущего вывода на экран и для записи в файл
    int i = 0;

    // Погрешность
    float eps = 0.0005F;
    private double aps = 0;

    public static float ToSingle(double value)      //функция конвертации типа данных из double в float
    {
        return (float)value;
    }

    void labCalculation()                           //функция расчётов
    {
        mass = sliderMass.value;
        hight = line.transform.localPosition.y;

        angV = g / lenght;
        angV = Mathf.Sqrt(ToSingle(angV));
        T = 2 * Mathf.PI / angV;

        result = 2 * Mathf.PI * Mathf.Sqrt((float)((lenght + Random.Range(-1 * sliderAPS.value, sliderAPS.value)) / g)) + Random.Range(-1 * eps, eps);

        data.add((int)mass, lenght, result);
    }
    void labAnimation()
    {
        time = time + Time.deltaTime;
        ang = A * Mathf.Sin(ToSingle(angV * time));
        dz = Mathf.Sin(ToSingle(ang * Mathf.PI / 180)) * sliderLenght.value * 0.05;
        A = A - 0.0001;
        if (A < 0)
        {
            trig = 0;
            A = 5;
            return;
        }
        // Учитываем сопротивление воздуха
        parent.transform.eulerAngles = new Vector3(ToSingle(-ang), 0, 0);
    }
    void LabIsOn()                                      //функция, отключающая возможность изменять настройки лабораторной работы после активации
    {
        GameObject obj = GameObject.Find("LabEngine");
        Lab08SliderEng SEscript = obj.GetComponent<Lab08SliderEng>();
        SEscript.enabled = false;
        sliderLenght.interactable = false;
        sliderMass.interactable = false;
        sphere.transform.parent = parent.transform;
        line.transform.parent = parent.transform;
    }
    void Start()
    {
        menuExpScript.pdf = "file:///C:/Program%20Files/Projects/Lab/Assets/StreamingAssets/Lab_1p08.pdf";  //ссылка на методические указания
    }

    void Update()
    {
        if (Input.GetKey("space") && (trig == 0))       //активации лабораторной работы и расчёты
        {
            trig = 1;
            i++;
            numOfExp.text = "Проведено измерений: " + i;
            labCalculation();
            LabIsOn();
        }
        if (trig == 1)                                  //запуск анимации лабораторной работы
        {
            labAnimation();
            if (time > result)
            {
                periodSurface.text = result.ToString("0.00");
                periodUI.text = result.ToString("0.0000");
            }
        }

        if (trig == 0)
        {
            lenght = sliderLenght.value * 0.05;
            line.transform.localScale = new Vector3(
                line.transform.localScale.x,
                (float)lenght,
                line.transform.localScale.z);
            line.transform.localPosition = new Vector3(
                line.transform.localPosition.x,
                -0.255F + 0.05F * (5 - ((int)(lenght * 100) / 5)),
                line.transform.localPosition.z
            );

            sphere.transform.localPosition = new Vector3(
                sphere.transform.localPosition.x,
                -0.515F + 0.1F * (5 - ((int)(lenght * 100) / 5)),
                sphere.transform.localPosition.z
            );

            down.transform.localPosition = new Vector3(
                down.transform.localPosition.x,
                1.25F + 0.1F * (5 - ((int)(lenght * 100) / 5)),
                down.transform.localPosition.z
            );
        }
        if (menuExpScript.resetTrig == 1)               //функция перезагрузки эксперимента, сбрасывает настройки установки к начальным
        {
            GameObject obj = GameObject.Find("LabEngine");
            SliderEng1_08 SEscript = obj.GetComponent<SliderEng1_08>();
            SEscript.enabled = true;
            sliderLenght.interactable = true;
            sliderMass.interactable = true;

            sphere.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
            line.transform.rotation = new Quaternion(0, 0, 0, 0);
            sphere.transform.rotation = new Quaternion(0, 0, 0, 0);
            parent.transform.rotation = new Quaternion(0, 0, 0, 0);

            sliderLenght.value = 5;
            sliderMass.value = 1;

            time = 0;
            trig = 0;
            menuExpScript.resetTrig = 0;
            A = 5;

            result = 0;
            periodSurface.text = result.ToString("0.00");
            periodUI.text = result.ToString("0.0000");
        }
    }
}
