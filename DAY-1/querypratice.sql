
-- STEP 1: Create customer table

CREATE TABLE customer (
   customer_id SERIAL PRIMARY KEY,
   first_name VARCHAR(100) NOT NULL,
   last_name VARCHAR(100) NOT NULL,
   email VARCHAR(255) UNIQUE NOT NULL,
   created_date TIMESTAMPTZ NOT NULL DEFAULT NOW(),
   updated_date TIMESTAMPTZ
);


-- STEP 2: Add a new column
ALTER TABLE customer ADD COLUMN active BOOLEAN;


-- STEP 3: Rename email to email_address
ALTER TABLE customer RENAME COLUMN email TO email_address;


-- STEP 4: Create orders table
CREATE TABLE orders (
    order_id SERIAL PRIMARY KEY,
    customer_id INTEGER NOT NULL REFERENCES customer(customer_id),
    order_date TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    order_number VARCHAR(50) NOT NULL,
    order_amount DECIMAL(10,2) NOT NULL
);


-- STEP 5: Insert single customer
INSERT INTO customer (first_name, last_name, email_address, created_date, updated_date, active)
VALUES ('Preksha', 'Divaraniya', 'preksha.divaraniya@tatvasoft.com', NOW(), NULL, true);


-- STEP 6: Insert multiple customers
INSERT INTO customer (first_name, last_name, email_address, created_date, updated_date, active) VALUES
  ('John', 'Doe', 'johndoe@example.com', NOW(), NULL, true),
  ('Alice', 'Smith', 'alicesmith@example.com', NOW(), NULL, true),
  ('Bob', 'Johnson', 'bjohnson@example.com', NOW(), NULL, true),
  ('Emma', 'Brown', 'emmabrown@example.com', NOW(), NULL, true),
  ('Michael', 'Lee', 'michaellee@example.com', NOW(), NULL, false),
  ('Sarah', 'Wilson', 'sarahwilson@example.com', NOW(), NULL, true),
  ('David', 'Clark', 'davidclark@example.com', NOW(), NULL, true),
  ('Olivia', 'Martinez', 'oliviamartinez@example.com', NOW(), NULL, true),
  ('James', 'Garcia', 'jamesgarcia@example.com', NOW(), NULL, false),
  ('Sophia', 'Lopez', 'sophialopez@example.com', NOW(), NULL, false);


-- STEP 7: Insert orders
INSERT INTO orders (customer_id, order_date, order_number, order_amount) VALUES
  (1, '2024-01-01', 'ORD001', 50.00),
  (2, '2024-01-01', 'ORD002', 35.75),
  (3, '2024-01-01', 'ORD003', 100.00),
  (4, '2024-01-01', 'ORD004', 30.25),
  (5, '2024-01-01', 'ORD005', 90.75),
  (6, '2024-01-01', 'ORD006', 25.50),
  (7, '2024-01-01', 'ORD007', 60.00),
  (8, '2024-01-01', 'ORD008', 42.00),
  (9, '2024-01-01', 'ORD009', 120.25),
  (10,'2024-01-01', 'ORD010', 85.00),
  (1, '2024-01-02', 'ORD011', 55.00),
  (1, '2024-01-03', 'ORD012', 80.25),
  (2, '2024-01-03', 'ORD013', 70.00),
  (3, '2024-01-04', 'ORD014', 45.00),
  (1, '2024-01-05', 'ORD015', 95.50);


-- STEP 8: Select queries
SELECT first_name FROM customer;

SELECT first_name, last_name, email_address FROM customer;

SELECT * FROM customer;


-- STEP 9: ORDER BY examples
SELECT first_name, last_name FROM customer ORDER BY first_name ASC;

SELECT first_name, last_name FROM customer ORDER BY last_name DESC;

SELECT customer_id, first_name, last_name FROM customer
ORDER BY first_name ASC, last_name DESC;

-- STEP 10: WHERE clause examples
SELECT last_name, first_name FROM customer
WHERE first_name = 'Hiren';

SELECT customer_id, first_name, last_name FROM customer
WHERE first_name = 'Hiren' AND last_name = 'Parejiya';

SELECT customer_id, first_name, last_name FROM customer
WHERE first_name IN ('John', 'David', 'James');

SELECT first_name, last_name FROM customer
WHERE first_name LIKE '%EN%'; -- case-sensitive

SELECT first_name, last_name FROM customer
WHERE first_name ILIKE '%EN%'; -- case-insensitive


-- STEP 11: JOIN examples
SELECT * FROM orders o
INNER JOIN customer c ON o.customer_id = c.customer_id;

SELECT * FROM customer c
RIGHT JOIN orders o ON c.customer_id = o.customer_id;


-- STEP 12: GROUP BY aggregation
SELECT c.customer_id, c.first_name, c.last_name, c.email_address,
       COUNT(o.order_id) AS no_of_orders,
       SUM(o.order_amount) AS total_amount
FROM customer c
INNER JOIN orders o ON c.customer_id = o.customer_id
GROUP BY c.customer_id;


-- STEP 13: GROUP BY with HAVING
SELECT c.customer_id, c.first_name, c.last_name, c.email_address,
       COUNT(o.order_id) AS no_of_orders,
       SUM(o.order_amount) AS total_amount
FROM customer c
INNER JOIN orders o ON c.customer_id = o.customer_id
GROUP BY c.customer_id
HAVING COUNT(o.order_id) >= 2;


-- STEP 14: Subqueries
-- Subquery with IN
SELECT * FROM orders
WHERE customer_id IN (
  SELECT customer_id FROM customer WHERE active = true
);

-- Subquery with EXISTS
SELECT customer_id, first_name, last_name, email_address
FROM customer
WHERE EXISTS (
  SELECT 1 FROM orders WHERE orders.customer_id = customer.customer_id
);


-- STEP 15: Update statement
UPDATE customer
SET first_name = 'Priya',
    last_name = 'Trivedi',
    email_address = 'priyatrivedi@example.com'
WHERE customer_id = 10;


-- STEP 16: Delete statement
DELETE FROM customer WHERE customer_id = 11;


-- Final data check
SELECT * FROM customer;
SELECT * FROM orders;

