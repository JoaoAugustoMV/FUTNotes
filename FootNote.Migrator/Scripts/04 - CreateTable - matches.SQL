CREATE TABLE IF NOT EXISTS matches 
(
    id UUID NOT NULL,
    home_team_id UUID NOT NULL,
    away_team_id UUID NOT NULL,
    competition_id UUID NULL,
    match_date TIMESTAMPTZ NULL,
    status SMALLINT NOT NULL,
    decision_type SMALLINT NULL,       
    home_score INT NULL,
    away_score INT NULL,
    home_penalty_score INT NULL,
    away_penalty_score INT NULL,

    CONSTRAINT matchs_pkey PRIMARY KEY (id)
);