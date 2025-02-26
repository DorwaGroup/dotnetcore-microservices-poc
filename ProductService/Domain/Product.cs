﻿namespace ProductService.Domain;

public class Product
{
    private Product()
    {
    }

    private Product(string code, string name, string image, string description, int maxNumberOfInsured,
        string productIcon)
    {
        Id = Guid.NewGuid();
        Code = code;
        Name = name;
        Status = ProductStatus.Draft;
        Image = image;
        Description = description;
        Covers = new List<Cover>();
        Questions = new List<Question>();
        MaxNumberOfInsured = maxNumberOfInsured;
        ProductIcon = productIcon;
    }

    public Guid Id { get; }
    public string Code { get; }
    public string Name { get; }
    public ProductStatus Status { get; private set; }
    public string Image { get; }
    public string Description { get; }
    public IList<Cover> Covers { get; }
    public IList<Question> Questions { get; }
    public int MaxNumberOfInsured { get; }

    public string ProductIcon { get; }

    public static Product CreateDraft(string code, string name, string image, string description,
        int maxNumberOfInsured, string productIcon)
    {
        return new Product(code, name, image, description, maxNumberOfInsured, productIcon);
    }

    public void Activate()
    {
        EnsureIsDraft();
        Status = ProductStatus.Active;
    }

    public void Discontinue()
    {
        Status = ProductStatus.Discontinued;
    }

    public void AddCover(string code, string name, string description, bool optional, decimal? sumInsured)
    {
        EnsureIsDraft();
        Covers.Add(new Cover(code, name, description, optional, sumInsured));
    }

    public void AddQuestions(IEnumerable<Question> questions)
    {
        EnsureIsDraft();
        foreach (var q in questions)
            Questions.Add(q);
    }

    private void EnsureIsDraft()
    {
        if (Status != ProductStatus.Draft)
            throw new ApplicationException("Only draft version can be modified and activated");
    }
}

public enum ProductStatus
{
    Draft,
    Active,
    Discontinued
}