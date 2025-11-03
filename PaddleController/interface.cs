public interface IPaddleController
{
    // Funktion som ger ett värde på nästa move.
    // I vårt fall behöver vi bara ange ändringen för Y eftersom X alltid kommer vara densamma då vi bara rör paddle upp eller ner
    (int movement, bool ShouldFire) GetMove(Paddle paddle);
}
