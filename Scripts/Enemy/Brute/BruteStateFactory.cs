
public class BruteStateFactory
{
    BruteMachine _context;

    public BruteStateFactory(BruteMachine currentContext)
    {
        _context = currentContext;
    }

    // Super-states
    public BruteBaseState Searching()
    {
        return new BruteSearching(_context, this);
    }

    public BruteBaseState Detected()
    {
        return new BruteDetected(_context, this);
    }

    public BruteBaseState Staggered()
    {
        return new BruteStaggered(_context, this);
    }

    public BruteBaseState Dead()
    {
        return new BruteDead(_context, this);
    }

    // Leaf states
    public BruteBaseState Patrol()
    {
        return new BrutePatrol(_context, this);
    }

    public BruteBaseState Looking()
    {
        return new BruteLooking(_context, this);
    }

    public BruteBaseState Block()
    {
        return new BruteGuard(_context, this);
    }

    public BruteBaseState Attack()
    {
        return new BruteAttack(_context, this);
    }

    public BruteBaseState Pursue()
    {
        return new BrutePursue(_context, this);
    }
}
