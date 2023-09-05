using System.Data.SqlClient;
using System.Runtime.InteropServices;
using Microsoft.SemanticKernel.SkillDefinition;

class NorthwindSkill {
    
    [SKFunction]
    public async Task<string?> GetProducts() {
        var sql = @"
SELECT 
    p.ProductID,
    p.ProductName,
    s.CompanyName AS SupplierName,
    c.CategoryName,
    p.UnitPrice,
    p.UnitsInStock
FROM Products p 
JOIN Categories c on p.CategoryID=c.CategoryID
JOIN Suppliers s on p.SupplierID=s.SupplierID
WHERE p.ProductID<=10
FOR JSON PATH
";
        var con = new SqlConnection("");
        var com = new SqlCommand(sql, con);
        await con.OpenAsync();
        var json = await com.ExecuteScalarAsync();
        await con.CloseAsync();
        return json == null ? "" : json.ToString();
    }
}