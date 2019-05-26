USE webshop;

DELIMITER //
 CREATE PROCEDURE GetLowStockProducts()
   BEGIN
   SELECT *  FROM Items where Items.Stock < 50;
   END //
DELIMITER ;