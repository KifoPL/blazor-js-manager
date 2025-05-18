namespace BlazorJsManager.Tests;

public class UnitTest1
{
    [Test]
    public async Task Test1()
    {
        bool result = string.IsNullOrWhiteSpace("");
        await Assert.That(result).IsEqualTo(true);
    }
}