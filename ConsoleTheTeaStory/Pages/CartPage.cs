namespace ConsoleTheTeaStory.Pages
{
    public class CartPage
    {
        public CartPage() { }

        public static bool IsItemAdded(string item)
        {
            return new CartCommand().IsItemAdded(item);
        }
    }
}
