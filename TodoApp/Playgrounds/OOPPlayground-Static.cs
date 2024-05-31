namespace TodoApp.Playground.OOPPlayground.Static;

// Describiption of static 
// Static members are shared among all instances of a class.
// Static members are accessed using the class name.
public class Sample
{
    public static int Score = 0;
    public int InstanceScore = 0;
    
    public void IncreaseScore()
    {
        Score++;
    }
    public void IncreaseInstanceScore()
    {
        InstanceScore++;
    }
}

public class OOPPlayground
{
    public void Run()
    {
        var sample1 = new Sample();
        sample1.IncreaseScore();
        
        var sample2 = new Sample();
        
        sample2.IncreaseScore();
        sample2.IncreaseScore();
        
        sample1.IncreaseInstanceScore();
        sample2.IncreaseInstanceScore();
        sample2.IncreaseInstanceScore();
        
        Console.WriteLine($"Score: {sample1.InstanceScore}"); // Score becomes 1
        Console.WriteLine($"Score: {sample2.InstanceScore}"); // Score becomes 2
        Console.WriteLine($"Score: {Sample.Score}"); // Score becomes 3
        //Console.WriteLine($"Score: {sample1.Score}"); // Score is not accessable through an instance of class
    }
}