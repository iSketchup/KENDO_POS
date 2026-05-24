using Main.Control;

namespace Main.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public EditorViewModel Editor { get; } = new();
    public ShaderRendererViewModel ShaderRenderer { get; } = new ();

    public MainWindowViewModel()
    {
        ShaderRenderer.ChangeCode(Editor.Code);
    }
}