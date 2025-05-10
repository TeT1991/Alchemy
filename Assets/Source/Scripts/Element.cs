public class Element
{
    public ElementData Data { get; }
    public bool IsDiscovered { get; private set; }
    public bool IsNew { get; private set; }

    public Element(ElementData data)
    {
        Data = data;
        IsDiscovered = false;
        IsNew = false;
    }

    public void MarkAsDiscovered() { IsDiscovered = true; IsNew = true; }
    public void MarkAsSeen() { IsNew = false; }
}

