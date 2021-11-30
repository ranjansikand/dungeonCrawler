public class MobStateFactory
{
    MobStateMachine _context;

    public MobStateFactory(MobStateMachine currentContext)
    {
        _context = currentContext;
    }

    public MobBaseState Idle()
    {
        return new MobIdleState(_context, this);
    }

    public MobBaseState Patrol()
    {
        return new MobPatrolState(_context, this);
    } 

    public MobBaseState Chase()
    {
        return new MobChaseState(_context, this);
    } 

    public MobBaseState Attack()
    {
        return new MobAttackState(_context, this);
    } 
}
