using System.Collections.Generic;

namespace Main.ViewModels;

public class FrontPageViewModel: ViewModelBase
{
    public List<string> Slides { get; set; } = new() {"123123", "123123"};
    
    public int CurrentSlide { get; set; }
}