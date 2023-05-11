using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public GameObject PlayerBulletGO;

    // Start is called before the first frame update
    void Start()
    {
        // đạn của địch được bắn sau 1s
        Invoke("FirePlayerBullet", 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // hàm tạo đạn bắn của tàu địch
    void FirePlayerBullet()
    {
        // tìm đối tượng player
        GameObject enemyShip = GameObject.Find("EnemySpawnerGO");

        if (enemyShip != null)
        {
            // khởi tạo đạn của tàu địch
            GameObject bullet = (GameObject)Instantiate(PlayerBulletGO);

            // khởi tạo vị trí viên đạn xuất hiện = vị trí của tàu địch
            bullet.transform.position = transform.position;

            // hướng viên đạn đến tàu player
            Vector2 direction = enemyShip.transform.position - bullet.transform.position;

            // đặt hướng của viên đạn
            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
        }
    }
}
