

CREATE TABLE public.tenants
(
    id integer NOT NULL ,
    userid integer NOT NULL,
    houseaddress character varying(120) NOT NULL,
    deleted bit varying(1) NOT NULL,
    CONSTRAINT tenants_pkey PRIMARY KEY (id),
    CONSTRAINT fk_tenantuserid FOREIGN KEY (userid)
        REFERENCES public.users (id)
)

