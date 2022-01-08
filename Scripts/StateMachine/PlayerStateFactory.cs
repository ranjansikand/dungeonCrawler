public class PlayerStateFactory
{
    PlayerStateMachine _context;

    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        _context = currentContext;
    }

    // Root states that pose an environment
    public PlayerBaseState Grounded()
    {
        return new PlayerGroundedState(_context, this);
    }

    public PlayerBaseState Jump()
    {
        return new PlayerJumpState(_context, this);
    }

    public PlayerBaseState Falling()
    {
        return new PlayerFallState(_context, this);
    }
    
    // Mid-level action states
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

    public PlayerBaseState Attack()
    {
        return new PlayerAttackState(_context, this);
    }

    public PlayerBaseState Dodge()
    {
        return new PlayerDodgeState(_context, this);
    }

    public PlayerBaseState AirAttack()
    {
        return new PlayerAirAttack(_context, this);
    }
    
    // "Leaf" states that modify actions
    public PlayerBaseState Standard()
    {
        return new PlayerStandardState(_context, this);
    }

    public PlayerBaseState Block()
    {
        return new PlayerBlockState(_context, this);
    }

    public PlayerBaseState Glide()
    {
        return new PlayerGlideState(_context, this);
    }
}
