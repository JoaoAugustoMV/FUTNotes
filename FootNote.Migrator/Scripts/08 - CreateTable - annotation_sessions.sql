CREATE TABLE annotation_sessions (
    id UUID PRIMARY KEY,
    user_id UUID NOT NULL,
    match_id UUID NOT NULL,
    started TIMESTAMP WITH TIME ZONE NOT NULL,
    ended TIMESTAMP WITH TIME ZONE,          
    status SMALLINT NOT NULL,
    type SMALLINT NOT NULL  
);
