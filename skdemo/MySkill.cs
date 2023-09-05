using Microsoft.SemanticKernel.SkillDefinition;

class MySkill {
    [SKFunction]
    public string Uppercase(string input) => input.ToUpperInvariant();

    [SKFunction]
    public string FullName(string firstName, string lastName) => $"{firstName} {lastName}";
}