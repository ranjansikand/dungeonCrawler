public class PlayerStateFactory
{
    PlayerStateMachine _context;

    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        _context = currentContext;
    }

    public PlayerBaseState Idle()
    {
        return new PlayerIdleState(_context, this);
    }

    public PlayerBaseState Walk()
    {
        return new PlayerWalkState(_context, this);
    }

    public PlayerBaseState Run()
    {
        return new PlayerRunState(_context, this);
    }

    public PlayerBaseState Jump()
    {
        return new PlayerJumpState(_context, this);
    }

    public PlayerBaseState Grounded()
    {
        return new PlayerGroundedState(_context, this);
    }

    public PlayerBaseState Attack()
    {
        return new PlayerAttackState(_context, this);
    }

    public PlayerBaseState Dodge()
    {
        return new PlayerDodgeState(_context, this);
    }

    public PlayerBaseState Standard()
    {
        return new PlayerStandardState(_context, this);
    }

    public PlayerBaseState Block()
    {
        return new PlayerBlockState(_context, this);
    }

    public PlayerBaseState Falling()
    {
        return new PlayerFallState(_context, this);
    }
}
