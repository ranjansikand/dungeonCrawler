public class EnemyStateFactory
{
    EnemyStateMachine _context;

    public EnemyStateFactory(EnemyStateMachine currentContext)
    {
        _context = currentContext;
    }

    public EnemyBaseState Patrol()
    {
        return new EnemyPatrolState(_context, this);
    }

    public EnemyBaseState Detected()
    {
        return new EnemyDetectedState(_context, this);
    }

    public EnemyBaseState Attack()
    {
        return new EnemyAttackState(_context, this);
    }

    public EnemyBaseState Standard()
    {
        return new EnemyStandardState(_context, this);
    }
}
