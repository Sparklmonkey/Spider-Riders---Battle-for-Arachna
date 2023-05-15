using System;

[Serializable]
public struct StatModifiers : IEquatable<StatModifiers>
{
    public static readonly StatModifiers Empty = new StatModifiers();

    public bool IsEmpty => this == Empty;
    public int attackModifier, defenseModifier, diceModifier;

    public StatModifiers(int _attackModifier = 0, int _defenseModifier = 0, int _diceModifier = 0)
    {
        this.attackModifier = _attackModifier;
        this.defenseModifier = _defenseModifier;
        this.diceModifier = _diceModifier;
    }

    public static StatModifiers operator +(StatModifiers a) => a;
    public static StatModifiers operator -(StatModifiers a) => new StatModifiers(
        -a.attackModifier,
        -a.defenseModifier,
        -a.diceModifier);
    public static StatModifiers operator +(StatModifiers a, StatModifiers b) => new StatModifiers(
        a.attackModifier + b.attackModifier,
        a.defenseModifier + b.defenseModifier,
        a.diceModifier + b.diceModifier);
    public static StatModifiers operator -(StatModifiers a, StatModifiers b) => new StatModifiers(
        a.attackModifier - b.attackModifier,
        a.defenseModifier - b.defenseModifier,
        a.diceModifier - b.diceModifier);
    public static bool operator ==(StatModifiers a, StatModifiers b) => 
        a.attackModifier == b.attackModifier
        && a.defenseModifier == b.defenseModifier
        && a.diceModifier == b.diceModifier;
    public static bool operator !=(StatModifiers a, StatModifiers b) =>
        a.attackModifier != b.attackModifier
        || a.defenseModifier != b.defenseModifier
        || a.diceModifier != b.diceModifier;
    
    public bool Equals(StatModifiers other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return attackModifier.Equals(other.attackModifier)
            && defenseModifier.Equals(other.defenseModifier)
            && diceModifier.Equals(other.diceModifier);
    }
    public override bool Equals(object obj) => obj is StatModifiers other && Equals(other);
    public override int GetHashCode() => (attackModifier, defenseModifier, diceModifier).GetHashCode();
    public override string ToString() => $"Attack: {attackModifier}, Defense: {defenseModifier}, Dice: {diceModifier}";
}
