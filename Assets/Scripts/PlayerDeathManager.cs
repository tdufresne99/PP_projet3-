using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mother;
using Father;
using Boy;

public class PlayerDeathManager : MonoBehaviour
{
    [SerializeField] private MotherStateManager _motherManagerCS;
    [SerializeField] private FatherStateManager _fatherManagerCS;
    [SerializeField] private BoyStateManager _boyManagerCS;

    [SerializeField] private Animator _deathCanvasAnimator;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _playerRespawnTransform;
    private string _fadeOutTrigger = "fadeOut";
    private string _fadeInTrigger = "fadeIn";

    [SerializeField] private Animator _smokeCanvasAnimator;
    private string _smokeOnTrigger = "smokeOn";
    private string _smokeOffTrigger = "smokeOff";
    private bool _smokeStarted = false;

    [SerializeField] private float _timeToKillPlayerWithSmoke = 5f;
    private Coroutine _coroutineSmokePlayer;

    private Coroutine _coroutineResetGame;

    void Start()
    {
        _deathCanvasAnimator.SetTrigger(_fadeInTrigger);
    }

    private IEnumerator CoroutineResetGameState()
    {
        ResetPlayer();
        yield return new WaitForSecondsRealtime(1f);
        ResetAIStates();
        ResetEnvironment();
    }


    private void ResetAIStates()
    {
        if(_boyManagerCS.gameObject.activeInHierarchy) _boyManagerCS.ResetState();
        if(_fatherManagerCS.gameObject.activeInHierarchy) _fatherManagerCS.ResetState();
        if(_motherManagerCS.gameObject.activeInHierarchy) _motherManagerCS.ResetState();
    }

    private void ResetEnvironment()
    {

    }

    private void ResetPlayer()
    {
        PlayDeathCanvasAnimations();
    }

    private void PlayDeathCanvasAnimations()
    {
        _deathCanvasAnimator.SetTrigger(_fadeOutTrigger);
        Invoke("FadeInPlayerDeathCanvas", 4f);
    }

    public void ResetPlayerPosition()
    {
        _playerTransform.position = _playerRespawnTransform.position;
    }

    private void FadeInPlayerDeathCanvas()
    {
        _deathCanvasAnimator.SetTrigger(_fadeInTrigger);
        Invoke("ToggleDeathCanvas", 2f);
    }

    public void StartCoroutineResetGame()
    {
        _coroutineResetGame = StartCoroutine(CoroutineResetGameState());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<MeleeAttack>() != null)
        {
            StartCoroutineResetGame();
        }
    }

    public void ToggleCoroutineSmoke(bool start)
    {
        if (start)
        {
            if (_smokeStarted == false) _coroutineSmokePlayer = StartCoroutine(CoroutineSmokePlayer());
        }
        else if (_smokeStarted == true)
        {
            if(_coroutineSmokePlayer != null) StopCoroutine(_coroutineSmokePlayer);
            _smokeCanvasAnimator.SetTrigger(_smokeOffTrigger);
            _smokeStarted = false;
        }
    }

    private IEnumerator CoroutineSmokePlayer()
    {
        _smokeStarted = true;
        _smokeCanvasAnimator.SetTrigger(_smokeOnTrigger);
        yield return new WaitForSecondsRealtime(_timeToKillPlayerWithSmoke);
        StartCoroutineResetGame();
        _smokeStarted = false;
    }
}
