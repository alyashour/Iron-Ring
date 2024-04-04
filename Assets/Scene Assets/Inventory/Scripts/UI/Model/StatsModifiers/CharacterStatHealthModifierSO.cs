using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatHealthModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        /*
        Health health = character.GetComponent<Health>();
        if (health != null)
            health.AddHealth((int)val);
        */
        PlayerAttributes.PlayerHealth += 5;
    }
}

public class Health
{
    public void AddHealth(int val)
    {
        throw new System.NotImplementedException();
    }
}
