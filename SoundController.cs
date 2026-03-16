using UnityEngine;

[DisallowMultipleComponent]
public class SoundController : MonoBehaviour
{
    private const float DefaultVolume = 0.5f;
    private const float MinPitch = 0.85f;
    private const float MaxPitch = 1.15f;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _footstepSounds;
    [SerializeField] private AudioClip[] _playerDamageSounds;
    [SerializeField] private AudioClip[] _enemyDamageSounds;
    [SerializeField] private AudioClip _reloadSound;
    [SerializeField] private AudioClip _swapSound;
    [SerializeField] private AudioClip _ammoPickUpSound;
    [SerializeField] private AudioClip _healPickUpSound;
    [SerializeField] private AudioClip _enemyDieSound;
    [SerializeField] private PlayerShooter _shooter;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private WaveManager _waveManager;

    private int _lastIndexFootstep;
    private int _lastIndexEnemyDamage;
    private int _lastIndexPlayerDamage;

    private Weapon _currentWeapon;

    private void OnEnable()
    {
        if (_shooter != null)
        {
            _shooter.OnWeapon += UpdateCurrentWeapon;
            _shooter.OnSwappedSound += PlaySwap;
        }

        if (_playerController != null)
        {
            _playerController.OnMoveSound += PlayFootstep;
        }

        if (_waveManager != null)
        {
            _waveManager.OnEnemyDieSound += PlayEnemyDie;
        }

        EnemyDamage.OnPlayerDamageSound += PlayPlayerDamage;
        Ammo.OnAmmoPickedUp += PlayAmmoPickUp;
        Heal.OnHealPickedUp += PlayHealPickUp;
    }

    private void OnDisable()
    {
        if (_shooter != null)
        {
            _shooter.OnWeapon -= UpdateCurrentWeapon;
            _shooter.OnSwappedSound -= PlaySwap;
        }

        if (_playerController != null)
        {
            _playerController.OnMoveSound -= PlayFootstep;
        }

        if (_waveManager != null)
        {
            _waveManager.OnEnemyDieSound -= PlayEnemyDie;
        }

        UnsubscribeFromWeapon(_currentWeapon);

        EnemyDamage.OnPlayerDamageSound -= PlayPlayerDamage;
        Ammo.OnAmmoPickedUp -= PlayAmmoPickUp;
        Heal.OnHealPickedUp -= PlayHealPickUp;
    }

    private void UpdateCurrentWeapon(Weapon weapon)
    {
        UnsubscribeFromWeapon(_currentWeapon);
        _currentWeapon = weapon;

        if (_currentWeapon == null)
        {
            return;
        }

        _currentWeapon.OnShootSound += PlayShoot;
        _currentWeapon.OnReloadSound += PlayReload;
        _currentWeapon.OnStopSound += StopSound;
        _currentWeapon.OnHitEnemySound += PlayEnemyDamage;
    }

    private void UnsubscribeFromWeapon(Weapon weapon)
    {
        if (weapon != null)
        {
            weapon.OnShootSound -= PlayShoot;
            weapon.OnReloadSound -= PlayReload;
            weapon.OnStopSound -= StopSound;
            weapon.OnHitEnemySound -= PlayEnemyDamage;
        }
    }

    private void PlayShoot(AudioClip shootSound)
    {
        Play(shootSound);
    }

    private void PlayReload()
    {
        Play(_reloadSound);
    }

    private void PlaySwap()
    {
        Play(_swapSound);
    }

    private void PlayAmmoPickUp()
    {
        Play(_ammoPickUpSound);
    }

    private void PlayHealPickUp()
    {
        Play(_healPickUpSound);
    }

    private void PlayEnemyDie()
    {
        Play(_enemyDieSound);
    }

    private void StopSound()
    {
        _audioSource?.Stop();
    }

    private void PlayFootstep()
    {
        PlayRandom(_footstepSounds, ref _lastIndexFootstep);
    }

    private void PlayPlayerDamage()
    {
        PlayRandom(_playerDamageSounds, ref _lastIndexPlayerDamage);
    }

    private void PlayEnemyDamage()
    {
        PlayRandom(_enemyDamageSounds, ref _lastIndexEnemyDamage);
    }

    private void PlayRandom(AudioClip[] clips, ref int lastIndex)
    {
        if (clips == null || clips.Length == 0)
        {
            return;
        }

        if (clips.Length == 1)
        {
            PlayWithPitch(clips[0]);
            return;
        }

        int index;

        do
        {
            index = Random.Range(0, clips.Length);
        }
        while (index == lastIndex);

        lastIndex = index;

        PlayWithPitch(clips[index]);
    }

    private void PlayWithPitch(AudioClip clip)
    {
        if (_audioSource == null || clip == null)
        {
            return;
        }

        float originalPitch = _audioSource.pitch;

        _audioSource.pitch = Random.Range(MinPitch, MaxPitch);
        _audioSource.PlayOneShot(clip, DefaultVolume);

        _audioSource.pitch = originalPitch;
    }

    private void Play(AudioClip clip)
    {
        if (_audioSource == null || clip == null)
        {
            return;
        }

        _audioSource.PlayOneShot(clip, DefaultVolume);
    }
}