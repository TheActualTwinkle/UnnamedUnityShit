using System.Collections.Generic;
using System.Linq;

public abstract class GameCharacter : Unit
{
    public List<Buff> Buffs { get; set; } = new List<Buff>();

    public void RemoveAllBuffs()
    {
        foreach (var buff in Buffs.ToList())
            buff.TryDeactivateBuff(this);
    }

    public void AddBuff(Buff buff, float currentDuration = 0f)
    {
        buff.ActivateBuff(this, currentDuration);
    }

    public void RemoveBuff(Buff buff)
    {
        buff.TryDeactivateBuff(this);
    }
}
