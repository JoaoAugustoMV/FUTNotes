CREATE TABLE annotation_tags (
    annotation_id UUID NOT NULL REFERENCES annotations(id) ON DELETE CASCADE,
    tag_id UUID NOT NULL REFERENCES tags(id) ON DELETE CASCADE,
    PRIMARY KEY (annotation_id, tag_id)
);