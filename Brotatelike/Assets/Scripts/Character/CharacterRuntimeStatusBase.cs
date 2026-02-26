using UnityEngine;

public abstract class CharacterRuntimeStatusBase : MonoBehaviour
{
    public virtual float MaxHealth => 0;
    public virtual float Strength => 0;
    public virtual float AttackSpeed => 0;
    public virtual float AttackRange => 0;
    public virtual float MoveSpeed => 0;
}
