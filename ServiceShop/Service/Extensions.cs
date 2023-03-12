namespace ServiceShop.Service
{
    public static class Extensions
    {
        //example HomeController/Index -> Home/Index, valuable when use nameof(controllerName);
        public static string CutController(this string str)
        {
            return str.Replace("Controller", "");
        }
    }
}
