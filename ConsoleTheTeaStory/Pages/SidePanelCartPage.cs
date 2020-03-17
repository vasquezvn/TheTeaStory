namespace ConsoleTheTeaStory.Pages
{
    public class SidePanelCartPage
    {
        public static void ClickViewCart()
        {
            new SidePanelCartCommand().ClickViewCart();
        }

        public static bool IsQuantityCorrect(int quantity)
        {
            return new SidePanelCartCommand().IsQuantityCorrect(quantity);
        }
    }
}
