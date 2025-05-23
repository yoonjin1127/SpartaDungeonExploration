using System;
using System.Collections;
using UnityEngine;

public interface IDamagable
{
    void TakePhysicalDamage(int damage);
}

public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uiCondition;
    private PlayerController controller;

    Condition health { get { return uiCondition.health; } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition stamina { get { return uiCondition.stamina; } }
    Condition jumpPower { get { return uiCondition.jumpPower; } }

    // 허기 0일때 체력 감소량
    public float noHungerHealthDecay;

    // 내부에서 호출하는 이벤트, 액션
    public event Action onTakeDamage;

    private void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
    }

    private void Update()
    {
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if(hunger.curValue == 0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        if(health.curValue == 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    // 5초동안 점프력을 강화하는 코루틴 실행
    public void JumpEnforce(float amount)
    {
        StartCoroutine(JumpEnforceCoroutine(amount, 5f));
    }

    private IEnumerator JumpEnforceCoroutine(float amount, float duration)
    {
        jumpPower.Add(amount);
        controller.jumpPower += amount;

        yield return new WaitForSeconds(duration);

        jumpPower.Subtract(amount);
        controller.jumpPower -= amount;
    }

    public void Die()
    {

    }

    public void TakePhysicalDamage(int damage)
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();
    }
}
