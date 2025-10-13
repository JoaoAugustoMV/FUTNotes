ALTER TABLE annotations
ADD COLUMN annotation_session_id UUID;

ALTER TABLE annotations
ADD CONSTRAINT fk_annotations_session
FOREIGN KEY (annotation_session_id)
REFERENCES annotation_sessions(id)
ON DELETE CASCADE;