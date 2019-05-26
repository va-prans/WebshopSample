USE webshop;

CREATE VIEW ExpensiveItems AS
SELECT i.Name, i.Price, c.Name CatName
FROM Items i JOIN Categories c
ON i.CategoryId = c.CategoryId
ORDER BY i.Price DESC
LIMIT 20;