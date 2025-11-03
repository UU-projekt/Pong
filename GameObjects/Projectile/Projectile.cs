using System.Dynamic;

public interface IProjectileMovement
{
    (int X, int Y) GetMove(Projectile projectile, Paddle target);
}

public class GuidedMovement : IProjectileMovement
{
    public (int X, int Y) GetMove(Projectile projectile, Paddle target)
    {
        var delta = projectile.DistanceVectorTo(target);

        return (delta.deltaX > 1 ? -1 : 1, delta.deltaY > 1 ? -1 : 1);
    }
}

public class DumbfireMovement : IProjectileMovement
{
    public (int X, int Y) GetMove(Projectile projectile, Paddle target)
    {
        return (projectile.Velocity.X, 0);
    }
}

// KRAV #3:
// 1: bridge pattern
// 2: det kan finnas flera olika typer av projectile som har olika movement och effect och det går att blanda som man vill
// 3: Vi vill ha olika typer av projektiler (dumb fire och guided) och dessa har olika effekter när dom "träffar"
public class Projectile : GameObject
{
    public IProjectileMovement Movement { get; set; }
    public IProjectiveEffect Effect { get; set; }
    private Paddle _target;
    public (int X, int Y) Velocity { get; set; } = (1, 1);
    public bool IsActive { get; private set; } = true;

    public Projectile(IProjectileMovement movement, IProjectiveEffect effect, (int X, int Y) position, (int X, int Y) velocity, Paddle target) : base(1, 1)
    {
        Movement = movement;
        Effect = effect;
        Velocity = velocity;
        _target = target;
        MoveTo(position.X, position.Y);
    }

    public override void Update(GameState state)
    {
        if (!IsActive) return;
        var move = Movement.GetMove(this, _target);
        TranslateClamped(move.X, move.Y);

        if (DistanceTo(_target) < 5)
        {
            // BOOM
            Effect.ApplyEffect(_target);
            IsActive = false;
        }

        if (Position.X == 0 || Position.X == Console.BufferWidth - 1)
        {
            IsActive = false;
        }
    }

    public override void Draw()
    {
        if (!IsActive) return;
        Console.SetCursorPosition(Position.X, Position.Y);
        Console.WriteLine("💣");
    }
}