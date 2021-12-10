public class MobFactory
{
    MobMachine _context;

    public MobFactory(MobMachine currentContext)
    {
        _context = currentContext;
    }

    public MobBase Searching()
    {
        return new MobSearching(_context, this);
    }

    public MobBase Detected()
    {
        return new MobDetected(_context, this);
    }

    public MobBase Patrol()
    {
        return new MobPatrol(_context, this);
    }

    public MobBase Looking()
    {
        return new MobLooking(_context, this);
    }

    public MobBase Chase()
    {
        return new MobChase(_context, this);
    }

    public MobBase Attack()
    {
        return new MobAttack(_context, this);
    }

    public MobBase Dead()
    {
        return new MobDead(_context, this);
    }

    public MobBase Retreat()
    {
        return new MobRetreat(_context, this);
    }

    public MobBase Circle()
    {
        return new MobCircling(_context, this);
    }
}
