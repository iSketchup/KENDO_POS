namespace Main.ViewModels;

public class EditorViewModel : ViewModelBase
{
    public string Code { get; set; } = """
                                       #version 330

                                       out vec4 outputColor;

                                       in vec2 TexCoord;

                                       uniform sampler2D texture0;

                                       void main()
                                       {
                                           outputColor = texture(texture0, TexCoord);
                                       }
                                       """;
}