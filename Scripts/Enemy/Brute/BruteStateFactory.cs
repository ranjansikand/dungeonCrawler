
public class BruteStateFactory
{
    BruteMachine _context;

    public BruteStateFactory(BruteMachine currentContext)
    {
        _context = currentContext;
    }

    public BruteBaseState Searching()
    {
        return new BruteSearching(_context, this);
    }

    public BruteBaseState Patrol()
    {
        return new BrutePatrol(_context, this);
    }

    public BruteBaseState Looking()
    {
        return new BruteLooking(_context, this);
    }
}
