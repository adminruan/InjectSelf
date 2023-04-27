using My.Test3;

namespace My.Test2;

public class Test2Service : ITest2Service
{
    private readonly ITest3Service _test3Service;
    public Test2Service(ITest3Service test3Service)
    {
        _test3Service = test3Service;
    }

    public void Show()
    {
        Console.WriteLine("This is from Test2Service show");

        _test3Service.Show();
    }
}
