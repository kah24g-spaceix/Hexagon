using UnityEngine;

public class Lock : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Kunai"))
        {
            // 충돌된 Lock 오브젝트를 제거
            LockBehaviour.lockBehaviour.numberOfLocks--;

            // LockBehaviour에서 리스트에서 해당 오브젝트 제거
            LockBehaviour.lockBehaviour.lockObjectList.Remove(gameObject);
            LockBehaviour.lockBehaviour.orbitingObjects.Remove(gameObject);

            // 게임 오브젝트 파괴
            Destroy(gameObject);
            Destroy(other.gameObject); // Kunai 오브젝트 삭제
        }
    }
}
