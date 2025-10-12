CREATE TABLE competitions (
    id UUID PRIMARY KEY, -- UUIDv7 gerado no backend
    name VARCHAR(255) NOT NULL,    
    season VARCHAR(50) NULL,
    scope SMALLINT NOT NULL,
    type SMALLINT NOT NULL
);