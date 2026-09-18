namespace Common.Authorization;

public static class Permissions
{
    public const string ClaimType = "permission";

    public static class Categories
    {
        public const string Create = "categories.create";
        public const string Update = "categories.update";
        public const string Delete = "categories.delete";
    }

    public static class Products
    {
        public const string Create = "products.create";
        public const string Update = "products.update";
        public const string Delete = "products.delete";
    }

    public static IReadOnlyList<string> All =>
    [
        Categories.Create, Categories.Update, Categories.Delete,
        Products.Create, Products.Update, Products.Delete
    ];
}
