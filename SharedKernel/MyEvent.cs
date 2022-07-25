namespace SharedKernel;

public class MyEvent
{
    public string Foo { get; set; }

    public override string ToString()
    {
        return $"MyEvent Foo:{Foo}";
    }
}
