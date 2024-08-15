BEGIN;

CREATE SCHEMA IF NOT EXISTS data;

CREATE TABLE
    data.chain (
        id INT PRIMARY KEY,
        hex_id TEXT NOT NULL,
        name TEXT NOT NULL,
        symbol TEXT NOT NULL,
        img TEXT,
        is_testnet BOOLEAN DEFAULT FALSE,
        creation TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
        is_active BOOLEAN DEFAULT TRUE
    );

CREATE TABLE
    data.token (
        id UUID PRIMARY KEY DEFAULT gen_random_uuid (),
        name TEXT NOT NULL,
        symbol VARCHAR(10) NOT NULL,
        img TEXT,
        creation TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
        is_active BOOLEAN DEFAULT TRUE
    );

CREATE TABLE data.chain_token (
    chain_id INT NOT NULL,
    id UUID PRIMARY KEY NOT NULL,
    address TEXT NOT NULL,
    UNIQUE(chain_id, address),
    CONSTRAINT fk_chain_id FOREIGN KEY (chain_id) REFERENCES data.chain (id) ON DELETE CASCADE,
    CONSTRAINT fk_token_id FOREIGN KEY (token_id) REFERENCES data.token (id) ON DELETE CASCADE
);

CREATE TABLE
    data.currency (
        id int primary key,
        iso_code VARCHAR(3) NOT NULL,
        name TEXT NOT NULL,
        description TEXT,
        img TEXT,
        creation TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
        is_active BOOLEAN DEFAULT TRUE
    );

CREATE TABLE
    data.payment (
        id UUID PRIMARY KEY DEFAULT gen_random_uuid (),
        name TEXT NOT NULL,
        creation TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
        is_active BOOLEAN DEFAULT TRUE
    );

COMMIT;