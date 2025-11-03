// KRAV #4:
// 1: Factory Method Pattern
// 2: Vi använder det genom att skapa abstrakta klassen GenericFactory och sen två implementationer av denna
// 3: Vi använder det för att göra det enklare att skapa random projektiler. Sen har vi klassen DefectiveMissileFactory som kan
//    användas om man vill lägga till så att missiler har en chans att träffa en själv istället
public abstract class GenericFactory
{
    static Random rand = new();

    protected (int X, int Y) getVelocity(Paddle sender, Paddle target)
    {
        var dist = sender.DistanceVectorTo(target);
        if (dist.deltaX > 0) return (-1, 0);
        else return (1, 0);
    }

    protected Projectile create(Paddle sender, Paddle target)
    {
        IProjectileMovement movement = rand.NextDouble() < 0.8 ? new DumbfireMovement() : new GuidedMovement();
        IProjectiveEffect warhead;

        double effectChoice = rand.NextDouble();
        if (effectChoice < 0.33)
        {
            warhead = new FreezeEffect();
        }
        else if (effectChoice < 0.66)
        {
            warhead = new ShrinkEffekt();
        }
        else
        {
            warhead = new Move();
        }


        return new Projectile(movement, warhead, sender.Position, getVelocity(sender, target), target);
    }

    public abstract Projectile CreateMissile(Paddle sender, Paddle target);
}


public class NormalMissileFactory : GenericFactory
{
    public override Projectile CreateMissile(Paddle sender, Paddle target)
    {
        return create(sender, target);
    }
}

public class DefectiveMissileFactory : GenericFactory
{
    static Random rand = new();
    public override Projectile CreateMissile(Paddle sender, Paddle target)
    {
        if (rand.NextDouble() > 0.8)
        {
            var temp = sender;
            sender = target;
            target = temp;
        }

        return create(sender, target);
    }
}