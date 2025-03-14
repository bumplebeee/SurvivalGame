using UnityEngine;

public class Gun : MonoBehaviour
{
    private float rotateOffset = 180f;
    [SerializeField] private Transform bulletPos;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shotDelay = 0.15f;
    private float nextShot;

    [SerializeField] private GameObject skillPrefab;
    private float skillCooldown = 10f;
    private float nextSkillTime = 0f;
    void Update()
    {
        RotateGun();
        HandleInput();
        if (PlayerSkill1.cooldownTimer > 0)
        {
            PlayerSkill1.cooldownTimer -= Time.deltaTime;
        }
    }

    void RotateGun()
    {
        if (Input.mousePosition.x < 0 || Input.mousePosition.x > Screen.width || Input.mousePosition.y < 0 || Input.mousePosition.y > Screen.height)
        {
            return;
        }

        Vector3 displacement = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + rotateOffset);
    }

    void HandleInput()
    {
        if (Input.GetMouseButton(0) && Time.time > nextShot)
        {
            Shoot();
        }

        if (Input.GetMouseButtonDown(1) && PlayerSkill1.CanUseSkill())
        {
            UseSkill();
        }
    }

    void Shoot()
    {
        nextShot = Time.time + shotDelay;
        Instantiate(bulletPrefab, bulletPos.position, bulletPos.rotation);
    }

    void UseSkill()
    {
        if (PlayerSkill1.CanUseSkill())
        {
            PlayerSkill1.ActivateSkill();
            Instantiate(skillPrefab, bulletPos.position, bulletPos.rotation);
            Debug.Log("Skill used! Cooldown started.");
        }
    }
}
