using TMPro;

namespace UI
{
    public class UITextMeshPro : UIElement<TextMeshProUGUI, string>
    {
        protected override void SetValue(string arg)
        {
            Component.text = arg;
        }
    }
}