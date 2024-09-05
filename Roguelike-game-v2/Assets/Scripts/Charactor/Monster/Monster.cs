public interface Monster : IDamage, IDamageReceiver, IMoveable
{
    public Stat_SO Stat { get; }
}
