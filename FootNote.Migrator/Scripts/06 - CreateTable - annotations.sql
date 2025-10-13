CREATE TABLE annotations (
    id UUID PRIMARY KEY,       
    time_stamp TIMESTAMP WITH TIME ZONE NOT NULL,
    type SMALLINT NOT NULL,   
    description TEXT
);