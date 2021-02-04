using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCharacter : MonoBehaviour
{
    CharacterMovement character;
    private void Awake()
    {
        character = GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        character.Move();
    }
}
