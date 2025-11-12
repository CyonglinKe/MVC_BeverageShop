-- =============================================
-- 飲料店資料庫設置腳本
-- 在 MS SQL Server 中執行此腳本
-- =============================================

-- 步驟 1: 創建資料庫
USE master;
GO

-- 如果資料庫已存在，先刪除（開發環境使用，生產環境請小心！）
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'DrinkShopDB')
BEGIN
    ALTER DATABASE DrinkShopDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE DrinkShopDB;
END
GO

-- 創建新資料庫
CREATE DATABASE DrinkShopDB;
GO

-- 切換到新資料庫
USE DrinkShopDB;
GO

-- =============================================
-- 步驟 2: 創建飲品分類表格
-- =============================================
CREATE TABLE DrinkCategories (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL,
    Description NVARCHAR(200) NULL,
    IconClass NVARCHAR(50) NULL,
    SortOrder INT NOT NULL DEFAULT 0,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

-- =============================================
-- 步驟 3: 創建飲品表格
-- =============================================
CREATE TABLE Drinks (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500) NULL,
    Price DECIMAL(18,2) NOT NULL,
    CategoryId INT NOT NULL,
    ImageUrl NVARCHAR(500) NULL,
    IsAvailable BIT NOT NULL DEFAULT 1,
    IsHot BIT NOT NULL DEFAULT 1,
    IsCold BIT NOT NULL DEFAULT 1,
    IsSeasonal BIT NOT NULL DEFAULT 0,
    SortOrder INT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME2 NULL,
    CONSTRAINT FK_Drinks_DrinkCategories FOREIGN KEY (CategoryId) 
        REFERENCES DrinkCategories(Id)
);
GO

-- 建立索引以提升查詢效能
CREATE INDEX IX_Drinks_CategoryId ON Drinks(CategoryId);
CREATE INDEX IX_Drinks_IsAvailable ON Drinks(IsAvailable);
GO

-- =============================================
-- 步驟 4: 插入分類資料
-- =============================================
INSERT INTO DrinkCategories (Name, Description, IconClass, SortOrder, IsActive)
VALUES 
    (N'經典茶類', N'傳統茶飲，經典口味', 'fas fa-leaf', 1, 1),
    (N'水果茶類', N'新鮮水果與茶的完美結合', 'fas fa-apple-alt', 2, 1),
    (N'鮮奶茶類', N'香濃鮮奶與茶的搭配', 'fas fa-coffee', 3, 1),
    (N'奶蓋茶類', N'濃郁奶蓋，層次豐富', 'fas fa-cloud', 4, 1),
    (N'特色飲品', N'獨特配方，創意無限', 'fas fa-star', 5, 1),
    (N'咖啡飲品', N'香醇咖啡，提神醒腦', 'fas fa-mug-hot', 6, 1),
    (N'季節限定', N'限時推出，錯過可惜', 'fas fa-snowflake', 7, 1);
GO

-- =============================================
-- 步驟 5: 插入飲品資料
-- =============================================

-- 經典茶類 (CategoryId = 1)
INSERT INTO Drinks (Name, Description, Price, CategoryId, SortOrder, IsHot, IsCold, IsSeasonal)
VALUES 
    (N'珍珠奶茶', N'Q彈珍珠搭配香濃奶茶', 55, 1, 1, 1, 1, 0),
    (N'四季春', N'清香淡雅，回甘甘甜', 35, 1, 2, 1, 1, 0),
    (N'紅茶', N'經典紅茶，香醇濃郁', 30, 1, 3, 1, 1, 0),
    (N'烏龍茶', N'台灣烏龍，茶香四溢', 35, 1, 4, 1, 1, 0),
    (N'鐵觀音', N'濃郁鐵觀音，回甘持久', 40, 1, 5, 1, 1, 0),
    (N'綠茶', N'清新綠茶，健康首選', 30, 1, 6, 1, 1, 0),
    (N'青茶', N'清香青茶，淡雅怡人', 35, 1, 7, 1, 1, 0),
    (N'仙女紅茶', N'傳說中的仙女紅茶', 45, 1, 8, 1, 1, 0);
GO

-- 水果茶類 (CategoryId = 2)
INSERT INTO Drinks (Name, Description, Price, CategoryId, SortOrder, IsHot, IsCold, IsSeasonal)
VALUES 
    (N'檸檬綠茶', N'新鮮檸檬搭配清香綠茶', 50, 2, 1, 0, 1, 0),
    (N'百香綠茶', N'酸甜百香果與綠茶', 55, 2, 2, 0, 1, 0),
    (N'芒果綠茶', N'香甜芒果與清新綠茶', 60, 2, 3, 0, 1, 0),
    (N'柳丁綠茶', N'鮮甜柳丁與綠茶', 50, 2, 4, 0, 1, 0),
    (N'荔枝紅茶', N'香甜荔枝與濃郁紅茶', 55, 2, 5, 0, 1, 0),
    (N'莓果康普紅茶', N'綜合莓果與康普茶', 65, 2, 6, 0, 1, 0),
    (N'熱帶水果氣泡', N'熱帶水果與氣泡水', 60, 2, 7, 0, 1, 0),
    (N'香柚綠茶', N'清香柚子與綠茶', 55, 2, 8, 0, 1, 0),
    (N'蘋果芒芒綠', N'蘋果芒果與綠茶', 60, 2, 9, 0, 1, 0);
