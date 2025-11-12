-- 測試資料庫連接和查看資料
USE DrinkShopDB;
GO

-- 檢查分類資料
SELECT COUNT(*) AS [分類總數] FROM DrinkCategories;
SELECT * FROM DrinkCategories ORDER BY SortOrder;

-- 檢查飲品資料
SELECT COUNT(*) AS [飲品總數] FROM Drinks;
SELECT TOP 10 * FROM Drinks ORDER BY CategoryId, SortOrder;

-- 檢查每個分類的飲品數量
SELECT 
    c.Name AS [分類名稱],
    COUNT(d.Id) AS [飲品數量]
FROM DrinkCategories c
LEFT JOIN Drinks d ON c.Id = d.CategoryId
GROUP BY c.Name, c.SortOrder
ORDER BY c.SortOrder;


