//var paddle = new Paddle(5, 5);
Console.WriteLine("PONG PONG PONG PONG :D");



var ball = new Ball(1, 1, 1, 1);

// CONTROLLERS
var cpuController = new CPUController(ball);
var humanController = new HumanController();

// PADDLES
// (vi bör fixa builders för dessa)
var paddle1 = new Paddle(5, 5, humanController);
paddle1.paddleColour = ConsoleColor.White;
paddle1.MoveTo(1, 0);


var paddle2 = new Paddle(5, 5, cpuController);
paddle2.paddleColour = ConsoleColor.DarkRed;
paddle2.MoveTo((uint)Console.BufferWidth - 1, 0);

var gameItems = new GameObjectCollection<GameObject>();
gameItems.AddItem("paddle1", paddle1);
gameItems.AddItem("paddle2", paddle2);
gameItems.AddItem("ball", ball);

Console.CursorVisible = false;

// GAME LOOP
while (true)
{
    Console.Clear();

    foreach (var gameObject in gameItems)
    {
        gameObject.Update();
        gameObject.Draw();
        Console.ResetColor();
    }

    Thread.Sleep(100);
}