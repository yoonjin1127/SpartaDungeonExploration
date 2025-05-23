using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue;
    public float startValue;
    public float maxValue;
    public float passiveValue;
    public Image uiBar;

    // Start is called before the first frame update
    void Start()
    {
        // 현재 값은 시작 값
        curValue = startValue;
    }

    // Update is called once per frame
    void Update()
    {
        // ui 업데이트
        uiBar.fillAmount = GetPercentage();
    }

    float GetPercentage()
    {
        // 현재의 값 / 최댓값
        return curValue / maxValue;
    }

    public void Add(float value)
    {
        // value에 들어온 값 중, 작은 쪽이 선택된다
        curValue = Mathf.Min(curValue + value, maxValue);
    }

    public void Subtract(float value)
    {
        // value에 들어온 값 중, 큰 쪽이 선택된다
        curValue = Mathf.Max(curValue - value, 0);
    }
}
