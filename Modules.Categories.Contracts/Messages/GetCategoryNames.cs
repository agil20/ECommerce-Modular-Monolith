namespace Modules.Categories.Contracts.Messages;

// Sual — Products göndərir
public record GetCategoryNames(List<int> CategoryIds);

// Cavab — Categories qaytarır
public record CategoryNamesResult(Dictionary<int, string> Names);
