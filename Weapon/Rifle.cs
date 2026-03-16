using UnityEngine;

public class Rifle : Weapon
{
    private const float MinShootDot = 0.2f;
    private const float MinShootDistance = 0.5f;

    private bool _isReloading = false;

    private void OnDisable()
    {
        if (_isReloading)
        {
            ResetReload();
        }
    }

    public override void Attack(Camera camera, Transform playerTransform, LayerMask mask)
    {
        if (!CanShoot())
        {
            Reload();
            return;
        }

        if (FirePoint == null || camera == null)
        {
            return;
        }

        Shoot(camera, playerTransform, mask);
    }

    public override void Cooldown()
    {
        if (!_isReloading || _maxAmmo <= 0)
        {
            return;
        }

        if (_cooldown > 0f)
        {
            _cooldown -= Time.deltaTime;
            InvokeCooldownChanged();
        }
        else
        {
            ReloadAmmo();
        }
    }

    public override void Reload()
    {
        if (_isReloading || _maxAmmo <= 0)
        {
            return;
        }

        if (_currentAmmo == Data.ReloadAmount)
        {
            return;
        }

        _isReloading = true;
        InvokeReloadSound();
    }

    private Vector3 GetAimPoint(Camera camera, LayerMask mask)
    {
        Vector2 screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        Ray ray = camera.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hit, Data.Range, mask))
        {
            if (hit.collider.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.TakeDamage(Data.Damage);

                InvokeSpawnHitEffect(hit.point, Quaternion.LookRotation(hit.normal));
                InvokeHitEnemySound();
            }

            InvokeSpawnCollisionHitEffect(hit.point, Quaternion.LookRotation(hit.normal));

            return hit.point;
        }

        return ray.origin + ray.direction * Data.Range;
    }

    private void ReloadAmmo()
    {
        if (_cooldown <= 0f)
        {
            int neededAmmo = Mathf.Min(Data.ReloadAmount - _currentAmmo, _maxAmmo);

            _currentAmmo += neededAmmo;
            _maxAmmo -= neededAmmo;
            _isReloading = false;
            _cooldown = Data.Cooldown;

            InvokeStopSound();

            InvokeMaxAmmoChanged();
            InvokeCurrentAmmoChanged();
            InvokeCooldownChanged();
        }
    }

    private void ReloadCount()
    {
        _currentAmmo--;
        InvokeCurrentAmmoChanged();

        if (_currentAmmo <= 0 && _maxAmmo > 0)
        {
            _isReloading = true;
            InvokeReloadSound();
        }
    }

    private void ResetReload()
    {
        _isReloading = false;
        _cooldown = Data.Cooldown;
        InvokeCooldownChanged();
        InvokeStopSound();
    }

    private bool CanShoot()
    {
        return !_isReloading && _currentAmmo > 0;
    }

    private void Shoot(Camera camera, Transform playerTransform, LayerMask mask)
    {
        ReloadCount();
        InvokeShootSound();

        Vector3 targetPoint = GetAimPoint(camera, mask);
        Vector3 direction = CalculateDirection(targetPoint, playerTransform);

        if (direction == Vector3.zero)
        {
            return;
        }

        InvokeSpawnShootEffect(FirePoint.position, Quaternion.LookRotation(direction));
    }

    private Vector3 CalculateDirection(Vector3 targetPoint, Transform playerTransform)
    {
        Vector3 direction = (targetPoint - FirePoint.position).normalized;

        float distanceToTarget = Vector3.Distance(FirePoint.position, targetPoint);

        if (distanceToTarget < MinShootDistance)
        {
            direction = playerTransform.forward;
        }

        if (Vector3.Dot(playerTransform.forward, direction) < MinShootDot)
        {
            return Vector3.zero;
        }

        return direction;
    }
}