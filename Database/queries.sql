PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS computer(
    id INTEGER PRIMARY KEY,
    uuid TEXT NOT NULL UNIQUE,
    name TEXT NOT NULL,
    brand TEXT NOT NULL,
    model TEXT NOT NULL,
    price REAL NOT NULL
);