GO

-- 鮮奶茶類 (CategoryId = 3)
INSERT INTO Drinks (Name, Description, Price, CategoryId, SortOrder, IsHot, IsCold, IsSeasonal)
VALUES 
    (N'芋頭鮮奶', N'香濃芋頭與鮮奶', 65, 3, 1, 1, 1, 0),
    (N'黑糖鮮奶', N'濃郁黑糖與鮮奶', 60, 3, 2, 1, 1, 0),
    (N'豆漿紅茶', N'香濃豆漿與紅茶', 50, 3, 3, 1, 1, 0),
    (N'鮮奶拿鐵', N'香濃鮮奶拿鐵', 55, 3, 4, 1, 1, 0);
GO

-- 奶蓋茶類 (CategoryId = 4)
INSERT INTO Drinks (Name, Description, Price, CategoryId, SortOrder, IsHot, IsCold, IsSeasonal)
VALUES 
    (N'黑朵奶蓋四季春', N'濃郁奶蓋與四季春', 60, 4, 1, 0, 1, 0),
    (N'黑朵奶蓋鐵觀音', N'濃郁奶蓋與鐵觀音', 65, 4, 2, 0, 1, 0),
    (N'海鹽奶蓋', N'鹹甜海鹽奶蓋', 55, 4, 3, 0, 1, 0),
    (N'芝士奶蓋', N'濃郁芝士奶蓋', 60, 4, 4, 0, 1, 0);
GO

-- 特色飲品 (CategoryId = 5)
INSERT INTO Drinks (Name, Description, Price, CategoryId, SortOrder, IsHot, IsCold, IsSeasonal)
VALUES 
    (N'咖啡凍拿鐵', N'Q彈咖啡凍與拿鐵', 65, 5, 1, 0, 1, 0),
    (N'布丁奶茶', N'滑嫩布丁與奶茶', 55, 5, 2, 1, 1, 0),
    (N'燒仙草', N'傳統燒仙草', 45, 5, 3, 1, 1, 0),
    (N'愛玉', N'清涼愛玉', 40, 5, 4, 0, 1, 0),
    (N'粉粿', N'Q彈粉粿', 35, 5, 5, 0, 1, 0),
    (N'寒天晶球', N'晶瑩剔透寒天', 40, 5, 6, 0, 1, 0),
    (N'檸檬冬瓜', N'酸甜檸檬與冬瓜', 45, 5, 7, 1, 1, 0),
    (N'冬瓜檸檬', N'清香冬瓜與檸檬', 45, 5, 8, 1, 1, 0),
    (N'養樂多系列', N'酸甜養樂多', 50, 5, 9, 0, 1, 0);
GO

-- 咖啡飲品 (CategoryId = 6)
INSERT INTO Drinks (Name, Description, Price, CategoryId, SortOrder, IsHot, IsCold, IsSeasonal)
VALUES 
    (N'美式咖啡', N'香醇美式咖啡', 50, 6, 1, 1, 1, 0),
    (N'拿鐵咖啡', N'香濃拿鐵咖啡', 60, 6, 2, 1, 1, 0),
    (N'卡布奇諾', N'經典卡布奇諾', 60, 6, 3, 1, 1, 0),
    (N'濃情巧克力', N'濃郁巧克力飲品', 55, 6, 4, 1, 1, 0);
GO

-- 季節限定 (CategoryId = 7)
INSERT INTO Drinks (Name, Description, Price, CategoryId, SortOrder, IsHot, IsCold, IsSeasonal)
VALUES 
    (N'芒果季節限定', N'香甜芒果限定飲品', 70, 7, 1, 0, 1, 1),
    (N'草莓季節限定', N'酸甜草莓限定飲品', 70, 7, 2, 0, 1, 1);
GO

-- =============================================
-- 步驟 6: 驗證資料
-- =============================================
PRINT '=============================================';
PRINT '資料庫設置完成！';
PRINT '=============================================';
PRINT '';

-- 顯示分類統計
PRINT '飲品分類統計:';
SELECT 
    c.Name AS [分類名稱],
    COUNT(d.Id) AS [飲品數量]
FROM DrinkCategories c
LEFT JOIN Drinks d ON c.Id = d.CategoryId
GROUP BY c.Name, c.SortOrder
ORDER BY c.SortOrder;
PRINT '';

-- 顯示總計
PRINT '總計資料:';
SELECT 
    (SELECT COUNT(*) FROM DrinkCategories) AS [分類總數],
    (SELECT COUNT(*) FROM Drinks) AS [飲品總數],
    (SELECT COUNT(*) FROM Drinks WHERE IsSeasonal = 1) AS [季節限定數];
PRINT '';

PRINT '✅ 資料庫 DrinkShopDB 已成功創建並插入測試資料！';
PRINT '✅ 您現在可以在應用程式中連接此資料庫了。';
GO


