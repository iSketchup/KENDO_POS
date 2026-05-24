namespace Main.ViewModels;

public class EditorViewModel : ViewModelBase
{
    public string Code { get; set; } = """
                                       #version 330 core

                                       in vec2 pos;

                                       uniform float uTime;

                                       out vec4 FragColor;

                                       void main()
                                       {
                                           float pulse = sin(uTime) * 0.5 + 0.5;

                                           float r = (pos.x + 1.0) * 0.5;
                                           float g = (pos.y + 1.0) * 0.5;

                                           FragColor = vec4(r * pulse, g * pulse, 0.9 - r * pulse, 1.0);
                                       }
                                       """;
}