using My.Test5;

namespace My.Test3;

public class Test3Service : ITest3Service
{
    private readonly ITest5Service _test5Service;
    public Test3Service(ITest5Service test5Service)
    {
        _test5Service = test5Service;
    }

    public void Show()
    {
        Console.WriteLine("This is from Test3Service show");
        _test5Service.Show();
    }
}
