interface ITagHelper
{
    string CreateTagHelper(string objectName, string controller, string action, int id) 
    {
        return ("<a asp-controller={0} asp-action={1} asp-route-Id={2}>{3}</a>", controller, action, id, objectName).ToString();
    }
}
