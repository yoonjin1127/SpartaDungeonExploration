using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMachine : MonoBehaviour
{
    public float jumpForce = 100f;
    private void OnTriggerEnter(Collider other)
    {
        // other에 Player 컴포넌트가 존재한다면 받아서 player로 반환
        if(other.TryGetComponent<Player>(out Player player))
        {
            Rigidbody playerRb = player.GetComponent<Rigidbody>();

            if(playerRb != null)
            {
                // 기존의 y 속도는 제거
                playerRb.velocity = new Vector3(playerRb.velocity.x, 0f, playerRb.velocity.z);
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }
}
