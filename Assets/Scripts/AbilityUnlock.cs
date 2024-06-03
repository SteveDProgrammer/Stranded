using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.VersionControl;

public class AbilityUnlock : MonoBehaviour
{
    public bool unlockDoubleJump, unlockDash, unlockBallMode, unlockBombing;
    public GameObject pickUpFX;

    public string unlockMessage;
    public TMP_Text unlockText;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            AbilityTracker abilityTracker = other.gameObject.GetComponentInParent<AbilityTracker>();

            if(unlockDash) abilityTracker.dashUnlocked = unlockDash;
            if(unlockDoubleJump) abilityTracker.doubleJumpUnlocked = unlockDoubleJump;
            if(unlockBallMode) abilityTracker.ballModeUnlocked = unlockBallMode;
            if(unlockBombing) abilityTracker.bombingUnlocked = unlockBombing;

            Instantiate(pickUpFX, transform.position, transform.rotation);
            
            unlockText.transform.parent.SetParent(null);
            unlockText.transform.parent.position = transform.position;
            unlockText.text = unlockMessage;

            unlockText.gameObject.SetActive(true);
            Destroy(unlockText.transform.parent.gameObject, 5f);

            Destroy(gameObject);
        }
    }
}
