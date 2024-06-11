BEGIN;

CREATE SCHEMA IF NOT EXISTS dbo;

CREATE TABLE dbo.role
(
    id int primary key,
    name TEXT not null
);

CREATE TABLE
    dbo.wallet
(
    address            TEXT PRIMARY KEY,
    role               INTEGER NOT NULL,
    language           VARCHAR(2) NOT NULL,
    creation_timestamp TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    is_active          BOOLEAN     DEFAULT TRUE,
    is_deleted         BOOLEAN     DEFAULT FALSE,
    delete_deadline    TIMESTAMPTZ,
    CONSTRAINT fk_user_role FOREIGN KEY (role) REFERENCES dbo.role (id)
);

-- TODO: Add additional data related to the wallet registered
CREATE TABLE
    dbo.user_additional_data
(
    wallet TEXT PRIMARY KEY,
    email TEXT,
    default_currency VARCHAR(3),
    CONSTRAINT fk_wallet_address FOREIGN KEY (wallet) REFERENCES dbo.wallet (address)
);

COMMIT;