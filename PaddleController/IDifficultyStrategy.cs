
// 1: Vi använder Strategy Pattern för att hantera olika 
//    AI-svårighetsgrader.
// 2: IDifficultyStrategy definierar kontraktet, och 
//    EasyStrategy, MediumStrategy och HardStrategy 
//    implementerar olika beteenden. Strategierna injiceras 
//    i CPUController.
// 3: Detta gör det lätt att lägga till nya svårighetsgrader
//    utan att ändra befintlig kod.
//    Varje strategi skiljer sig i BETEENDE (olika AI-logik),
//    inte bara data.
public interface IDifficultyStrategy
{
    int CalculateMove(int ballY, int paddleCenter);
}
