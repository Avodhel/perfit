public class SquarePooler : ObjectPooler {

    public static SquarePooler SharedInstance { get; private set; }

    public override void Awake()
    {
        SharedInstance = this;
        base.Awake();
    }
}
