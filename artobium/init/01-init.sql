-- ============================================================
--  Example database schema & seed data
--  Runs automatically on first container start
-- ============================================================

-- Extensions
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- ── Tables ───────────────────────────────────────────────────

CREATE TABLE users (
    id         UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    username   VARCHAR(50)  NOT NULL UNIQUE,
    email      VARCHAR(255) NOT NULL UNIQUE,
    created_at TIMESTAMPTZ  NOT NULL DEFAULT NOW()
);

CREATE TABLE products (
    id          UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name        VARCHAR(100)   NOT NULL,
    description TEXT,
    price       NUMERIC(10, 2) NOT NULL CHECK (price >= 0),
    stock       INTEGER        NOT NULL DEFAULT 0 CHECK (stock >= 0),
    created_at  TIMESTAMPTZ    NOT NULL DEFAULT NOW()
);

CREATE TABLE orders (
    id         UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    user_id    UUID        NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    status     VARCHAR(20) NOT NULL DEFAULT 'pending'
                   CHECK (status IN ('pending', 'paid', 'shipped', 'cancelled')),
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE TABLE order_items (
    id         UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    order_id   UUID           NOT NULL REFERENCES orders(id)   ON DELETE CASCADE,
    product_id UUID           NOT NULL REFERENCES products(id) ON DELETE RESTRICT,
    quantity   INTEGER        NOT NULL CHECK (quantity > 0),
    unit_price NUMERIC(10, 2) NOT NULL CHECK (unit_price >= 0)
);

-- ── Indexes ───────────────────────────────────────────────────

CREATE INDEX idx_orders_user_id      ON orders(user_id);
CREATE INDEX idx_order_items_order   ON order_items(order_id);
CREATE INDEX idx_order_items_product ON order_items(product_id);

-- ── Seed data ─────────────────────────────────────────────────

INSERT INTO users (username, email) VALUES
    ('alice',   'alice@example.com'),
    ('bob',     'bob@example.com'),
    ('charlie', 'charlie@example.com');

INSERT INTO products (name, description, price, stock) VALUES
    ('Mechanical Keyboard', 'Tactile 80% layout, RGB backlit',  129.99, 50),
    ('USB-C Hub',           '7-in-1 hub with 4K HDMI',          49.99, 120),
    ('Webcam 1080p',        'Wide-angle lens, built-in mic',     79.99, 35),
    ('Desk Lamp',           'LED with adjustable colour temp',   39.99, 200),
    ('Laptop Stand',        'Aluminium, adjustable height',      59.99, 75);

-- Place a sample order for alice
WITH alice AS (SELECT id FROM users WHERE username = 'alice'),
     kb    AS (SELECT id, price FROM products WHERE name = 'Mechanical Keyboard'),
     hub   AS (SELECT id, price FROM products WHERE name = 'USB-C Hub'),
     new_order AS (
         INSERT INTO orders (user_id, status)
         SELECT alice.id, 'paid' FROM alice
         RETURNING id
     )
INSERT INTO order_items (order_id, product_id, quantity, unit_price)
SELECT new_order.id, kb.id,  1, kb.price  FROM new_order, kb  UNION ALL
SELECT new_order.id, hub.id, 2, hub.price FROM new_order, hub;