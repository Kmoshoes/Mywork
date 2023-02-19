

CREATE TABLE public.users
(
    id integer NOT NULL ,
    name character varying(120) NOT NULL,
    lastname character varying(120) NOT NULL,
    deleted bit varying(1) NOT NULL,
    CONSTRAINT users_pkey PRIMARY KEY (id)
)
