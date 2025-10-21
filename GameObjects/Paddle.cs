public class Paddle<T> : GameObject where T : IPaddleController
{
    public Paddle(uint width, uint height) : base(width, height)
    {

    }
}