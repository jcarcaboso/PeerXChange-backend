BEGIN;

CREATE SCHEMA IF NOT EXISTS dbo;

CREATE TABLE dbo.role
(
    id int primary key,
    name TEXT not null
);

CREATE TABLE
    dbo.user
(
    wallet            TEXT PRIMARY KEY,
    role               INTEGER NOT NULL,
    language           VARCHAR(2) NOT NULL,
    creation_timestamp TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    is_active          BOOLEAN     DEFAULT TRUE,
    is_deleted         BOOLEAN     DEFAULT FALSE,
    delete_deadline    TIMESTAMPTZ,
    CONSTRAINT fk_user_role FOREIGN KEY (role) REFERENCES dbo.role (id)
);

CREATE TABLE
    dbo.user_configuration
(
    user_wallet TEXT PRIMARY KEY,
    email TEXT,
    default_currency VARCHAR(3),
    CONSTRAINT fk_user_address FOREIGN KEY (user_wallet) REFERENCES dbo.user (wallet)
);

COMMIT;