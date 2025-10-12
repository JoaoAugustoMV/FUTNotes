CREATE TABLE teams (
    id UUID PRIMARY KEY, 
    name VARCHAR(255) NOT NULL,
    has_created_manually BOOLEAN NOT NULL,
    short_name VARCHAR(50) NULL,
    coach_id UUID NULL
);