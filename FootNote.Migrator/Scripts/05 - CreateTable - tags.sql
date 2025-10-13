CREATE TABLE tags (
    id UUID PRIMARY KEY,            
    name TEXT NOT NULL,
    is_default BOOLEAN NOT NULL,
    user_id UUID,                    
    created_at TIMESTAMP WITH TIME ZONE NOT NULL,
    
    CONSTRAINT uq_tag_user UNIQUE (name, user_id)  
);
