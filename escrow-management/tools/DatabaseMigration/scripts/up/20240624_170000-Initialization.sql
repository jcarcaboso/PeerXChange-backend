BEGIN;

CREATE SCHEMA IF NOT EXISTS ext;

CREATE TABLE ext.user (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    external_id TEXT NOT NULL UNIQUE
);

-------------------------------------------------------------------------------------------------

CREATE SCHEMA IF NOT EXISTS dbo;

CREATE TABLE
    dbo.order_type (id SMALLINT PRIMARY KEY, "type" TEXT NOT NULL);

-------------------------------------------------------------------------------------------------

CREATE SCHEMA IF NOT EXISTS offer;

CREATE TABLE
    offer.offer (
        id UUID PRIMARY KEY DEFAULT gen_random_uuid (),
        user_id TEXT NOT NULL,
        type_id SMALLINT NOT NULL,
        amount DECIMAL NOT NULL,
        description TEXT NOT NULL,
        CONSTRAINT fk_user FOREIGN KEY (user_id) REFERENCES ext.user(id),
        CONSTRAINT fk_type_id FOREIGN KEY (type_id) REFERENCES dbo.order_type (id)
    );

CREATE TABLE
    offer.offer_payments (
        offer_id UUID NOT NULL,
        payment_id UUID NOT NULL,
        PRIMARY KEY (offer_id, payment_id),
        CONSTRAINT fk_offer_id FOREIGN KEY (offer_id) REFERENCES offer.offer (id),
        CONSTRAINT fk_payment_id FOREIGN KEY (payment_id) REFERENCES data.payment (id)
    );

-------------------------------------------------------------------------------------------------

CREATE SCHEMA IF NOT EXISTS escrow;

CREATE TABLE
    escrow.escrow_status (id SMALLINT PRIMARY KEY, "status" TEXT NOT NULL);

CREATE TABLE
    dbo.escrow (
        id UUID PRIMARY KEY DEFAULT gen_random_uuid (),
        offer_id UUID NOT NULL,
        seller TEXT NOT NULL,
        buyer TEXT NOT NULL,
        type_id SMALLINT NOT NULL,
        amount DECIMAL NOT NULL,
        payment_id UUID NOT NULL,
        status_id SMALLINT NOT NULL,
        chain_id UUID NOT NULL,
        token_id UUID NOT NULL,
        CONSTRAINT fk_seller FOREIGN KEY (seller) REFERENCES ext.user (id),
        CONSTRAINT fk_buyer FOREIGN KEY (buyer) REFERENCES ext.user (id),
        CONSTRAINT fk_type_id FOREIGN KEY (type_id) REFERENCES dbo.order_type (id),
        CONSTRAINT fk_payment_id FOREIGN KEY (payment_id) REFERENCES data.payment (id),
        CONSTRAINT fk_status_id FOREIGN KEY (status_id) REFERENCES dbo.escrow_status (id),
        CONSTRAINT fk_offer_id FOREIGN KEY (offer_id) REFERENCES offer.offer(id),
        CONSTRAINT fk_chain_id FOREIGN KEY (chain_id) REFERENCES data.chain(id),
        CONSTRAINT fk_token_id FOREIGN KEY (token_id) REFERENCES data.token(id)
    );

COMMIT;