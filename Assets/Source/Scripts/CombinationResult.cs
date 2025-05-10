public class CombinationResult
{
    public CombinationResultStatus Status { get; }
    public Element NewElement { get; }

    public CombinationResult(CombinationResultStatus status, Element newElement = null)
    {
        Status = status;
        NewElement = newElement;
    }
}
